using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interfacesHouse
{
    class RoomWithDoor : RoomWithHidingPlace, IHasExteriorDoor
    {
        public RoomWithDoor(string name, string decoration, string hidingPlace, string doorDescription) 
            : base(name, decoration, hidingPlace)
        {
            DoorDescription = doorDescription;
        }

        // exterior door
        public string DoorDescription { get; private set; }
        public Location DoorLocation { get; set; } 
    }
}
