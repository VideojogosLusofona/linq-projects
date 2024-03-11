# Steam Database

## Problem Description

Groups must implement a program in C# that manipulates and analyzes a series of data about Steam games. The program should start by reading a [CSV][] file, which contains the said data. Then, the program user can ask questions about the games, such as which games were released after a certain date, which games support a controller, as well as perform some actions on specific games, such as opening the respective Steam page in the browser or downloading the game's display image.

### CSV File Contents

The [CSV][] files (*comma-separated values*) contain data tables, with the fields in each line separated by commas. This type of file may or may not have a header line. Here is an example:

```
Id,Type,Health,Mana,Shield
1,Elf,50,200,40
2,Dwarf,40,100,150
3,Troll,100,10,140
4,Wizard,25,300,30
```

In this case, the CSV files have a series of fields with information about a Steam-available videogame, namely:

* **ID** - Game ID
* **Name** - Game name
* **ReleaseDate** - Release date
* **RequiredAge** - Minimum age to play
* **DLCCount** - Number of DLCs released
* **Metacritic** - Metacritic score (0 to 100)
* **MovieCount** - Number of *trailers*
* **RecommendationCount** - Number of recommendations
* **ScreenshotCount** - Number of screenshots
* **Owners** - Number of people who have the game
* **NumberOfPlayers** - Number of people who actually played the game
* **AchievementCount** - Number of *achievements*
* **ControllerSupport** - Controller support (*True* or *False*)
* **PlatformWindows** - Windows support (*True* or *False*)
* **PlatformLinux** - Linux support (*True* or *False*)
* **PlatformMac** - Mac support (*True* or *False*)
* **CategorySinglePlayer** - Singleplayer support (*True* or *False*)
* **CategoryMultiplayer** - Multiplayer support (*True* or *False*)
* **CategoryCoop** - Cooperative multiplayer support (*True* or *False*)
* **CategoryIncludeLevelEditor** - Includes level editor (*True* or *False*)
* **CategoryVRSupport** - VR support (*True* or *False*)
* **SupportURL** - Game support/help website
* **AboutText** - Game description
* **HeaderImage** - Game image URL
* **Website** - Game website

Each line in the file corresponds to a game, and the first line indicates the field names (header). Included in this repository is the [`games.csv`](games.csv) file, which allows groups to test their project.

### The Program to Develop

The program can be developed in three distinct ways, without any benefit or limitation in terms of grade. However, support will only be provided for the first form:

1. Console Application
2. Desktop Application
3. Unity Application

Should students choose the first form, the name of the CSV file should be given as the 1st argument on the command line. In the 2nd and 3rd forms, the name of the file should be requested in a graphical interface dialog box. If the file is considered valid, analysis of the games can begin; otherwise, the program must display an appropriate error message. In the case of the program being a console application, it should terminate after the error. If the program is a Desktop or Unity application, it should ask again for the file name in a dialog box.

During file reading, each line should correspond to the instantiation of a `Game` object, which should be stored in a collection containing all the listed games. The `Game` type should have all the fields present in the CSV file, and the type of these fields should be appropriate for the field in question. For example:

* Numeric fields are essentially [`int`][]s.
* Fields with *True* or *False* values should be represented with [`bool`][].
* Text fields should be stored as [`string`][].
* Date fields should use the `struct` [`DateTime`][].
* Fields with URL addresses should use the [`Uri`][] class.

Games that appear more than once should be ignored or discarded without any warning. A game is considered duplicated if it has the same ID. After reading and validating the file, the program must have the following options:

1. Show information about a game
2. Conduct a search
3. Exit

#### Show Information About a Game

If the user selects this option, the program should ask for the ID of the game to show. If the ID is valid (i.e., if it's an [`int`][] and matches a game that exists in the list), the program should display all the information about the game, formatted in a well-organized and pleasant to view manner. If the program is developed in Unity, WPF, or Windows Forms, the information about the game should also display the game's image. If it's developed as a console application, the program should download the image from the Internet and indicate the full path of the image on the local file system. Regardless of the chosen option, it's possible to download an image from the Internet using the [`WebClient`][] class (a good example of using this class is available [here](https://stackoverflow.com/questions/24797485/how-to-download-image-from-url)).

The group may implement the following optional extensions, which allow compensating for possible problems in other parts of the project, facilitating the attainment of the maximum grade:

1. If the program was developed as a console application, display the game image [directly from the console](https://stackoverflow.com/questions/33538527/display-a-image-in-a-console-application/33604540) or through the Windows image viewer.
2. In any type of implementation, give the option of opening the support/help website and the game website using the default Windows browser.

It may be necessary, to implement these extensions, to launch an external program to C#. The way to perform this action is through the [`Process`][] class. Several examples of how to use this class for this purpose are available [here](https://stackoverflow.com/questions/181719/how-do-i-start-a-process-from-c), [here](https://stackoverflow.com/questions/3173775/how-to-run-external-program-via-a-c-sharp-program), and [here](https://stackoverflow.com/questions/4580263/how-to-open-in-default-browser-in-c-sharp).

The implementation of the optional extensions can compensate for possible difficulties in other parts of the project, facilitating the attainment of the maximum grade of 2 points.

#### Conduct a Search

If the user chooses this option, they will have the following options available:

* Specify sort criteria
* Specify search filters
* Perform a search
* Go back

##### Specify Sort Criteria

The following sorting criteria should be available:

* By ID (ascending)
* By name (ascending, in alphabetical order)
* By release date (descending, from the most recent to the oldest)
* By number of DLCs (descending)
* By Metacritic score (descending)
* By number of recommendations (descending)
* By number of people who have the game (descending)
* By the number of people who actually played the game (descending)
* By the number of *achievements* (descending)

The default sorting criterion is by ID. After specifying the sorting criterion, the program should return to the previous menu.

##### Specify Search Filters

In this menu, the following search filters can be incrementally added:

* By name (just a partial match, regardless of uppercase or lowercase)
* By date (games released from)
* By age (older than)
* By Metacritic score (higher than)
* By number of recommendations (higher than)
* By controller support (being *true*)
* By Windows support (being *true*)
* By Linux support (being *true*)
* By Mac support (being *true*)
* By singleplayer support (being *true*)
* By multiplayer support (being *true*)
* By cooperative multiplayer support (being *true*)
* By the inclusion of a level editor (being *true*)
* By VR support (being *true*)

It should be clear to the user which filters have been selected and with which value for the filter by name, by date, and the numeric filters.

##### Perform a Search

This option starts the search with the specified sorting criterion and selected filters. If the project is developed as a console application, 10 or 20 games at a time can be displayed, requiring the user to press a key to see the next 10/20 games. On the other hand, if it's a WPF, Windows Forms, or Unity project, the list of games should be scrollable up and down. It's also possible to have a scrollable list in console mode, but as it's not so straightforward, it remains as an optional extension (with potential benefits to the final grade). In any case, all fields that can be used as a sorting criterion should be displayed for each game.

### Project Organization and Class Structure

The project must be properly organized, making use of classes, `struct`s, and/or enumerations, as appropriate. Each type (i.e., class, `struct`, or enumeration) should be placed in a file with the same name. For example, a class called `Game` should be placed in the `Game.cs` file. In turn, the choice of the collection or collections to use should also be appropriate for the problem.

The class structure should be well thought out and organized logically, making use of *design patterns* when and if appropriate. In particular, the project should be developed taking into account the following principles (well explained in the referenc, which is part of the course bibliography):

* [Each class should have a specific and well-defined responsibility][SRP]. In particular, there should be a clear separation of responsibilities regarding visualization, application control, data manipulation, and file handling.
* Program to interfaces, not implementations. In other words, the code should depend as little as possible on concrete classes. For example, if we use a [`List<T>`][] to store information, and the rest of the code only needs to iterate over the information contained therein, then this rest of the code only needs to know that it is dealing with an [`IEnumerable<T>`][]. This makes it easier to change the concrete collection used in the future, as we may find that a [`HashSet<T>`][] is much more efficient for what we intend to do. A principle that further emphasizes this idea is the [dependency inversion principle][DIP], which states that we should only depend on abstractions (i.e., interfaces and abstract classes) and not on concrete classes. Two interesting explanations on this topic can be found [here](https://stackoverflow.com/questions/383947/what-does-it-mean-to-program-to-an-interface) and [here](https://pt.stackoverflow.com/questions/86484/programar-voltado-para-interface-e-n%C3%A3o-para-a-implementa%C3%A7%C3%A3o-por-qu%C3%AA).

These principles should be balanced with the [KISS][KISS] principle, crucial in the development of any system.

<a name="objetivos"></a>
## Objectives and Evaluation Criteria

This project has the following objectives:

* **O1** - The program must work as specified.
* **O2** - Project and code well organized, namely:
    * Well-thought-out class structure (see section "Project Organization and Class Structure").
    * Properly commented and indented code.
    * No "dead" code that does nothing, such as variables, properties, or methods never used.
    * Project compiles and runs without errors and/or *warnings*.
* **O3** - Project appropriately documented. Documentation must be done with [XML documentation comments][XML].
* **O4** - Git repository must reflect good usage of it, with *commits* from all group members and *commit* messages that follow the best practices for the purpose (as indicated [here](https://chris.beams.io/posts/git-commit/), [here](https://gist.github.com/robertpainsi/b632364184e70900af4ab688decf6f53), [here](https://github.com/erlang/otp/wiki/writing-good-commit-messages) and [here](https://stackoverflow.com/questions/2290016/git-commit-messages-50-72-formatting)). Any binary *assets*, such as images, should be integrated into the repository using Git LFS.
* **O5** - Report in [Markdown][] format (file `README.md`), organized as follows:
    * Project title.
    * Authors' names (first and last) and respective student numbers.
    * Indication of the public Git repository used. This indication is optional, as they may prefer to develop the project in a private repository.
    * Information on who did what in the project. This information is **mandatory** and must reflect the *commits* made in Git.
    * Description of the solution:
        * Solution architecture, with a brief explanation of how the program was organized, indication of the collections used and why, as well as the algorithms used (e.g., for parsing the CSV file, to combine the various queries to the database, etc.).
        * A simple UML class diagram (i.e., without indicating the class members) describing the class structure.
        * A flowchart showing the program operation.
    * Conclusions and material learned.
    * References, including ideas exchanged with colleagues, open-source code reused (e.g., from StackOverflow) and third-party libraries used. They should be as detailed as possible.
* **Note:** The report should be simple and brief, with minimal and sufficient information so that one can have a good idea of what was done. Attention to spelling errors, as they will be taken into account in the final grade.

## References

* <a name="ref1">\[1\]</a> Whitaker, R. B. (2016). **The C# Player's Guide** (3rd Edition). Starbound Software.
* <a name="ref2">\[2\]</a> Albahari, J. (2017). **C# 7.0 in a Nutshell**. O’Reilly Media.
* <a name="ref3">\[3\]</a> Kelly, C. (2016). **Steam Game Data**. Retrieved from <https://data.world/craigkelly/steam-game-data>.
* <a name="ref4">\[4\]</a> Freeman, E., Robson, E., Bates, B., & Sierra, K. (2004). **Head First Design Patterns**. O'Reilly Media.
* <a name="ref5">\[5\]</a> Dorsey, T. (2017). **Doing Visual Studio and .NET Code Documentation Right**. Visual Studio Magazine. Retrieved from <https://visualstudiomagazine.com/articles/2017/02/21/vs-dotnet-code-documentation-tools-roundup.aspx>.

## Licenses

This statement is made available through the [CC BY-NC-SA 4.0][] license.

## Metadata

- Author: [Nuno Fachada]
- Affiliation: [Lusófona University, COPELABS][ULHT]

[Nuno Fachada]:https://github.com/fakenmc
[ULHT]:https://www.ulusofona.pt/
[CC BY-NC-SA 4.0]:https://creativecommons.org/licenses/by-nc-sa/4.0/
[Markdown]:https://guides.github.com/features/mastering-markdown/
[SRP]:https://en.wikipedia.org/wiki/Single_responsibility_principle
[KISS]:https://en.wikipedia.org/wiki/KISS_principle
[CSV]:https://en.wikipedia.org/wiki/Comma-separated_values
[`DateTime`]:https://docs.microsoft.com/dotnet/api/system.datetime
[`int`]:https://docs.microsoft.com/dotnet/api/system.int32
[`Uri`]:https://docs.microsoft.com/dotnet/api/system.uri
[`bool`]:https://docs.microsoft.com/dotnet/api/system.boolean
[`string`]:https://docs.microsoft.com/dotnet/api/system.string
[`List<T>`]:https://docs.microsoft.com/dotnet/api/system.collections.generic.list-1
[`IEnumerable<T>`]:https://docs.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1
[`HashSet<T>`]:https://docs.microsoft.com/dotnet/api/system.collections.generic.hashset-1
[DIP]:https://en.wikipedia.org/wiki/Dependency_inversion_principle
[XML]:https://docs.microsoft.com/dotnet/csharp/codedoc
[`WebClient`]:https://docs.microsoft.com/dotnet/api/system.net.webclient
[`Process`]:https://docs.microsoft.com/dotnet/api/system.diagnostics.process
