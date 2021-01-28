namespace Model {
	public delegate void ParticipantEquipmentBroke(ParticipantEquipmentBrokeEventArgs participantEquipmentBrokeEventArgs);
	public class ParticipantEquipmentBrokeEventArgs {
		public IParticipant Participant;

		public ParticipantEquipmentBrokeEventArgs(IParticipant participant) {
			Participant = participant;
		}
	}
}
