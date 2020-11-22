using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model {
	public class StatFinishTime : IParticipantStats {
		public string Name { get; set; }
		public TimeSpan FinishTime { get; set; }

		public StatFinishTime(IParticipant participant, TimeSpan finishTime) {
			Name = participant.Name;
			FinishTime = finishTime;
		}

		public void Add(List<IParticipantStats> list) {
			list.Add(this);
		}

		public string BesteSpeler(List<IParticipantStats> list) {
			Dictionary<string, TimeSpan> finishTimeDict = new Dictionary<string, TimeSpan>();
			foreach (IParticipantStats stat in list) {
				StatFinishTime tempStat = (StatFinishTime)stat;
				if (!finishTimeDict.ContainsKey(tempStat.Name)) {
					finishTimeDict.Add(tempStat.Name, tempStat.FinishTime);
				} else {
					if (tempStat.FinishTime.CompareTo(finishTimeDict[tempStat.Name]) < 0) {
						finishTimeDict.Remove(tempStat.Name);
						finishTimeDict.Add(tempStat.Name, tempStat.FinishTime);
					}
				}
			}
			string returnName = finishTimeDict.Count > 0 ? finishTimeDict.Keys.First() : null;
			foreach (string name in finishTimeDict?.Keys) {
				if (finishTimeDict[name].CompareTo(finishTimeDict[returnName]) < 0) {
					returnName = name;
				}
			}
			return returnName != null ? returnName : "";
		}
	}
}
