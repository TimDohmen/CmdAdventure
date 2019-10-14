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
          Messages.Add(thisRoom.TakeThrone(_game.CurrentPlayer.Name));
        }
        else
        {
          Messages.Add($@"
  The guards storm in and immediately see you are an imposter! They knock you out...
  
    You come to your senses back in the unknown room... once again in a pile of your own vomit");
          Reset();
        }
        return;
      }
      _game.CurrentRoom = _game.CurrentRoom.Move(direction);
      string to = _game.CurrentRoom.Name;

      if (from == to)
      {
        Messages.Add("Area not available currently");
        return;
      }
      Messages.Add($"Traveled to {to} from {from}");
      Look();
    }
    public void Help()
    {
      Messages.Add($@"
          Help Menu
Type -
       look to see where you are and what options you have

       go direction to travel

       take itemName to take item from a room

       inventory to view items in inventory

       use itemName to use item

       help to revisit this menu.
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
      Messages.Add($@" {_game.CurrentPlayer.Name}'s Adventure  
      {_game.CurrentRoom.GetTemplate()}");
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
        if (item.Name.ToLower() == itemName)
        {
          Messages.Add($"Picked up {item.Name}");
          _game.CurrentPlayer.Inventory.Add(item);
          _game.CurrentRoom.Items.Remove(item);
          _game.CurrentRoom.Items.Clear();
        }
        return;
      }
      Messages.Add("Invalid Item");
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
        // _game.CurrentPlayer.Inventory.Find(itemName);
        // var itemm = _game.CurrentPlayer.Inventory.IndexOf(itemName as Item);
        for (int i = 0; i < _game.CurrentPlayer.Inventory.Count; i++)
        {
          var item = _game.CurrentPlayer.Inventory[i];
          if (item.Name.ToLower() == itemName)
          {
            TrapRoom trap = (TrapRoom)room;
            trap.UseItem(item);
            Messages.Add(trap.UseItem(item));
            // _game.CurrentPlayer.Inventory.Remove(item);
            Messages.Add($"Used your {item.Name}");
            return;
          }
        }
        {
          Messages.Add("Invalid Item");
        }
      }
      else if (room is SafeTrapRoom)
      {
        for (int i = 0; i < _game.CurrentPlayer.Inventory.Count; i++)
        {
          var item = _game.CurrentPlayer.Inventory[i];
          if (item.Name.ToLower() == itemName)
          {
            SafeTrapRoom trap = (SafeTrapRoom)room;
            Messages.Add(trap.UseItem(item));
            // _game.CurrentPlayer.Inventory.Remove(item);
            Messages.Add($"Used your {item.Name}");
            return;
          }
        }
        {
          Messages.Add("Invalid Item");
        }
      }
      else if (room is ThroneRoom)
      {
        for (int i = 0; i < _game.CurrentPlayer.Inventory.Count; i++)
        {
          var item = _game.CurrentPlayer.Inventory[i];
          if (item.Name.ToLower() == itemName)
          {
            ThroneRoom winningRoom = (ThroneRoom)room;
            Messages.Add(winningRoom.UseItem(item));
            return;
          }
        }
        {
          Messages.Add("Invalid Item");
        }
      }
      else
      {
        for (int i = 0; i < _game.CurrentPlayer.Inventory.Count; i++)
        {
          var item = _game.CurrentPlayer.Inventory[i];
          if (item.Name.ToLower() == itemName)
          {
            Messages.Add($"Used your {item.Name} but it has little effect here.");
            return;
          }
        }
        {
          Messages.Add("Invalid Item");
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