using System;
using System.ComponentModel;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HideAndSeek
{
    public partial class Form1 : Form
    {

        private int _moves;

        private Location _currentLocation;

        private RoomWithDoor _livingRoom;
        private RoomWithDoor _kitchen;

        private Room _stairs;

        private RoomWithHidingPlace _diningRoom;
        private RoomWithHidingPlace _hallway;
        private RoomWithHidingPlace _bathroom;
        private RoomWithHidingPlace _masterBedroom;
        private RoomWithHidingPlace _secondBedroom;

        private OutsideWithDoor _frontYard;
        private OutsideWithDoor _backYard;

        private OutsideWithHidingPlace _garden;
        private OutsideWithHidingPlace _driveway;

        private readonly Opponent _opponent;

        public Form1()
        {
            InitializeComponent();
            CreateObjects();
            _opponent = new Opponent(_frontYard);
            ResetGame(false);
        }

        private void MoveToANewLocation(Location newLocation)
        {
            _moves++;
            _currentLocation = newLocation;
            RedrawForm();
        }

        private void RedrawForm()
        {
            exits.Items.Clear();

            for (int i = 0; i < _currentLocation.Exits.Length; i++)
                exits.Items.Add(_currentLocation.Exits[i].Name);
            exits.SelectedIndex = 0;
            description.Text = _currentLocation.Description + "\r\n(move #" + _moves + ")";
            if (_currentLocation is IHidingPlace)
            {
                IHidingPlace hidingPlace = _currentLocation as IHidingPlace;
                check.Text = "Check " + hidingPlace.HidingPlaceName;
                check.Visible = true;
            }
            else
                check.Visible = false;
            if (_currentLocation is IHasExteriorDoor)
                goThroughTheDoor.Visible = true;
            else
                goThroughTheDoor.Visible = false;
        }

        private void CreateObjects()
        {
            _livingRoom = new RoomWithDoor("Living Room", "an antique carpet",
          "inside the closet", "an oak door with a brass handle");
            _diningRoom = new RoomWithHidingPlace("Dining Room", "a crystal chandelier",
                       "in the tall armoire");
            _kitchen = new RoomWithDoor("Kitchen", "stainless steel appliances",
                      "in the cabinet", "a screen door");
            _stairs = new Room("Stairs", "a wooden bannister");
            _hallway = new RoomWithHidingPlace("Upstairs Hallway", "a picture of a dog",
                      "in the closet");
            _bathroom = new RoomWithHidingPlace("Bathroom", "a sink and a toilet",
                      "in the shower");
            _masterBedroom = new RoomWithHidingPlace("Master Bedroom", "a large bed",
                      "under the bed");
            _secondBedroom = new RoomWithHidingPlace("Second Bedroom", "a small bed",
                      "under the bed");

            _frontYard = new OutsideWithDoor("Front Yard", false, "a heavy-looking oak door");
            _backYard = new OutsideWithDoor("Back Yard", true, "a screen door");
            _garden = new OutsideWithHidingPlace("Garden", false, "inside the shed");
            _driveway = new OutsideWithHidingPlace("Driveway", true, "in the garage");

            _diningRoom.Exits = new Location[] { _livingRoom, _kitchen };
            _livingRoom.Exits = new Location[] { _diningRoom, _stairs };
            _kitchen.Exits = new Location[] { _diningRoom };
            _stairs.Exits = new Location[] { _livingRoom, _hallway };
            _hallway.Exits = new Location[] { _stairs, _bathroom, _masterBedroom, _secondBedroom };
            _bathroom.Exits = new Location[] { _hallway };
            _masterBedroom.Exits = new Location[] { _hallway };
            _secondBedroom.Exits = new Location[] { _hallway };
            _frontYard.Exits = new Location[] { _backYard, _garden, _driveway };
            _backYard.Exits = new Location[] { _frontYard, _garden, _driveway };
            _garden.Exits = new Location[] { _backYard, _frontYard };
            _driveway.Exits = new Location[] { _backYard, _frontYard };

            _livingRoom.DoorLocation = _frontYard;
            _frontYard.DoorLocation = _livingRoom;

            _kitchen.DoorLocation = _backYard;
            _backYard.DoorLocation = _kitchen;
        }

        private void ResetGame(bool displayMessage)
        {
            if (displayMessage)
            {
                MessageBox.Show("You found me in " + _moves + " moves!");
                IHidingPlace foundLocation = _currentLocation as IHidingPlace;
                description.Text = "You found your opponent in " + _moves
                      + " moves! He was hiding " + foundLocation.HidingPlaceName + ".";
            }
            _moves = 0;
            hide.Visible = true;
            goHere.Visible = false;
            check.Visible = false;
            goThroughTheDoor.Visible = false;
            exits.Visible = false;
        }

        private void Check_Click(object sender, EventArgs e)
        {
            _moves++;
            if (_opponent.Check(_currentLocation)) 
                ResetGame(true);
            else 
                RedrawForm();
        }

        private void Hide_Click(object sender, EventArgs e)
        {
            hide.Visible = false;

            for (int i = 1; i <= 10; i++)
            {
                _opponent.Move();
                description.Text = i + "... ";
                Application.DoEvents();
                System.Threading.Thread.Sleep(200);
            }

            description.Text = "Ready or not, here I come!";
            Application.DoEvents();
            System.Threading.Thread.Sleep(500);

            goHere.Visible = true;
            exits.Visible = true;
            MoveToANewLocation(_livingRoom);
        }

        private void GoHere_Click(object sender, EventArgs e)
        {
            MoveToANewLocation(_currentLocation.Exits[exits.SelectedIndex]);
        }

        private void GoThroughTheDoor_Click(object sender, EventArgs e)
        {
            IHasExteriorDoor hasDoor = _currentLocation as IHasExteriorDoor;
            MoveToANewLocation(hasDoor.DoorLocation);
        }
    }
}
