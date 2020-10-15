using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Timers;

namespace ControllerTest
{
    public class Race {
        public Track Track { get; }
        public List<IParticipant> Participants { get; }
        public DateTime StartTime { get; }
        private Random _random { get; }
        private Dictionary<Section, SectionData> _positions { get; set; }
        private Timer _timer { get; set; }


        public event EventHandler<DriversChangedEventArgs> driverChanged;

        public Race(Track track, List<IParticipant> participants) {
            Track = track;
            Participants = participants;
            _random = new Random(DateTime.Now.Millisecond);
            PlaceParticipants();
            _timer = new Timer(500);
			_timer.Elapsed += onTimedEvent;
        }

        public void Start() {
            _timer.Start();
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

        public void PlaceParticipants() {
            List<IParticipant> tempParticipants = new List<IParticipant>(Participants);
            Queue<IParticipant> participantQueue = new Queue<IParticipant>();
			while (tempParticipants.Count > 0) {
                int randInt = _random.Next(tempParticipants.Count-1);
                participantQueue.Enqueue(tempParticipants[randInt]);
                tempParticipants.RemoveAt(randInt);
            }

            Dictionary<Section, SectionData> tempPositions = new Dictionary<Section, SectionData>();
            int i = 0;
            foreach (Section section in Track.Sections) {
				if (section.SectionType == SectionTypes.StartGrid) {
					if (participantQueue.Count%2==1) {
                        tempPositions.Add(section, new SectionData(null, 0, participantQueue.Dequeue(), i));

                    } else {
                        tempPositions.Add(section, new SectionData(participantQueue.Dequeue(), i, participantQueue.Dequeue(), i));
					}
                }
                i++;
            }
            _positions = tempPositions;
        }

        public void MovePlayers(){
            Section[] sections = Track.Sections.ToArray();
            for (int i = sections.Length - 1; i >= 0; i--) {
                sections[]
            }
		}

        public void onTimedEvent(object o, ElapsedEventArgs args) {
            MovePlayers();
		}
    }
}
