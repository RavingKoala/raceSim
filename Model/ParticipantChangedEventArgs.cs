using System;

namespace Model {
	public delegate void ParticipantChanged(ParticipantChangedEventArgs ParticipantChangedEventArgs);
	public class ParticipantChangedEventArgs : EventArgs {
		public Track Track { get; }

		public ParticipantChangedEventArgs(Track track) {
			Track = track;
		}
	}
}
