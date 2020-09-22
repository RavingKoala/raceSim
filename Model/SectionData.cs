using System;
using System.Collections.Generic;
using System.Text;

namespace Model {
    public class SectionData {
        public IParticipant Left { get; }
        public int DistanceLeft { get; }
        public IParticipant Right { get; }
        public int DistanceRight { get; }

        public SectionData(IParticipant left, int distanceLeft, IParticipant right, int distanceRight) {
            Left = left;
            DistanceLeft = distanceLeft;
            Right = right;
            DistanceRight = distanceRight;
        }
    }
}
