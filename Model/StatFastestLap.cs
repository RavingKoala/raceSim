using System;
using System.Collections.Generic;
using System.Linq;

namespace Model {
	public class StatFastestLap : IParticipantStats {
		public string Name { get; set; }
		public TimeSpan FastestLapTime { get; set; }

		public StatFastestLap(IParticipant participant, TimeSpan lapTime) {
			Name = participant.Name;
			FastestLapTime = lapTime;
		}

		public void Add(List<IParticipantStats> list) {
			StatFastestLap tempClass = null;
			foreach (IParticipantStats Stat in list.ToList()) {
				if (Stat.Name.Equals(this.Name)) {
					tempClass = (StatFastestLap)Stat;
					if (FastestLapTime.CompareTo(tempClass.FastestLapTime) > 0) {
						list.Remove(tempClass);
						tempClass = null;
					}
				}
			}
			if (tempClass == null) {
				list.Add(this);
				tempClass = this;
			}
		}

		public string BesteSpeler(List<IParticipantStats> list) {
			Dictionary<string, TimeSpan> lapTimeDict = new Dictionary<string, TimeSpan>();
			foreach (IParticipantStats stat in list.ToList()) {
				StatFastestLap tempStat = (StatFastestLap)stat;
				if (!lapTimeDict.ContainsKey(tempStat.Name)) {
					lapTimeDict.Add(tempStat.Name, tempStat.FastestLapTime);
				} else {
					if (tempStat.FastestLapTime.CompareTo(lapTimeDict[tempStat.Name]) > 0) {
						lapTimeDict.Remove(tempStat.Name);
						lapTimeDict.Add(tempStat.Name, tempStat.FastestLapTime);
					}
				}
			}
			string returnName = lapTimeDict.Count > 0 ? lapTimeDict.Keys.First() : null;
			foreach (string name in lapTimeDict?.Keys) {
				if (lapTimeDict[name].CompareTo(lapTimeDict[returnName]) > 0) {
					returnName = name;
				}
			}
			return returnName != null ? returnName : "";
		}
	}
}