﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interfacesHouse
{
    class OutsideWithDoor : Outside, IHasExteriorDoor
    {
        public OutsideWithDoor(string name, bool hot, string doorDescription) 
            : base(name, hot)
        {
            DoorDescription = doorDescription;
        }

        // exterior door
        public string DoorDescription { get; private set; }
        public Location DoorLocation { get; set; }

        public override string Description
        {
            get
            {
                return base.Description + " You see " + DoorDescription + ".";
            }
        }
    }
}
