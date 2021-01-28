namespace Model {
	public interface IParticipant {
		public string Name { get; set; }
		public int Points { get; set; }
		public IEquipment Equiptment { get; set; }
		public TeamColors TeamColor { get; set; }
	}
}
