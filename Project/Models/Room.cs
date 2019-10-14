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

    private Dictionary<IItem, KeyValuePair<string, IRoom>> lockedExits { get; set; }
    private Dictionary<IItem, IItem> unlockableItems { get; set; }
    private Dictionary<IItem, string> descriptionChangers { get; set; }



    // public string Use(IItem item){
    //   //check locked exits
    //     //if contains key then add room from locked to exits
    //   //check unlockcables
    //   //ch

    // }

    public void AddRoomConnection(IRoom room, string direction)
    {
      Exits.Add(direction, room);
    }

    public string UseItem(Item itemName)
    {
      return $"You used {itemName} it has no effect here.";
    }

    public IRoom Move(string direction)
    {
      // IRoom room = this;
      // if (room is TrapRoom)
      // {
      //   TrapRoom trap = (TrapRoom)room;
      //   System.Console.WriteLine("We hit the lock");
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