using System.Collections.Generic;
using System.Linq;

namespace Model {
	public class StatPoints : IParticipantStats {
		public string Name { get; set; }
		public int Points { get; set; }

		public StatPoints(IParticipant participant, int points) {
			Name = participant.Name;
			Points = points;
		}

		public void Add(List<IParticipantStats> list) {
			StatPoints tempClass = null;
			foreach (IParticipantStats Stat in list) {
				if (Stat.Name.Equals(this.Name)) {
					tempClass = (StatPoints)Stat;
					tempClass.Points += Points;
				}
			}
			if (tempClass == null) {
				list.Add(this);
			}
		}

		public string BesteSpeler(List<IParticipantStats> list) {
			Dictionary<string, int> pointsDict = new Dictionary<string, int>();
			foreach (IParticipantStats stat in list) {
				StatPoints tempStat = (StatPoints)stat;
				if (!pointsDict.ContainsKey(tempStat.Name)) pointsDict.Add(tempStat.Name, 0);
				pointsDict[tempStat.Name] += tempStat.Points;
			}
			string returnName = pointsDict.Count > 0 ? pointsDict.Keys.First() : null;
			foreach (string name in pointsDict?.Keys) {
				if (pointsDict[name] > pointsDict[returnName]) {
					returnName = name;
				}
			}
			return returnName != null ? returnName : "";
		}
	}
}
