using System;
using System.Collections.Generic;
using System.Text;

namespace Model {
    public class Car : IEquipment {
        public int quality;
        public int performance;
        public int speed;
        public int isBroken;
        public int Quality { get => quality; set { quality = value; } }
        public int Performance { get => performance; set { performance = value; } }
        public int Speed { get => speed; set { speed= value; } }
        public int IsBroken { get => isBroken; set { isBroken= value; } }
    }
}
