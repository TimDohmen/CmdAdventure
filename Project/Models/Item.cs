using System.Collections.Generic;
using ConsoleAdventure.Project.Interfaces;

namespace ConsoleAdventure.Project.Models
{
  public class Item : IItem
  {


    public string Name { get; set; }
    public string Description { get; set; }

    public string GetTemplate()
    {
      return $@"
 {Name}  --- {Description}";

    }

    public Item(string name, string description)
    {
      Name = name;
      Description = description;
    }
  }
}