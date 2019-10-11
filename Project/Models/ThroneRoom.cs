using System;
using System.Collections.Generic;
using ConsoleAdventure.Project.Interfaces;
using ConsoleAdventure.Project.Models;

namespace CmdAdventure.Project.Models
{
  public class ThroneRoom : IRoom
  {

    public string Name { get; set; }
    public string Description { get; set; }
    public List<Item> Items { get; set; }
    public Dictionary<string, IRoom> Exits { get; set; }
    public List<Item> Unlockable { get; set; }

    public bool Locked { get; set; } = true;

    public void UseItem(Item itemName)
    {
      if (Unlockable.Contains(itemName))
      {
        Locked = false;
        System.Console.WriteLine("You place the cold gold crown upon your head");
      }
      else
      {
        System.Console.WriteLine("Item has no use here");
      }

    }
    public string GetTemplate()
    {
      string item = "";
      Items.ForEach(i => item += $"\n{i.Name }");
      string exits = "";
      foreach (var exit in Exits)
      {
        exits += exit.Key + " ";
      }

      return $@"    
You are now in {Name}
 {Description}
 ";



    }


    public string TakeThrone(string name)
    {
      return "You sit upon the throne and hear the people start chanting" + name + ". . ." + name + " . . . " + name;
    }
    public IRoom Move(string direction)
    {
      IRoom room = this;
      if (room is ThroneRoom && direction == "sit")
      {
        if (Locked == false)
        {
          return this;
        }
        return Exits["secret"];
      }
      else
      {
        return Exits["secret"];
      }

    }
    public void AddRoomConnection(IRoom room, string direction)
    {
      Exits.Add(direction, room);
    }
    public void addUnlockable(Item item)
    {
      Unlockable.Add(item);
    }
    public ThroneRoom(string name, string description)
    {
      Name = name;
      Description = description;
      Items = new List<Item>();
      Exits = new Dictionary<string, IRoom>();
      Unlockable = new List<Item>();
    }

  }
}