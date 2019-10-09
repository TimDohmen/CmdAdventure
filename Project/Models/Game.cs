using ConsoleAdventure.Project.Interfaces;

namespace ConsoleAdventure.Project.Models
{
  public class Game : IGame
  {
    public Room CurrentRoom { get; set; }
    public IPlayer CurrentPlayer { get; set; }

    //NOTE Make yo rooms here...
    public void Setup()
    {
      Room start = new Room("First Room", "Dim lit room not a lot going on here");
      Room two = new Room("Second Room", "Bright Room too bright to see");

      start.AddRoomConnection(two, "west");
      two.AddRoomConnection(start, "east");

      Item sword = new Item("Rusty Sword", "Big long sword");
      start.Items.Add(sword);
      CurrentRoom = start;
    }
    public Game()
    {
      CurrentPlayer = new Player();
      Setup();
    }
  }
}