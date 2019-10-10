using System.Collections.Generic;
using CmdAdventure.Project.Models;
using ConsoleAdventure.Project.Interfaces;

namespace ConsoleAdventure.Project.Models
{
  public class Room : IRoom
  {


    public string Name { get; set; }
    public string Description { get; set; }
    public List<Item> Items { get; set; }

    public Dictionary<string, Room> Exits { get; set; }

    public void AddRoomConnection(Room room, string direction)
    {
      Exits.Add(direction, room);
    }

    public Room Move(string direction)
    {
      // if(Exits is TrapRoom)
      // {

      // }
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

      return $@"    
You are now in {Name}
 {Description}
 
 In this room you find:
 {item}

 You also have room(s) to your
 {exits + ""}  
 ";
    }
    public Room(string name, string description)
    {
      Name = name;
      Description = description;
      Items = new List<Item>();
      Exits = new Dictionary<string, Room>();
    }
  }

}