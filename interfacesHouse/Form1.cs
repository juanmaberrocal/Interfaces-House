using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace interfacesHouse
{
    public partial class Form1 : Form
    {
        int NumberOfMoves;

        // players
        Location currentLocation;
        Oponent oponent;

        // rooms
        RoomWithDoor livingRoom;
        RoomWithHidingPlace diningRoom;
        RoomWithDoor kitchen;
        Room stairs;
        RoomWithHidingPlace upstairsHallway;
        RoomWithHidingPlace masterBedroom;
        RoomWithHidingPlace secondBedroom;
        RoomWithHidingPlace bathroom;

        // outsides
        OutsideWithDoor frontYard;
        OutsideWithHidingPlace garden;
        OutsideWithDoor backYard;
        OutsideWithHidingPlace driveway;

        public Form1()
        {
            InitializeComponent();
            CreateObjects();

            // instantiate oponent
            oponent = new Oponent(frontYard);

            ResetGame(false);
        }

        public void CreateObjects()
        {
            // instantiate rooms
            livingRoom = new RoomWithDoor("Living Room", "an antique carpet", "in the closet", "an oak door with a brass knob");
            diningRoom = new RoomWithHidingPlace("Dining Room", "a crystal chandelier", "in the tall armoire");
            kitchen = new RoomWithDoor("Kithen", "stainless steel appliances", "in the cabinet", "a screen door");
            stairs = new Room("Stairs", "a wooden banister");
            upstairsHallway = new RoomWithHidingPlace("Upstairs Hallway", "a picture of a dog", "in the closet");
            masterBedroom = new RoomWithHidingPlace("Master Bedroom", "a large bed", "under the bed");
            secondBedroom = new RoomWithHidingPlace("Second Bedroom", "a small bed", "under the bed");
            bathroom = new RoomWithHidingPlace("Bathroom", "a sink and a toilet", "in the shower");

            frontYard = new OutsideWithDoor("Front Yard", false, "an oak door with a brass knob");
            garden = new OutsideWithHidingPlace("Garden", false, "in the shed");
            backYard = new OutsideWithDoor("Back Yard", true, "a screen door");
            driveway = new OutsideWithHidingPlace("Driveway", false, "in the garage");

            // set exits
            livingRoom.Exits = new Location[] { diningRoom, stairs };
            diningRoom.Exits = new Location[] { livingRoom, kitchen };
            kitchen.Exits = new Location[] { diningRoom };
            stairs.Exits = new Location[] { livingRoom, upstairsHallway };
            upstairsHallway.Exits = new Location[] { stairs, masterBedroom, secondBedroom, bathroom };
            masterBedroom.Exits = new Location[] { upstairsHallway };
            secondBedroom.Exits = new Location[] { upstairsHallway };
            bathroom.Exits = new Location[] { upstairsHallway };

            frontYard.Exits = new Location[] { garden, backYard, driveway };
            garden.Exits = new Location[] { frontYard, backYard };
            backYard.Exits = new Location[] { frontYard, garden, driveway };
            driveway.Exits = new Location[] { frontYard, backYard };

            // outside doors
            livingRoom.DoorLocation = frontYard;
            frontYard.DoorLocation = livingRoom;

            kitchen.DoorLocation = backYard;
            backYard.DoorLocation = kitchen;
        }

        private void RedrawForm()
        {
            description.Text = currentLocation.Description + 
                "\r\n(move #" + NumberOfMoves + ")";

            exits.Items.Clear();
            for (int i = 0; i < currentLocation.Exits.Length; i++)
                exits.Items.Add(currentLocation.Exits[i].Name);
            exits.SelectedIndex = 0;

            if (currentLocation is IHasExteriorDoor)
                goThroughDoor.Visible = true;
            else
                goThroughDoor.Visible = false;

            if (currentLocation is IHidingPlace)
            {
                IHidingPlace hidingLocation = currentLocation as IHidingPlace;
                check.Text = "Check " + hidingLocation.HidingPlace;
                check.Visible = true;
            }
            else
                check.Visible = false;
        }

        private void ResetGame(bool displayMessage)
        {
            if (displayMessage)
            {
                MessageBox.Show("You found me in " + NumberOfMoves + " moves");
                IHidingPlace hidingLocation = currentLocation as IHidingPlace;
                description.Text = "You found your oponent in " + NumberOfMoves +
                    " moves. He was hiding in " + hidingLocation.HidingPlace + ".";
            }

            NumberOfMoves = 0;
            hide.Visible = true;
            goHere.Visible = false;
            check.Visible = false;
            goThroughDoor.Visible = false;
            exits.Visible = false;
        }

        private void MoveToANewLocation(Location newLocation)
        {
            NumberOfMoves++;
            currentLocation = newLocation;
            RedrawForm();
        }

        private void goHere_Click(object sender, EventArgs e)
        {
            MoveToANewLocation(currentLocation.Exits[exits.SelectedIndex]);
        }

        private void goThroughDoor_Click(object sender, EventArgs e)
        {
            IHasExteriorDoor exteriorLocation = currentLocation as IHasExteriorDoor;
            MoveToANewLocation(exteriorLocation.DoorLocation);
        }

        private void check_Click(object sender, EventArgs e)
        {
            NumberOfMoves++;
            if (oponent.Check(currentLocation))
                ResetGame(true);
            else
                MessageBox.Show("Not here");
                RedrawForm();
        }

        private void hide_Click(object sender, EventArgs e)
        {
            hide.Visible = false;

            for (int i = 0; i < 10; i++)
            {
                oponent.Move();
                description.Text = i + "...";
                Application.DoEvents();
                System.Threading.Thread.Sleep(200);
            }

            description.Text = "Ready or not, here I come.";
            Application.DoEvents();
            System.Threading.Thread.Sleep(500);

            goHere.Visible = true;
            exits.Visible = true;
            MoveToANewLocation(livingRoom);
        }
    }
}
