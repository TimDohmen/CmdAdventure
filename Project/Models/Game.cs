using CmdAdventure.Project.Models;
using ConsoleAdventure.Project.Interfaces;

namespace ConsoleAdventure.Project.Models
{
  public class Game : IGame
  {
    public IRoom CurrentRoom { get; set; }
    public Player CurrentPlayer { get; set; }

    //NOTE Make yo rooms here...
    public void Setup()
    {
      IRoom start = new Room("An Unknown Room", "You come to your senses in a pile of your own vomit.");
      IRoom two = new Room("Outside", "To your north you hear a noise but to your west you see something mysterious");
      IRoom three = new TrapRoom("Westeros Bar", "The bar is very crowded but there is an open spot off to the side");
      IRoom four = new Room("Hidden Tunnel", "There is ancient markings on the walls and lit torchs leading down a corridor.");
      IRoom five = new Room("Courtyard", "You see large arch doors to your south but a smaller normal door to your east.");
      IRoom six = new Room("Jailer", "You walk into the room to see a group of guards sitting around a table looking up at you.");
      IRoom seven = new Room("Dark Hallway", $@"
You enter the arch doors to a long dark hallway. 
Do you dare go north?");
      IRoom eight = new Room("Throne Room", "There is the Iron Throne is with nobody to claim it... Do you dare sit in the throne?");

      start.AddRoomConnection(two, "west");
      two.AddRoomConnection(start, "east");

      two.AddRoomConnection(three, "north");
      three.AddRoomConnection(two, "south");

      two.AddRoomConnection(four, "west");
      four.AddRoomConnection(two, "east");

      four.AddRoomConnection(five, "north");
      five.AddRoomConnection(four, "south");

      five.AddRoomConnection(six, "west");
      six.AddRoomConnection(five, "east");

      five.AddRoomConnection(seven, "north");
      seven.AddRoomConnection(five, "south");

      seven.AddRoomConnection(eight, "north");
      eight.AddRoomConnection(seven, "south");



      Item sword = new Item("Sword", "Big long sword");
      Item drink = new Item("Ale", "Biggest container of beer you've ever seen.");
      Item torch = new Item("Torch", "Used to light even the darkest of places.");
      Item crown = new Item("Crown", "Fit for a king");

      start.Items.Add(sword);
      three.Items.Add(drink);
      four.Items.Add(torch);



      CurrentRoom = start;
    }
    public Game()
    {
      CurrentPlayer = new Player("");
      Setup();
    }
  }
}