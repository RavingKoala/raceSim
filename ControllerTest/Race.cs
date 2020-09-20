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

        private Race(Track track, List<IParticipant> participants, DateTime startTime, Random random, Dictionary<Section, SectionData> positions) {
            Track = track;
            Participants = participants;
            StartTime = startTime;
            _random = random;
            _positions = positions;
        }

        public Race(Track track, List<IParticipant> participants) {
            Track = track;
            Participants = participants;
            _random = new Random(DateTime.Now.Millisecond);

        }

        public SectionData getSectionData(Section section) {
            return _positions[section];
        }

        public void randomizeEquipment() {
            foreach (IParticipant participant in Participants) {
                participant.Equiptment.Quality = _random.Next(10);
                participant.Equiptment.Performance = _random.Next(10);
            }
        }
    }
}
