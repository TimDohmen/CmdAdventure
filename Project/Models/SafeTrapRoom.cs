using System;
using System.Collections.Generic;
using ConsoleAdventure.Project.Interfaces;
using ConsoleAdventure.Project.Models;

namespace CmdAdventure.Project.Models
{
  public class SafeTrapRoom : IRoom
  {
    public void Unlock()
    {
      Locked = false;
      IRoom room = this;
    }
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
        if (itemName.Name.ToString().ToLower() == "key")
        {
          Unlock();
          System.Console.WriteLine("Unlocked door");
        }
      }
      else
      {
        System.Console.WriteLine("You wave your" + itemName.ToString() + " around but it has no use here");
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
      if (Locked == true)
      {
        return $@"    
You are now in {Name}
 {Description}
 
 In this room you find:
 {item}

 You also have room(s) to your
 {exits.ToUpper() + ""}  
 ";
      }
      ///Changes Jailor room based on used sword or not
      else if (Locked == false && Name.ToString().ToLower() == "jailor")
      {
        string template = $@"
Someone really left a mess in here...";

        if (Items.Count != 0)
        {
          template += $@"
  Through the gore you see something shiny 
 {item}";
        }

        template += $@"The only way out is
 { exits + ""}";
        return template;
      }
      else
      {
        return $@"    
You are now in {Name}
 {Description}
 
 In this room you find:
 {item}

 You also have room(s) to your
 {exits + ""}  
 ";
      }
    }
    public IRoom Move(string direction)
    {
      if (Exits.ContainsKey(direction))
      {
        if (Exits[direction].Move("west") == Exits[direction] && Exits[direction].Move("south") == Exits[direction])
        {
          System.Console.WriteLine("You try to open the door but it is locked shut...");
          return this;
        }
        else
        {
          return Exits[direction];
        }
      }
      return this;
    }
    public void AddRoomConnection(IRoom room, string direction)
    {
      Exits.Add(direction, room);
    }
    public void addUnlockable(Item item)
    {
      Unlockable.Add(item);
    }
    public SafeTrapRoom(string name, string description)
    {
      Name = name;
      Description = description;
      Items = new List<Item>();
      Exits = new Dictionary<string, IRoom>();
      Unlockable = new List<Item>();
    }

  }
}