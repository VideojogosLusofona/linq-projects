using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SampleLP2p1
{
    public class Program
    {
        // Application name
        private const string appName = "MyIMDBSearcher";

        // Name of the file of interest
        private const string fileTitleBasics = "title.basics.tsv.gz";

        // Number of titles to show at a time
        private const int numTitlesToShowOnScreen = 10;

        // Collection of titles
        private ICollection<Title> titles;

        // Different genres
        private ISet<string> allGenres;

        // The program starts here
        private static void Main(string[] args)
        {
            Program p = new Program();
            p.ShowAnExampleOfHowThisMightWork();
        }

        // An example of how some things might work in this project
        private void ShowAnExampleOfHowThisMightWork()
        {
            // Helper variable used for searches
            Title[] queryResults;

            // Number of titles
            int numTitles = 0;

            // Number of titles already shown to the user
            int numTitlesShown = 0;

            // Initialize set containing the different genres in the database (does not allow repeated genres)
            allGenres = new HashSet<string>();

            // Full path of the folder containing the data files
            string folderWithFiles = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), appName);

            // Full path of each of the data files
            string fileTitleBasicsFull = Path.Combine(folderWithFiles, fileTitleBasics);

            // Count number of lines (number of titles)
            GZipReader(fileTitleBasicsFull, (line) => numTitles++);

            // Instantiate list with pre-defined size for the number of existing titles
            titles = new List<Title>(numTitles);

            // Fill the list of titles with information read from the file
            GZipReader(fileTitleBasicsFull, LineToTitle);

            // How much memory are we occupying?
            Console.WriteLine("\t=> Program is currently occupying " + ((Process.GetCurrentProcess().VirtualMemorySize64) / 1024 / 1024) + " megabytes of memory");

            // Show all known genres, sorted by themselves
            Console.Write($"\t=> Known genres (total {allGenres.Count}): ");
            foreach (string genre in allGenres.OrderBy(g => g))
            {
                Console.Write($"{genre} ");
            }
            Console.WriteLine();

            // Search for titles whose title contains "video" and "game",
            // sorting the results by year and then by title and
            // converting the results into an array so we can
            // browse them efficiently
            queryResults = (from title in titles
                            where title.PrimaryTitle.ToLower().Contains("video")
                            where title.PrimaryTitle.ToLower().Contains("game")
                            select title).OrderBy(title => title.StartYear).ThenBy(title => title.PrimaryTitle).ToArray();

            // Say how many titles were found
            Console.WriteLine($"\t=> There are {queryResults.Count()} titles" + " with \"video\" and \"game\"");

            // Show the titles, 10 at a time
            while (numTitlesShown < queryResults.Length)
            {
                Console.WriteLine($"\t=> Press key to see next {numTitlesToShowOnScreen} titles...");
                Console.ReadKey(true);

                // Show next 10
                for (int i = numTitlesShown; i < numTitlesShown + numTitlesToShowOnScreen && i < queryResults.Length; i++)
                {
                    // Use to improve how we show the genres
                    bool firstGenre = true;

                    // Get current title
                    Title title = queryResults[i];

                    // Show title information
                    Console.Write("\t\t* ");
                    Console.Write($"\"{title.PrimaryTitle}\" ");
                    Console.Write($"({title.StartYear?.ToString() ?? "unknown year"}): ");
                    foreach (string genre in title.Genres)
                    {
                        if (!firstGenre) Console.Write("/ ");
                        Console.Write($"{genre} ");
                        firstGenre = false;
                    }
                    Console.WriteLine();
                }

                // Next 10
                numTitlesShown += numTitlesToShowOnScreen;
            }
        }

        // This method applies an action (in the form of a delegate) to each
        // line of a GZip-compressed text file
        private static void GZipReader(string file, Action<string> actionForEachLine)
        {
            // Open file in read mode
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                // Decorate the file with a compressor for the GZip format
                using (GZipStream gzs = new GZipStream(fs, CompressionMode.Decompress))
                {
                    // Use a StreamReader to simplify reading
                    using (StreamReader sr = new StreamReader(gzs))
                    {
                        // Line to read
                        string line;

                        // Ignore first line of headers
                        sr.ReadLine();

                        // Iterate through lines
                        while ((line = sr.ReadLine()) != null)
                        {
                            // Apply action to the current line
                            actionForEachLine.Invoke(line);
                        }
                    }
                }
            }
        }

        // Method that converts a file line into a title, adding it
        // to the titles collection
        // Also processes the genres
        private void LineToTitle(string line)
        {
            //0 1 2 3 4 5 6 7 8
            //tconst titleType primaryTitle originalTitle isAdult startYear endYear runtimeMinutes genres
            short aux;
            string[] fields = line.Split("\t");
            string[] titleGenres = fields[8].Split(",");
            ICollection<string> cleanTitleGenres = new List<string>();
            short? startYear;

            // Try to determine release year, if possible
            try
            {
                startYear = short.TryParse(fields[5], out aux) ? (short?)aux : null;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Tried to parse '{line}', but got exception '{e.Message}'" + $" with this stack trace: {e.StackTrace}");
            }

            // Remove invalid genres
            foreach (string genre in titleGenres)
                if (genre != null && genre.Length > 0 && genre != @"\N")
                    cleanTitleGenres.Add(genre);

            // Add valid genres to the set of all genres in the database
            foreach (string genre in cleanTitleGenres)
                allGenres.Add(genre);

            // Create new Title using the information obtained from the line
            Title t = new Title(fields[2], startYear, cleanTitleGenres.ToArray());

            // Add Title to the titles collection
            titles.Add(t);
        }
    }
}
