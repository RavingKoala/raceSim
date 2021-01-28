namespace Model {
	public delegate void ParticipantPassed(ParticipantPassedEventArgs participantPassedEventArgs);
	public class ParticipantPassedEventArgs {
		public IParticipant Participant { get; }

		public ParticipantPassedEventArgs(IParticipant participant) {
			Participant = participant;
		}
	}
}
