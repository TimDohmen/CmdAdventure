using ConsoleAdventure.Project.Models;

namespace ConsoleAdventure.Project.Interfaces
{
  public interface IGame
  {
    Room CurrentRoom { get; set; }
    Player CurrentPlayer { get; set; }

    void Setup();
  }
}