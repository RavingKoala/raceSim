using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControllerTest
{
    public class Race {
        public Track Track { get; }
        public List<IParticipant> Participants { get; }
        DateTime StartTime { get; }
        private Random _random { get; }
        private Dictionary<Section, SectionData> _positions { get; }

        public Race(Track track, List<IParticipant> participants) {
            Track = track;
            Participants = participants;
            _random = new Random(DateTime.Now.Millisecond);

        }

        public SectionData GetSectionData(Section section) {
            return _positions[section];
        }

        public void RandomizeEquipment() {
            foreach (IParticipant participant in Participants) {
                participant.Equiptment.Quality = _random.Next(10);
                participant.Equiptment.Performance = _random.Next(10);
            }
        }
    }
}
