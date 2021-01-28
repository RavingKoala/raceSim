using Model;

namespace Controller {
	public static class Data {
		public static Competition Competition { get; private set; }
		public static Race CurrentRace { get; private set; }

		public static void Initialize() {
			setupCompetition();
		}

		private static void setupCompetition() {
			Competition = new Competition();

			Competition.Participants.Add(new Snake("Steve", 0, new Scooter(), TeamColors.Blue));
			Competition.Participants.Add(new Snake("Mick", 0, new Scooter(), TeamColors.Red));
			Competition.Participants.Add(new Snake("Jeff", 0, new Scooter(), TeamColors.Lime));
			Competition.Participants.Add(new Snake("Berd", 0, new Scooter(), TeamColors.Purple));
			Competition.Participants.Add(new Snake("Alex", 0, new Scooter(), TeamColors.Violet));

			Competition.Tracks.Enqueue(new Track("First track", new SectionTypes[] { SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.Finish, SectionTypes.RightCorner, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.RightCorner }));
			Competition.Tracks.Enqueue(new Track("Second track", new SectionTypes[] { SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.Finish, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.LeftCorner, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.LeftCorner, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner }));
			Competition.Tracks.Enqueue(new Track("Third track", new SectionTypes[] { SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.Finish, SectionTypes.RightCorner, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.LeftCorner, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.RightCorner }));
		}

		public static void NextRace() {
			Track nextTrack = Competition.NextTrack();
			if (nextTrack != null) {
				CurrentRace = new Race(nextTrack, Competition.Participants);
			}
		}
	}
}