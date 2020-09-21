using System;
using System.Collections.Generic;
using Model;

namespace ControllerTest {
    public static class Data {
        static Competition Competition;
        static Race CurrentRace;


        public static void Initialize() {
            SetupCompetition();
        }

        private static void SetupCompetition() {
            Competition = new Competition();

            Competition.Participants.Add(new Driver("steve", 0, new Car(), TeamColors.Blue));
            Competition.Participants.Add(new Driver("bob", 0, new Car(), TeamColors.Red));

            LinkedList<Section> sections = new LinkedList<Section>();
            sections.AddFirst(new Section(SectionTypes.Straight));
            sections.AddFirst(new Section(SectionTypes.LeftCorner));
            Competition.Tracks.Enqueue(new Track("Test", sections));
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