using System;
using System.Collections.Generic;
using ConsoleAdventure.Project.Interfaces;
using ConsoleAdventure.Project.Models;

namespace ConsoleAdventure.Project.Controllers
{

  public class GameController : IGameController
  {
    private GameService _gameService = new GameService();

    //NOTE Makes sure everything is called to finish Setup and Starts the Game loop
    public void Run()
    {
      Console.Clear();
      System.Console.WriteLine("whats your name");
      string player = Console.ReadLine();
      _gameService.Setup(player);
      while (true)
      {
        GetUserInput();
        Print();
      }

    }

    //NOTE Gets the user input, calls the appropriate command, and passes on the option if needed.
    public void GetUserInput()
    {
      Console.WriteLine("What would you like to do?");
      string input = Console.ReadLine().ToLower() + " ";
      string command = input.Substring(0, input.IndexOf(" "));
      string option = input.Substring(input.IndexOf(" ") + 1).Trim();
      //NOTE this will take the user input and parse it into a command and option.
      //IE: take silver key => command = "take" option = "silver key"
      Console.Clear();
      switch (command)
      {

        case "look":
          Console.Clear();
          _gameService.Look();
          break;
        case "go":
          Console.Clear();
          _gameService.Go(option);
          break;
        case "help":
          _gameService.Help();
          break;
        case "take":
          _gameService.TakeItem(option);
          break;
        case "inventory":
          _gameService.Inventory();
          break;
        case "use":
          _gameService.UseItem(option);
          break;
        case "q":
          _gameService.Quit();
          break;

        case "sit":
          if (!_gameService.getCurrentRoom())
          {
            System.Console.WriteLine("Invalid command");
          }
          else
          {
            System.Console.WriteLine("As you are approaching you hear footsteps coming up behind you, do you continue to the Throne?");
            string final = Console.ReadLine();
            switch (final)
            {
              case "yes":
                _gameService.Go(command);
                System.Console.WriteLine("you win");
                Environment.Exit(0);
                break;
              case "no":
                System.Console.WriteLine("guards come storming in and arrest you");
                _gameService.Reset();
                break;
              default:
                System.Console.WriteLine("Please Enter Valid Command");
                break;
            }
          }
          break;




        default:
          System.Console.WriteLine("Enter Valid Command");
          break;


      }

    }

    //NOTE this should print your messages for the game.
    private void Print()
    {

      foreach (string message in _gameService.Messages)
      {
        System.Console.WriteLine(message);

      }
      _gameService.Messages.Clear();
    }

  }
}