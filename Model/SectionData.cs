using System;
using System.Collections.Generic;
using System.Text;

namespace Model {
    public class SectionData {
        IParticipant Left;
        int DistanceLeft;
        IParticipant Right;
        int DistanceRight;

        public SectionData(IParticipant left, int distanceLeft, IParticipant right, int distanceRight) {
            Left = left;
            DistanceLeft = distanceLeft;
            Right = right;
            DistanceRight = distanceRight;
        }
    }
}
