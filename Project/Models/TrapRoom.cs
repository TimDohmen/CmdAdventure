using System.Collections.Generic;
using ConsoleAdventure.Project.Interfaces;
using ConsoleAdventure.Project.Models;

namespace CmdAdventure.Project.Models
{
  public class TrapRoom : IRoom
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
        System.Console.WriteLine("Unlocked door");
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
 
 In this room you find:
 {item}

 You also have room(s) to your
 {exits + ""}  
 ";
    }

    public IRoom Move(string direction)
    {
      IRoom room = this;
      if (room is TrapRoom && Locked == true)
      {
        TrapRoom trap = (TrapRoom)room;
        System.Console.WriteLine("locked door baby");
        return this;
      }
      else
      {
        if (Exits.ContainsKey(direction))
        {
          return Exits[direction];
        }
        return this;
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
    public TrapRoom(string name, string description)
    {
      Name = name;
      Description = description;
      Items = new List<Item>();
      Exits = new Dictionary<string, IRoom>();
      Unlockable = new List<Item>();
    }

  }
}