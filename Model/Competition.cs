using System;
using System.Collections.Generic;
using System.Text;

namespace Model {
    public class Competition {
        public List<IParticipant> Participants { get; }
        public Queue<Track> Tracks { get; set; }

        public Competition() {
            Participants = new List<IParticipant>();
            Tracks = new Queue<Track>();
        }

        public Track NextTrack() {
            Track returnValue;
            try {
                returnValue = Tracks.Dequeue();
            } catch {
                returnValue = null;
            }
            return returnValue;
        }
    }
}
