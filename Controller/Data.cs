using System;
using System.Collections.Generic;
using Model;

namespace ControllerTest {
    public static class Data {
        public static Competition Competition { get; private set;  }
        public static Race CurrentRace { get; private set; }


        public static void Initialize() {
            SetupCompetition();
        }

        private static void SetupCompetition() {
            Competition = new Competition();

            Competition.Participants.Add(new Snake("steve", 0, new Scooter(), TeamColors.Blue));
            Competition.Participants.Add(new Snake("bob", 0, new Scooter(), TeamColors.Red));

            Competition.Tracks.Enqueue(new Track("Test", new SectionTypes[] { SectionTypes.Straight, SectionTypes.LeftCorner}));
        }
        
        public static void NextRace() {
            Track nextTrack = Competition.NextTrack();
            if ( nextTrack != null) {
                Console.WriteLine(nextTrack.Name);
                CurrentRace = new Race(nextTrack, Competition.Participants);
            }
        }
    }
}