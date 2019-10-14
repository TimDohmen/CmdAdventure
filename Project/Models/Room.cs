using System.Collections.Generic;
using CmdAdventure.Project.Models;
using ConsoleAdventure.Project.Interfaces;

namespace ConsoleAdventure.Project.Models
{
  public class Room : IRoom
  {

    public void Unlock()
    {

    }
    public bool Locked()
    {
      return false;
    }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Item> Items { get; set; }

    public Dictionary<string, IRoom> Exits { get; set; }

    public void AddRoomConnection(IRoom room, string direction)
    {
      Exits.Add(direction, room);
    }

    public void UseItem(Item itemName)
    {

    }

    public IRoom Move(string direction)
    {
      IRoom room = this;
      if (room is TrapRoom)
      {
        TrapRoom trap = (TrapRoom)room;
        System.Console.WriteLine("We hit the lock");
      }
      if (Exits.ContainsKey(direction))
      {
        return Exits[direction];
      }
      return this;

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


      if (Items.Count == 0)
      {
        return $@"    
You are now in {Name}
  {Description}
 
 No items found in this room

 You also have exit(s) to your
   {exits.ToUpper() + "   "}  
 ";
      }
      else
      {
        return $@"    
You are now in {Name}
  {Description}
 
 In this room you find:
      {item}

 You also have exit(s) to your
   {exits.ToUpper() + "   "}  
 ";
      }
    }
    public Room(string name, string description)
    {
      Name = name;
      Description = description;
      Items = new List<Item>();
      Exits = new Dictionary<string, IRoom>();
    }
  }

}