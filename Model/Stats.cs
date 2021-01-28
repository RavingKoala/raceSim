using System.Collections.Generic;

namespace Model {
	public class Stats<T> where T : IParticipantStats {
		private List<IParticipantStats> _list = new List<IParticipantStats>();

		public void Add(T item) {
			item.Add(_list);
		}

		public List<IParticipantStats> GetList() {
			return _list;
		}

		public string BesteSpeler() {
			if (_list.Count > 0)
				return _list[0].BesteSpeler(_list);
			return null;
		}

		public void ResetList() {
			_list = new List<IParticipantStats>();
		}
	}
}
