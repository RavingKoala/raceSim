﻿namespace Model {
	public class SectionData {
		public IParticipant Left { get; set; }
		public int DistanceLeft { get; set; }
		public IParticipant Right { get; set; }
		public int DistanceRight { get; set; }

		public SectionData(IParticipant left, int distanceLeft, IParticipant right, int distanceRight) {
			Left = left;
			DistanceLeft = distanceLeft;
			Right = right;
			DistanceRight = distanceRight;
		}

		public SectionData(IParticipant right, int distanceRight) {
			Left = null;
			DistanceLeft = 0;
			Right = right;
			DistanceRight = distanceRight;
		}
	}
}
