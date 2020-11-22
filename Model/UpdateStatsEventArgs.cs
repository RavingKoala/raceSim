using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;

namespace Model {
	public delegate void UpdateStats(UpdateStatsEventArgs updateStatsEventArgs);
	public class UpdateStatsEventArgs {
		public string ParticipantName { get; set; }
		public StatTypes Stat { get; set; }

		public UpdateStatsEventArgs(string participantName, StatTypes stat) {
			ParticipantName = participantName;
			Stat = stat;
		}
	}
}
