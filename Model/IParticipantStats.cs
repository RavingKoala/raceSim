using System;
using System.Collections.Generic;

namespace Model {
	public interface IParticipantStats {
		string Name { get; set; }

		void Add(List<IParticipantStats> list);
		String BesteSpeler(List<IParticipantStats> list);
	}
}
