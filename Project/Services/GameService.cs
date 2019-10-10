using System;
using System.Collections.Generic;
using CmdAdventure.Project.Models;
using ConsoleAdventure.Project.Interfaces;
using ConsoleAdventure.Project.Models;

namespace ConsoleAdventure.Project
{
  public class GameService : IGameService
  {
    private Game _game { get; set; }

    public List<string> Messages { get; set; }

    public bool getCurrentRoom()
    {
      IRoom room = _game.CurrentRoom;
      if (room is ThroneRoom)
      {
        return true;
      }
      else
      {
        return false;
      }
    }
    public void Go(string direction)
    {
      string from = _game.CurrentRoom.Name;

      IRoom room = _game.CurrentRoom;
      if (room is ThroneRoom)
      {
        if (_game.CurrentRoom.Move(direction) == _game.CurrentRoom)
        {
          ThroneRoom thisRoom = (ThroneRoom)room;

          Messages.Add(thisRoom.TakeThrone());
        }
        else
        {
          Messages.Add("You have been knocked out by the guards");
        }
      }

      _game.CurrentRoom = _game.CurrentRoom.Move(direction);


      string to = _game.CurrentRoom.Name;

      if (from == to)
      {
        Messages.Add("Invalid Room");
        return;
      }
      Messages.Add($"Traveled to {to} from {from}");
    }
    public void Help()
    {
      Messages.Add($@"
Type look to see where you are and what option you have

Type go (direction) to travel

Type take (item) to take item from a room

Type use (item) to use item
");
    }

    public void Inventory()
    {
      Messages.Add("Your Inventory : ");
      foreach (Item i in _game.CurrentPlayer.Inventory)
      {
        Messages.Add($"{i.Name}  -   {i.Description}");

      }
      return;
    }

    public void Look()
    {
      Messages.Add($" {_game.CurrentPlayer.Name}  {_game.CurrentRoom.GetTemplate()}");
    }

    public void Quit()
    {
      Environment.Exit(0);
    }
    ///<summary>
    ///Restarts the game 
    ///</summary>
    public void Reset()
    {
      _game.CurrentPlayer.Inventory.Clear();
      _game.Setup();
    }

    public void Setup(string playerName)
    {
      _game.CurrentPlayer.Name = playerName;
    }
    ///<summary>When taking an item be sure the item is in the current room before adding it to the player inventory, Also don't forget to remove the item from the room it was picked up in</summary>
    public void TakeItem(string itemName)
    {
      if (_game.CurrentRoom.Items.Count == 0)
      {
        Messages.Add("No Items Available");
        return;
      }
      for (int i = 0; i < _game.CurrentRoom.Items.Count; i++)
      {
        var item = _game.CurrentRoom.Items[i];
        if (itemName != item.Name.ToLower())
        {
          Messages.Add("Invalid Item");
          return;
        }
        _game.CurrentPlayer.Inventory.Add(item);
        _game.CurrentRoom.Items.Remove(item);
      }


    }
    ///<summary>
    ///No need to Pass a room since Items can only be used in the CurrentRoom
    ///Make sure you validate the item is in the room or player inventory before
    ///being able to use the item
    ///</summary>
    public void UseItem(string itemName)
    {
      IRoom room = _game.CurrentRoom;
      if (room is TrapRoom)
      {
        for (int i = 0; i < _game.CurrentPlayer.Inventory.Count; i++)
        {
          var item = _game.CurrentPlayer.Inventory[i];
          if (itemName != item.Name.ToLower())
          {
            Messages.Add("Invalid Item");
          }
          else
          {
            TrapRoom trap = (TrapRoom)room;
            trap.UseItem(item);
            System.Console.WriteLine("using item in trapservice");
          }
        }
      }

    }

    public GameService()
    {
      _game = new Game();
      Messages = new List<string>();

    }

  }
}