using System;
using System.Collections.Generic;
using System.Linq;

namespace Model {
	public class StatLapTime : IParticipantStats {
		public string Name { get; set; }
		public TimeSpan LapTime { get; set; }

		public StatLapTime(IParticipant participant, TimeSpan lapTime) {
			Name = participant.Name;
			LapTime = lapTime;
		}

		public void Add(List<IParticipantStats> list) {
			list.Add(this);
		}

		public string BesteSpeler(List<IParticipantStats> list) {
			Dictionary<string, TimeSpan> lapTimeDict = new Dictionary<string, TimeSpan>();
			foreach (IParticipantStats stat in list) {
				StatLapTime tempStat = (StatLapTime)stat;
				if (!lapTimeDict.ContainsKey(tempStat.Name)) {
					lapTimeDict.Add(tempStat.Name, tempStat.LapTime);
				} else {
					if (tempStat.LapTime.CompareTo(lapTimeDict[tempStat.Name]) > 0) {
						lapTimeDict.Remove(tempStat.Name);
						lapTimeDict.Add(tempStat.Name, tempStat.LapTime);
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
