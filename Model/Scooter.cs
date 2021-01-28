namespace Model {
	public class Scooter : IEquipment {
		public int Quality { get; set; }
		public int Performance { get; set; }
		public int Speed { get; set; }
		public bool IsBroken { get; set; }

		public Scooter() {
			Quality = 100;
			Performance = 15;
			Speed = 8;
			IsBroken = false;
		}
	}
}
