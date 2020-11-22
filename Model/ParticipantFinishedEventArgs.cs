using System;
using System.Collections.Generic;
using System.Text;

namespace Model {
	public delegate void ParticipantFinished(ParticipantFinishedEventArgs participantFinishedEventArgs);
	public class ParticipantFinishedEventArgs {
		public IParticipant Participant { get; private set; }
		public TimeSpan FinishTime { get; private set; }

		public ParticipantFinishedEventArgs(IParticipant participant, TimeSpan finnishTime) {
			Participant = participant;
			FinishTime = finnishTime;
		}
	}
}
