using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interfacesHouse
{
    class Oponent
    {
        public Oponent(Location location)
        {
            myLocation = location;
            random = new Random();
        }

        private Location myLocation;
        private Random random;

        public void Move()
        {
            if (myLocation is IHasExteriorDoor)
                if (random.Next(2) == 1)
                {
                    IHasExteriorDoor outsideLocation = myLocation as IHasExteriorDoor;
                    myLocation = outsideLocation.DoorLocation;
                }

            myLocation = myLocation.Exits[random.Next(myLocation.Exits.Length)];

            if (!(myLocation is IHidingPlace))
                Move();
        }

        public bool Check(Location location)
        {
            return location == myLocation;
        }
    }
}
