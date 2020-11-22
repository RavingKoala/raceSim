using System;
using System.Collections.Generic;
using System.Text;

namespace Model {
	public delegate void ParticipantLapped(ParticipantLappedEventArgs ParticipantLappedEventArgs);
	public class ParticipantLappedEventArgs : EventArgs {
		public IParticipant Participant { get; private set; }
		public TimeSpan LapTime { get; private set; }

		public ParticipantLappedEventArgs(IParticipant participant, TimeSpan lapTime) {
			Participant = participant;
			LapTime = lapTime;
		}
	}
}
