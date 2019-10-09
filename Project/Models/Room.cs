using System.Collections.Generic;
using ConsoleAdventure.Project.Interfaces;

namespace ConsoleAdventure.Project.Models
{
  public class Room : IRoom
  {


    public string Name { get; set; }
    public string Description { get; set; }
    public List<Item> Items { get; set; }

    public Dictionary<string, IRoom> Exits { get; set; }

    public void AddRoomConnection(IRoom room)
    {
      Exits.Add(room.Name, room);
    }

    public IRoom Move(string direction)
    {
      if (Exits.ContainsKey(direction))
      {
        return Exits[direction];
      }
      return this;
    }

    public string GetTemplate()
    {
      return $@"
      
You are now in {Name}
 {Description}
 
 In this room you find:
 {Items}

 You also have
 {Exits}
 ";
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