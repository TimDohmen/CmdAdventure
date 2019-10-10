using System.Collections.Generic;
using ConsoleAdventure.Project.Models;

namespace CmdAdventure.Project.Models
{
  public class TrapRoom : Room
  {



    public bool Locked()
    {
      return false;
    }


    public TrapRoom(string name, string description) : base(name, description)
    {
      Name = name;
      Description = description;


    }
  }
}