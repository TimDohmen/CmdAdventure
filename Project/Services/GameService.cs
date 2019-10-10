using System.Collections.Generic;
using ConsoleAdventure.Project.Interfaces;
using ConsoleAdventure.Project.Models;

namespace ConsoleAdventure.Project
{
  public class GameService : IGameService
  {
    private Game _game { get; set; }

    public List<string> Messages { get; set; }


    public void Go(string direction)
    {
      string from = _game.CurrentRoom.Name;
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
      throw new System.NotImplementedException();
    }

    public void Look()
    {
      Messages.Add(_game.CurrentRoom.GetTemplate());
    }

    public void Quit()
    {
      throw new System.NotImplementedException();
    }
    ///<summary>
    ///Restarts the game 
    ///</summary>
    public void Reset()
    {
      throw new System.NotImplementedException();
    }

    public void Setup(string playerName)
    {
    }
    ///<summary>When taking an item be sure the item is in the current room before adding it to the player inventory, Also don't forget to remove the item from the room it was picked up in</summary>
    public void TakeItem(string itemName)
    {
      if (_game.CurrentRoom.Items.Count == 0)
      {
        Messages.Add("No Items Available");
        return;
      }
      _game.CurrentPlayer.Inventory.AddRange(_game.CurrentRoom.Items);
      _game.CurrentRoom.Items.Clear();
    }
    ///<summary>
    ///No need to Pass a room since Items can only be used in the CurrentRoom
    ///Make sure you validate the item is in the room or player inventory before
    ///being able to use the item
    ///</summary>
    public void UseItem(string itemName)
    {
      throw new System.NotImplementedException();
    }

    public GameService()
    {
      _game = new Game();
      Messages = new List<string>();
    }

  }
}