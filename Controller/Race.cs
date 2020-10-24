using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
        private Dictionary<IParticipant, int> _laps { get; set; }
        private Timer _timer { get; set; }
        private bool _movingDrivers { get; set; }


        public event DriversChanged driverChanged;
        public event EventHandler raceFinished;

        public Race(Track track, List<IParticipant> participants) {
            Track = track;
            Participants = participants;
            _random = new Random(DateTime.Now.Millisecond);
            _positions = new Dictionary<Section, SectionData>();
            _laps = new Dictionary<IParticipant, int>();
            RandomizeEquipment();
            PlaceParticipants();
            _timer = new Timer(500);
			_timer.Elapsed += OnTimedEvent;
            _movingDrivers = false;
        }

        public void Start() {
            _timer.Start();
		}

        public void ClearEvents() {
            /*driverChanged.*/
            Delegate[] list = driverChanged.GetInvocationList();
            foreach (Delegate del in list) {
                driverChanged -= (DriversChanged) del;
			}
            list = raceFinished.GetInvocationList();
            foreach (Delegate del in list) {
                raceFinished -= (EventHandler)del;
            }
        }

        public SectionData GetSectionData(Section section) {
			return _positions.ContainsKey(section) ? _positions[section] : null;
		}

		public void RandomizeEquipment() {
            foreach (IParticipant participant in Participants) {
                participant.Equiptment.Quality = _random.Next(50, 100);
                participant.Equiptment.Performance = _random.Next(12, 17);
            }
        }

        public void PlaceParticipants() {
            List<IParticipant> tempParticipants = new List<IParticipant>(Participants);
            Stack<IParticipant> participantQueue = new Stack<IParticipant>();
			while (tempParticipants.Count > 0) {
                int randInt = _random.Next(tempParticipants.Count-1);
                participantQueue.Push(tempParticipants[randInt]);
                _laps.Add(tempParticipants[randInt], 0);
                tempParticipants.RemoveAt(randInt);
            }

            Stack<Section> sectionQueue = new Stack<Section>();
            foreach (Section section in Track.Sections) {
                if (section.SectionType == SectionTypes.StartGrid) {
                    sectionQueue.Push(section);
                }
            }
			while (sectionQueue.Count > 0) {
                Section queuevalue = sectionQueue.Pop();
				if (participantQueue.Count > 1) {
                    _positions.Add(queuevalue, new SectionData(participantQueue.Pop(), 0, participantQueue.Pop(), 0));
                } else if (participantQueue.Count == 1) {
                    _positions.Add(queuevalue, new SectionData(participantQueue.Pop(), 0));
				}
			}
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
            if (fromSection.SectionType == SectionTypes.Finish){
				if (_positions[toSection].Left == null) {
					if (++_laps[_positions[toSection].Right] > 3) {
                        _positions.Remove(toSection);
                    }
				} else {
                    if (++_laps[_positions[toSection].Left] > 3) {
                        _positions[toSection].Left = null;
                        _positions[toSection].DistanceLeft = 0;
                    }
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

        private void moveLeftParticipantToSection(Section fromSection, Section toSection) {
            _positions[fromSection].DistanceLeft = _positions[fromSection].DistanceLeft % Section.length;
            if (!_positions.ContainsKey(toSection)) {
                _positions.Add(toSection, new SectionData(_positions[fromSection].Left, _positions[fromSection].DistanceLeft));
            } else {
                if (_positions[toSection].Left == null) {
                    _positions[toSection].Left = _positions[fromSection].Left;
                    _positions[toSection].DistanceLeft = _positions[fromSection].DistanceLeft;
                } else {
                    // section full, no catch up possible
                    _positions[fromSection].DistanceRight = Section.length - 1;
                    return;
                }
            }
            if (fromSection.SectionType == SectionTypes.Finish) {
                if (_positions[toSection].Left == null) {
                    if (++_laps[_positions[toSection].Right] > 3) {
                        _positions.Remove(toSection);
                    }
                } else {
                    if (++_laps[_positions[toSection].Left] > 3) {
                        _positions[toSection].Left = null;
                        _positions[toSection].DistanceLeft = 0;
                    }
                }
            }
            _positions[fromSection].Left = null;
            _positions[fromSection].DistanceLeft = 0;
        }

        public void MovePlayers() {
            Stack<Section> sections = new Stack<Section>(Track.Sections);
			// find track without players (so you dont move the player twice)
			while (_positions.ContainsKey(sections.Peek())) {
                Stack<Section> tempStack = new Stack<Section>(sections);
                Section tempSection = tempStack.Pop();
                sections = new Stack<Section>(tempStack);
                sections.Push(tempSection);
            }
            // loop every player
            Section currentSection, nextSection;
            currentSection = Track.Sections.First.Value;
            while (sections.Count > 0) {
                nextSection = currentSection;
                currentSection = sections.Pop();
                if (_positions.ContainsKey(currentSection)) {
                    bool hasLeftParticipant = _positions[currentSection].Left != null;

                    damageOrRepairEquipment(_positions[currentSection].Right);
                    if (hasLeftParticipant)
                        damageOrRepairEquipment(_positions[currentSection].Left);

                    if (!_positions[currentSection].Right.Equiptment.IsBroken) {
                        _positions[currentSection].DistanceRight += _positions[currentSection].Right.Equiptment.Speed * _positions[currentSection].Right.Equiptment.Performance;
					}
                    if (hasLeftParticipant) {
						if (!_positions[currentSection].Left.Equiptment.IsBroken) {
                            _positions[currentSection].DistanceLeft += _positions[currentSection].Left.Equiptment.Speed * _positions[currentSection].Left.Equiptment.Performance;
						}
                    }

                    bool rightMoved = false;
                    if (_positions[currentSection].DistanceRight >= Section.length) {
                        moveRightParticipantToSection(currentSection, nextSection);
                        rightMoved = true;
                    }
                    if (hasLeftParticipant) {
						if (_positions[currentSection].DistanceRight >= Section.length && rightMoved) {
                            moveRightParticipantToSection(currentSection, nextSection);
						} else if (_positions[currentSection].DistanceLeft >= Section.length && !rightMoved) {
                            moveLeftParticipantToSection(currentSection, nextSection);
						}
                    }
                }
            }
		}

        public void damageOrRepairEquipment(IParticipant player) {
            // 25 % chance to repair a broken item
			if (player.Equiptment.IsBroken == true) {
                player.Equiptment.Quality += _random.Next(1, 40);
                if (player.Equiptment.Quality > 75) {
                    if (player.Equiptment.Performance < 23)
                        player.Equiptment.Performance += _random.Next(1, 6);
                    player.Equiptment.IsBroken = false;
                }
            } else {
                int maxR = player.Equiptment.Quality;
                player.Equiptment.Quality = _random.Next(maxR - maxR/3, maxR);
				if (player.Equiptment.Quality < 7) {
                    player.Equiptment.IsBroken = true;
					if (player.Equiptment.Performance > 10)
                        player.Equiptment.Performance -= 3;
                }
            }
		}

        public void OnTimedEvent(object o, EventArgs args) {
            if (!_movingDrivers) {
                _movingDrivers = true;
                MovePlayers();
                if (_positions.Count > 0) {
                    driverChanged?.Invoke(new DriversChangedEventArgs(Data.CurrentRace.Track));
			    } else {
                    raceFinished?.Invoke(this, new EventArgs());
                }
                _movingDrivers = false;
            }
		}

    }
}
