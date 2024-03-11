# Recursive Inventory

## Introduction

Groups are tasked with implementing in Unity a multi-level inventory manager for later use in an RPG game.

Projects must be developed by **groups of 2 to 3 students**.

## Features

### Summary

The application should (1) open a file indicating the contents of the inventory, (2) display the inventory on the screen, (3) allow the user to select an item to display more information about it, and (4) add and remove items.

### Details

#### Items

Items have five mandatory characteristics (they may have more for game design and visualization purposes, although that is not mandatory):

* _Name_
* _FileCode_
* _Type_
* _Weight_
* _Value_

There are two general types of items:

* _Action items_
* _Container items_
  * In addition to the previous three characteristics, _container items_ have a _MaxWeight_ characteristic, indicating the maximum weight they can carry (in addition to their own weight).

##### _Action Items_

The program should recognize the following _action items_:

| Name                  | FileCode          | Type     | Weight | Value |
|-----------------------|-------------------|----------|-------:|------:|
| Sword                 | `sword`           | Weapon   | 5.5    | 10.3  |
| Axe                   | `axe`             | Weapon   | 8.9    | 13.7  |
| Shield                | `shield`          | Apparel  | 7.0    | 8.6   |
| Ring of Protection    | `ring_protection` | Apparel  | 0.3    | 3.9   |
| Scroll of Attack      | `scroll_attack`   | Scroll   | 0.4    | 2.9   |
| Scroll of Defense     | `scroll_defense`  | Scroll   | 0.4    | 2.8   |
| Health Potion         | `potion_health`   | Consumable | 0.5  | 4.8   |
| Food                  | `food`            | Consumable | 0.8  | 3.5   |
| Water                 | `water`           | Consumable | 0.5  | 1.0   |
| Coin                  | `coin`            | Coin     | 0.1    | 1.0   |

##### _Container Items_

The program should recognize the following _container items_:

| Name       | FileCode | Type     | Weight | Value | MaxWeight |
|------------|----------|----------|-------:|------:|----------:|
| Pouch      | `pouch`  | Container | 0.2    | 0.6   | 8.0       |
| Bag        | `bag`    | Container | 0.8    | 1.9   | 16.5      |
| Backpack   | `backpack` | Container | 2.3  | 4.9   | 24.8      |

#### Opening File

The application starts by asking the user for the file describing the inventory. These files must have the extension `rpginv`. For example, `coisas.rpginv`, `stuff.rpginv`, or `junk.rpginv` are valid names for files describing inventories.

To simplify, the application only looks for files in the folder `inventory` (all lowercase), located on the current user's _Desktop_/workspace. Note that this search must work on any computer and operating system. The application presents the user with a scrollable list of the existing `rpginv` files in that folder, and the user selects one of them. Those who wish to develop a more advanced application, with a bonus in the grade, may try using an asset like [UnitySimpleFileBrowser] that can open `rpginv` files anywhere on the disk.

The format of the files is exemplified by the following self-explanatory sample:

```text
sword
sword
backpack
    potion_health
    food
    pouch
        coin
        coin
        coin
food
scroll_defense
scroll_attack
pouch
    water
    water
    potion_health
    ring_protection
shield
```

The [`examples`](examples) folder in this repository contains some example files. Files with names ending in _invalid_ indicate invalid files, either because they contain invalid contents, or because some non-container item contains other items, or because there's excessive weight in one of the specified container items.

The UI must clearly indicate the specific error in each case and should not _crash_. You should test your project with all these files.

#### Inventory Presentation and Interaction

After opening a valid file, the application displays the zero level of the inventory (there should be an indication _Top Level_ somewhere in the UI) in the form of a scrollable list, showing the name, weight, and value of each item, and optionally a _sprite_, 3D model, or color representing it. The _container items_ (but not their contents) must be listed similarly, with their respective total weight and value corresponding to the **total** weight and value of the _container item_ and its contents.

If the user clicks on an _action item_, a detailed information panel about it should be displayed, indicating the name, type, weight, and value, as well as the _sprite_, 3D model or color chosen to represent that item (but in a larger format than presented in the scrollable list). The panel can be closed by the user, with the application returning to display the current inventory level.

If the user clicks on a _container item_, the next level of the inventory is presented in the form of a new scrollable list with all the items contained in that _container item_, virtually identical to the zero level with the following differences:

* Instead of the indication _Top Level_, the name of the _container item_ containing the listed items should be indicated.
* There should be a button to go back to the level above.

If there are _container items_ within other _container items_, the user can descend several levels, and the back button takes the user to the level immediately above the current one. At the zero level (_Top Level_), this button should be deactivated, as there are no more levels above.

The user must be able to add and/or remove items at each inventory level. When adding items, the user must be able to choose them from a list of all known items. Removing a _container item_, besides the _container item_ itself, also removes all the items contained within it.

The way the UI is implemented must follow good game design rules and should be clear to any user on how the system works.

#### Future Functionality

The application's UI should have 5 buttons, clearly marked from 1 to 5. When clicking on one of these buttons, a nearly empty panel should appear, only with the message "For the future 1" for button 1, "For the future 2" for button 2, etc.

The panel can be closed by the user, returning the application to display the map.

This functionality will be implemented individually for the personal evaluation _live coding_ session, so it's essential to successfully complete this project.

Here are some example queries that may arise in that evaluation session:

- The total weight of all items in the inventory.
- The total number of _Action items_ (i.e., whose type is not `Container`) in the inventory.
- The number of _Container items_ containing more than 2 _Weapons_ directly within them.
- The average value of _Container items_ with more than 2 _Weapon items_ contained within them, possibly including those contained in sub-containers.
- The total value of all _Coins_ directly placed in _pouches_.

#### Others

* The application should not _crash_ with exceptions but should elegantly show the user possible errors that may occur (for example, reading the file if it has an invalid format, or there are _container items_ with weight exceeding their _MaxWeight_).
* You should test the application with files containing hundreds or even thousands of items, ensuring that it works without performance issues.

## Project Organization and Class Structure

The project must be properly organized, following by making use of classes, `structs`, and/or enumerations, as appropriate. Each type (i.e., class, `struct`, or enumeration) should be placed in a file with the same name. For example, a class called `Item` should be placed in the file `Item.cs`.

In turn, the choice of the collection or collections to use should also be appropriate for the problem.

The class structure should be well thought out and logically organized, making use of *design patterns* when appropriate. In particular, the project should follow an [MVC] approach and be developed considering the principles of object-oriented programming, such as the [SOLID] principles, among others. These principles must be balanced with the [KISS] principle, crucial in the development of any application.

Lastly, adding new items should be very simple, preferably **without the need to change or add code**.

## Objectives and Evaluation Criteria

This project has the following objectives:

* **O1** - The program must work as specified.
* **O2** - The project and code should be well-organized, specifically:
  * A well-thought-out class structure (see the section "Project Organization and Class Structure").
  * Absence of "dead" code that does nothing, such as unused variables, properties, or methods.
  * The project compiles and runs without errors and/or _warnings_.
* **O3** - The code must be properly indented, commented, and documented. Documentation should be done with [XML documentation comments][XML].
* **O4** - The Git repository must reflect good use of it, with _commits_ from all group members and _commit_ messages that follow the best practices for the purpose (as indicated [here](https://chris.beams.io/posts/git-commit/), [here](https://gist.github.com/robertpainsi/b632364184e70900af4ab688decf6f53), [here](https://github.com/erlang/otp/wiki/writing-good-commit-messages) and [here](https://stackoverflow.com/questions/2290016/git-commit-messages-50-72-formatting)). Any binary _assets_, such as images, must be integrated into the repository in Git LFS mode (note that this last point is **very important**).
* **O5** - The report in [Markdown] format (file `README.md`), organized as follows:
  * The project title.
  * Authorship:
    * Names of the authors (first and last) and their respective student numbers.
    * Information on who did what in the project. This information is **mandatory** and must reflect the _commits_ made in Git.
  * The legend of the _items_, i.e., which object on the screen (_sprite_, 3D model, or color) represents each _item_.
  * Solution Architecture:
    * Description of the solution, with a brief explanation of how the program was organized, indication of the _design patterns_ used and why, as well as considerations regarding the [SOLID] principles.
    * A simple UML class diagram (i.e., without indicating the class members) describing the class structure (**mandatory**).
  * References, including idea exchanges with colleagues, suggestions given by generative AIs (e.g., ChatGPT), reused open-source code (e.g., from StackOverflow), and third-party libraries used. They should be as detailed as possible.
  * **Note:** The report should be simple and brief, with minimal and sufficient information to get a good understanding of what was done. Attention to spelling mistakes and correct [Markdown] formatting, as both will be taken into account in the final grade.

## References

* \[1\] Whitaker, R. B. (2022). **The C# Player's Guide** (5th Edition). Starbound Software.
* \[2\] Albahari, J., & Johannsen, E. (2020). **C# 8.0 in a Nutshell**. O’Reilly Media.
* \[3\] Freeman, E., Robson, E., Bates, B., & Sierra, K. (2020). **Head First Design Patterns** (2nd edition). O'Reilly Media.
* \[4\] Dorsey, T. (2017). **Doing Visual Studio and .NET Code Documentation Right**. Visual Studio Magazine. Retrieved from <https://visualstudiomagazine.com/articles/2017/02/21/vs-dotnet-code-documentation-tools-roundup.aspx>.

## Licenses

* This assignment is made available through the [CC BY-NC-SA 4.0] license.
* The example code is made available through the [MIT] license.

## Metadata

- Author: [Nuno Fachada]
- Affiliation: [Lusófona University, COPELABS][ULHT]

[CC BY-NC-SA 4.0]:https://creativecommons.org/licenses/by-nc-sa/4.0/
[MIT]:http://opensource.org/licenses/MIT
[Nuno Fachada]:https://github.com/nunofachada
[ULHT]:https://www.ulusofona.pt/
[Markdown]:https://guides.github.com/features/mastering-markdown/
[SOLID]:https://en.wikipedia.org/wiki/SOLID
[KISS]:https://en.wikipedia.org/wiki/KISS_principle
[XML]:https://docs.microsoft.com/dotnet/csharp/codedoc
[UnitySimpleFileBrowser]:https://github.com/yasirkula/UnitySimpleFileBrowser
[MVC]:https://www.geeksforgeeks.org/mvc-design-pattern/
