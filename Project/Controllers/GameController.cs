using System;
using System.Collections.Generic;
using ConsoleAdventure.Project.Interfaces;
using ConsoleAdventure.Project.Models;

namespace ConsoleAdventure.Project.Controllers
{

  public class GameController : IGameController
  {
    private GameService _gameService = new GameService();

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
        case "l":
          Console.Clear();
          _gameService.Look();
          break;
        case "go":
        case "g":
          Console.Clear();
          _gameService.Go(option);
          break;
        case "help":
        case "h":
          _gameService.Help();
          break;
        case "take":
        case "t":
          _gameService.TakeItem(option);
          break;
        case "inventory":
        case "i":
        case "inv":
          _gameService.Inventory();
          break;
        case "use":
        case "u":
          _gameService.UseItem(option);
          break;
        case "q":
        case "quit":
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
              case "y":
                _gameService.Go(command);
                break;
              case "no":
              case "n":
                System.Console.WriteLine("Guards come storming in and arrest you because you are not the real king.");
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