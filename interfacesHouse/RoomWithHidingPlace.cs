using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interfacesHouse
{
    class RoomWithHidingPlace : Room, IHidingPlace
    {
        public RoomWithHidingPlace(string name, string decoration, string hidingPlace) 
            : base(name, decoration)
        {
            HidingPlace = hidingPlace;
        }

        // hiding place
        public string HidingPlace { get; private set; }

        public override string Description
        {
            get
            {
                return base.Description + " Someone could hide " + HidingPlace + ".";
            }
        }
    }
}
