using System.Collections.Generic;
using System.Linq;

namespace Model {
	public class StatEquipmentBroke : IParticipantStats {
		public string Name { get; set; }
		public int EquipmentBroke { get; set; }

		public StatEquipmentBroke(IParticipant participant) {
			Name = participant.Name;
			EquipmentBroke = 0;
		}
		public void Add(List<IParticipantStats> list) {
			StatEquipmentBroke tempClass = null;
			foreach (IParticipantStats Stat in list) {
				if (Stat.Name.Equals(this.Name)) {
					tempClass = (StatEquipmentBroke)Stat;
				}
			}
			if (tempClass == null) {
				list.Add(this);
				tempClass = this;
			}
			tempClass.EquipmentBroke++;
		}

		public string BesteSpeler(List<IParticipantStats> list) {
			Dictionary<string, int> EquipmentBrokeDict = new Dictionary<string, int>();
			foreach (IParticipantStats stat in list) {
				StatEquipmentBroke tempStat = (StatEquipmentBroke)stat;
				EquipmentBrokeDict.Add(tempStat.Name, tempStat.EquipmentBroke);
			}
			string returnName = EquipmentBrokeDict.Count > 0 ? EquipmentBrokeDict.Keys.First() : null;
			foreach (string name in EquipmentBrokeDict.Keys) {
				if (EquipmentBrokeDict[name] > EquipmentBrokeDict[returnName]) {
					returnName = name;
				}
			}
			return returnName?? "";
		}
	}
}
