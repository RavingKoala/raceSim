using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControllerTest
{
    class Race {
        Track Track;
        List<IParticipant> Participants;
        DateTime StartTime;
        private Random _random;
        private Dictionary<Section, SectionData> _positions;

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
