using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
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
			_timer.Elapsed += OnTimedEvent;
        }

        public void Start() {
            _timer.Start();
		}

        public SectionData GetSectionData(Section section) {
			return _positions.ContainsKey(section) ? _positions[section] : null;
		}

		public void RandomizeEquipment() {
            foreach (IParticipant participant in Participants) {
                participant.Equiptment.Quality = _random.Next(10);
                participant.Equiptment.Performance = _random.Next(10);
            }
        }

        public void PlaceParticipants() {
            List<IParticipant> tempParticipants = new List<IParticipant>(Participants);
            Stack<IParticipant> participantQueue = new Stack<IParticipant>();
			while (tempParticipants.Count > 0) {
                int randInt = _random.Next(tempParticipants.Count-1);
                participantQueue.Push(tempParticipants[randInt]);
                tempParticipants.RemoveAt(randInt);
            }

            Dictionary<Section, SectionData> tempPositions = new Dictionary<Section, SectionData>();
            Stack<Section> sectionQueue = new Stack<Section>();
            foreach (Section section in Track.Sections) {
                if (section.SectionType == SectionTypes.StartGrid) {
                    sectionQueue.Push(section);
                }
            }
			while (sectionQueue.Count > 0) {
                Section queuevalue = sectionQueue.Pop();
				if (participantQueue.Count > 1) {
                    tempPositions.Add(queuevalue, new SectionData(participantQueue.Pop(), 0, participantQueue.Pop(), 0));
                } else if (participantQueue.Count == 1) {
                    tempPositions.Add(queuevalue, new SectionData(participantQueue.Pop(), 0));
				}
			}
            _positions = tempPositions;
        }

        private void moveRightParticipantToSection(Section fromSection, Section toSection) {
            _positions[fromSection].DistanceRight = _positions[fromSection].DistanceRight % Section.length;
            if (!_positions.ContainsKey(toSection)) {
                _positions.Add(toSection, new SectionData(_positions[fromSection].Right, _positions[fromSection].DistanceRight));
			} else {
				if (_positions[toSection].Left == null) {
                    _positions[toSection].Left = _positions[fromSection].Right;
                    _positions[toSection].DistanceLeft = _positions[fromSection].DistanceRight;
                } else {
                    // section full, no catch up possible
                    _positions[fromSection].DistanceRight = Section.length - 1;
                    return;
                }
			}
			if (_positions[fromSection].Left != null) {
                _positions[fromSection].Right = _positions[fromSection].Left;
                _positions[fromSection].Left = null;
                _positions[fromSection].DistanceRight = _positions[fromSection].DistanceLeft;
                _positions[fromSection].DistanceLeft = 0;
            } else {
                _positions.Remove(fromSection);
            }
        }

        public void MovePlayers() {
            Section[] sections = Track.Sections.ToArray();
			for (int i = sections.Length - 1; i >= 0; i--) {
                int nextSectionIndx = i + 1 == sections.Length ? 0 : i + 1;
                if (_positions.ContainsKey(sections[i])) {
                    bool hasLeftParticipant = _positions[sections[i]].Left != null;
                    
                    _positions[sections[i]].DistanceRight += _positions[sections[i]].Right.Equiptment.Speed * _positions[sections[i]].Right.Equiptment.Performance;
                    if (hasLeftParticipant) {
                        _positions[sections[i]].DistanceLeft += _positions[sections[i]].Left.Equiptment.Speed * _positions[sections[i]].Left.Equiptment.Performance;
                    }

                    if (_positions[sections[i]].DistanceRight >= Section.length) {
                        moveRightParticipantToSection(sections[i], sections[nextSectionIndx]);
                    }
                    if (hasLeftParticipant && _positions[sections[i]].DistanceRight >= Section.length) {
                        moveRightParticipantToSection(sections[i], sections[nextSectionIndx]);
                    }
                }
            }
		}

        public void OnTimedEvent(object o, ElapsedEventArgs args) {
            MovePlayers();
            driverChanged.Invoke(this, new DriversChangedEventArgs(Data.CurrentRace.Track));
		}
    }
}
