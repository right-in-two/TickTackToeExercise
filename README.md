# Tick Tack Toe sample app

Simple C# application letting two players play against each other. No fancy UI or network support.
Application draws the board and displays the game status. 'Reset game' button lets the users play once again.

The choice for the implementation is that the game engine is a .NET Standard 2.0 library with a simple Windows Forms application as UI.

## Getting Started

Simply clone the git repository, open in Visual Studio 2017 and Build/Debug/Run application.

## Running the tests

Unit test are written in the default unit testing framework (MSTest). After the solution is build, the tests are discovered and ready to run.

## MVP/Code sample vs Production quality code

Programming exercise is about simple TickTackToe game. Still, several production quality code measures are taken:
* game logic is placed inside .NET Standard 2.0 library so that it is reusable and portable
* UI project is bound to game logic interfaces, not hard implementation
* configurable logger is added 
* dependency injection is implemented
* unit tests are added 