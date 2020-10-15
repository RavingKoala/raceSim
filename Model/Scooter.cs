using System;
using System.Collections.Generic;
using System.Text;

namespace Model {
    public class Scooter : IEquipment {
        public int Quality { get; set; }
        public int Performance { get; set; }
        public int Speed { get; set; }
        public int IsBroken { get; set; }

		public Scooter() {
			Quality = 100;
			Performance = 2;
			Speed = 15;
			IsBroken = 0;
		}
	}
}
