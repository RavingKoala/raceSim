using Model;
using System;
using System.Collections.Generic;
using System.Timers;

namespace ControllerTest
{
    public class Race {
        public Track Track { get; }
        public List<IParticipant> Participants { get; }
        private DateTime StartTime { get; set; }
        private Random _random { get; }
        private Dictionary<Section, SectionData> _positions { get; set; }
        private Dictionary<IParticipant, int> _laps { get; set; }
        private Timer _timer { get; set; }
        private bool _movingDrivers { get; set; }


        public event ParticipantChanged DriverChanged;
        public event EventHandler RaceFinished;
        public event ParticipantPassed ParticipantPassed;
        public event ParticipantLapped ParticipantLapped;
        public event ParticipantFinished ParticipantFinished;
        public event ParticipantEquipmentBroke ParticipantEquipmentBroke;

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
            StartTime = DateTime.Now;
            _timer.Start();
        }

        public void ClearEvents() {
			DriverChanged = null;
			RaceFinished = null;
			ParticipantPassed = null;
			ParticipantLapped = null;
			ParticipantFinished = null;
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
                int randInt = _random.Next(tempParticipants.Count - 1);
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
            if (fromSection.SectionType == SectionTypes.Finish) {
                if (_positions[toSection].Left == null) {
                    ParticipantLapped?.Invoke(new ParticipantLappedEventArgs(_positions[toSection].Right, StartTime.Subtract(DateTime.Now)));
                    if (++_laps[_positions[toSection].Right] > Competition.Laps) {
                        ParticipantFinished?.Invoke(new ParticipantFinishedEventArgs(_positions[toSection].Right, StartTime.Subtract(DateTime.Now)));
                        _positions.Remove(toSection);
                    }
                } else {
                    ParticipantLapped?.Invoke(new ParticipantLappedEventArgs(_positions[toSection].Left, StartTime.Subtract(DateTime.Now)));
                    if (++_laps[_positions[toSection].Left] > Competition.Laps) {
                        ParticipantFinished?.Invoke(new ParticipantFinishedEventArgs(_positions[toSection].Left, StartTime.Subtract(DateTime.Now)));
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
            ParticipantPassed?.Invoke(new ParticipantPassedEventArgs(_positions[fromSection].Left));
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
                    ParticipantLapped?.Invoke(new ParticipantLappedEventArgs(_positions[toSection].Right, StartTime.Subtract(DateTime.Now)));
                    if (++_laps[_positions[toSection].Right] > Competition.Laps) {
                        ParticipantFinished?.Invoke(new ParticipantFinishedEventArgs(_positions[toSection].Right, StartTime.Subtract(DateTime.Now)));
                        _positions.Remove(toSection);
                    }
                } else {
                    ParticipantLapped?.Invoke(new ParticipantLappedEventArgs(_positions[toSection].Left, StartTime.Subtract(DateTime.Now)));
                    if (++_laps[_positions[toSection].Left] > Competition.Laps) {
                        ParticipantFinished?.Invoke(new ParticipantFinishedEventArgs(_positions[toSection].Left, StartTime.Subtract(DateTime.Now)));
                        _positions[toSection].Left = null;
                        _positions[toSection].DistanceLeft = 0;
                    }
                }
            }
            _positions[fromSection].Left = null;
            _positions[fromSection].DistanceLeft = 0;
        }
        //private void advanceParticipant(IParticipant Participant, Section section) { }
        //private void switchParticipantFromLeftToRight(Section section) { }
        //private void removeParticipant(IParticipant? Participant, Section section) { }
        //private void cleanSections() { } // remove all empty sections
        //private void lapParticipant(IParticipant participant) { }

        private void MoveParticipants() {
            Stack<Section> sections = new Stack<Section>(Track.Sections);
            //Find track without Participants (so you dont move the Participant twice)
            while (_positions.ContainsKey(sections.Peek())) {
                Stack<Section> tempStack = new Stack<Section>(sections);
                Section tempSection = tempStack.Pop();
                sections = new Stack<Section>(tempStack);
                sections.Push(tempSection);
            }
            //Loop every Participant
            Section currentSection, nextSection;
            currentSection = Track.Sections.First.Value;
            while (sections.Count > 0) {
                nextSection = currentSection;
                currentSection = sections.Pop();
                if (_positions.ContainsKey(currentSection)) {
                    bool hasLeftParticipant = _positions[currentSection].Left != null;

                    //Progress trough equipment
                    damageOrRepairEquipment(_positions[currentSection].Right);
                    if (hasLeftParticipant)
                        damageOrRepairEquipment(_positions[currentSection].Left);

                    //Add distance to Participants
                    if (!_positions[currentSection].Right.Equiptment.IsBroken) {
                        _positions[currentSection].DistanceRight += _positions[currentSection].Right.Equiptment.Speed * _positions[currentSection].Right.Equiptment.Performance;
					}
                    if (hasLeftParticipant) {
						if (!_positions[currentSection].Left.Equiptment.IsBroken) {
                            _positions[currentSection].DistanceLeft += _positions[currentSection].Left.Equiptment.Speed * _positions[currentSection].Left.Equiptment.Performance;
						}
                    }
                    //Move Participants 
                    bool rightMoved = false;
                    if (_positions[currentSection].DistanceRight >= Section.length) {
                        moveRightParticipantToSection(currentSection, nextSection);
                        rightMoved = true;
                    }
                    if (hasLeftParticipant) {
                        //Move left or right dependant on if the right one moved (if the last one moved the left got moved to the right)
						if (_positions[currentSection].DistanceRight >= Section.length && rightMoved) {
                            moveRightParticipantToSection(currentSection, nextSection);
						} else if (_positions[currentSection].DistanceLeft >= Section.length && !rightMoved) {
                            moveLeftParticipantToSection(currentSection, nextSection);
						}
                    }
                }
            }
		}

        private void damageOrRepairEquipment(IParticipant Participant) {
            // 25 % chance to repair a broken item
			if (Participant.Equiptment.IsBroken == true) {
                Participant.Equiptment.Quality += _random.Next(1, 40);
                if (Participant.Equiptment.Quality > 75) {
                    if (Participant.Equiptment.Performance < 25)
                        Participant.Equiptment.Performance += _random.Next(1, 6);
                    Participant.Equiptment.IsBroken = false;
                }
            } else {
                int maxR = Participant.Equiptment.Quality;
                Participant.Equiptment.Quality = _random.Next(maxR - maxR/3, maxR);
				if (Participant.Equiptment.Quality < 7) {
                    Participant.Equiptment.IsBroken = true;
					if (Participant.Equiptment.Performance > 10)
                        Participant.Equiptment.Performance -= 3;
                    ParticipantEquipmentBroke?.Invoke(new ParticipantEquipmentBrokeEventArgs(Participant));
                }
            }
		}


        public void OnTimedEvent(object o, EventArgs args) {
            if (!_movingDrivers) {
                _movingDrivers = true;
                MoveParticipants();
                if (_positions.Count > 0) {
                    DriverChanged?.Invoke(new ParticipantChangedEventArgs(Data.CurrentRace.Track));
			    } else {
                    RaceFinished?.Invoke(this, new EventArgs());
                }
                _movingDrivers = false;
            }
		}
    }
}
