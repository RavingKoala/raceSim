using System.Collections.Generic;
using System.Linq;

namespace Model {
	public class StatPasses : IParticipantStats {
		public string Name { get; set; }
		public int Passes { get; set; }

		public StatPasses(IParticipant participant) {
			Name = participant.Name;
			Passes = 0;
		}

		public void Add(List<IParticipantStats> list) {
			StatPasses tempClass = null;
			foreach (IParticipantStats Stat in list) {
				if (Stat.Name.Equals(this.Name)) {
					tempClass = (StatPasses)Stat;
				}
			}
			if (tempClass == null) {
				list.Add(this);
				tempClass = this;
			}
			tempClass.Passes++;
		}

		public string BesteSpeler(List<IParticipantStats> list) {
			Dictionary<string, int> PassesDict = new Dictionary<string, int>();
			foreach (IParticipantStats stat in list) {
				StatPasses tempStat = (StatPasses)stat;
				PassesDict.Add(tempStat.Name, tempStat.Passes);
			}
			string returnName = PassesDict.Count > 0 ? PassesDict.Keys.First() : null;
			foreach (string name in PassesDict?.Keys) {
				if (PassesDict[name] > PassesDict[returnName]) {
					returnName = name;
				}
			}
			return returnName ?? "";
		}
	}
}
