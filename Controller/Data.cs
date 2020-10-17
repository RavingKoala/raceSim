using System;
using System.Collections.Generic;
using Model;

namespace ControllerTest {
    public static class Data {
        public static Competition Competition { get; private set;  }
        public static Race CurrentRace { get; private set; }


        public static void Initialize() {
            setupCompetition();
        }

		private static void setupCompetition() {
            Competition = new Competition();

            Competition.Participants.Add(new Snake("Steve", 0, new Scooter(), TeamColors.Blue));
            Competition.Participants.Add(new Snake("Mick", 0, new Scooter(), TeamColors.Red));
            Competition.Participants.Add(new Snake("Jeff", 0, new Scooter(), TeamColors.Green));
            Competition.Participants.Add(new Snake("Berd", 0, new Scooter(), TeamColors.Grey));
            Competition.Participants.Add(new Snake("Alex", 0, new Scooter(), TeamColors.Yellow));

            Competition.Tracks.Enqueue(new Track("First", new SectionTypes[] {SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.Finish, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.LeftCorner, SectionTypes.Straight, SectionTypes.LeftCorner, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.LeftCorner, SectionTypes.Straight, SectionTypes.LeftCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.LeftCorner, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight }));
        }
        
        public static void NextRace() {
            Track nextTrack = Competition.NextTrack();
            if ( nextTrack != null) {
                CurrentRace = new Race(nextTrack, Competition.Participants);
            }
        }

    }
}