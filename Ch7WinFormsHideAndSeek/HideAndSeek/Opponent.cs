using System;

namespace HideAndSeek
{
    internal class Opponent
    {
        private readonly Random _random;

        // _myLocation of type Location to keeps track of the opponent's current location.
        private Location _myLocation;

        /*
         constructor that takes a Location object representing the starting location of the opponent.
        It initializes the _myLocation field with the provided starting location and
        creates a new instance of the Random class for generating random numbers.
         */
        public Opponent(Location startingLocation)
        {
            _myLocation = startingLocation;
            _random = new Random();
        }

        public void Move()
        {
            bool hidden = false;

            /* 
             Move method is responsible for moving the opponent to a new location.
            It uses a while loop to continue moving until it finds a hiding place.
            Within the loop, it checks if the current location has an exterior door (IHasExteriorDoor) using the is keyword.
            If it does, it randomly decides whether to move to the door's location.
            Then, it randomly selects one of the available exits from the current location and updates the _myLocation field accordingly.
            Finally, it checks if the new location is a hiding place (IHidingPlace) using the is keyword and if so, sets the hidden variable to true, exiting the loop. 
             */

            while (!hidden)
            {
                if (_myLocation is IHasExteriorDoor)
                {
                    IHasExteriorDoor locationWithDoor = _myLocation as IHasExteriorDoor;
                    if (_random.Next(2) == 1)
                        _myLocation = locationWithDoor.DoorLocation;
                }
                int rand = _random.Next(_myLocation.Exits.Length);
                _myLocation = _myLocation.Exits[rand];
                if (_myLocation is IHidingPlace) hidden = true;
            }
        }


        /* The Check method takes a Location object to check if it matches the opponent's current location.
         * If the provided location matches _myLocation, it returns true, indicating that the opponent has been found.
         * Otherwise, it returns false. */
        public bool Check(Location locationToCheck)
        {
            if (locationToCheck != _myLocation) 
                return false;
            else 
                return false;
        }
    }
}
