using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interfacesHouse
{
    class OutsideWithHidingPlace : Outside, IHidingPlace
    {
        public OutsideWithHidingPlace(string name, bool hot, string hidingPlace) 
            : base(name, hot)
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
