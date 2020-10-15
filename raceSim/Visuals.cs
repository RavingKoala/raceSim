using ControllerTest;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace raceSim {
    public static class Visuals {
		private static int x = 0, y = 0;
		private static int direction = 0;

        #region graphics
        private readonly static string[] _horizontalFinnish = { "----", "  # ", "  # ", "----" };
        private readonly static string[] _horizontal = { "----", "    ", "    ", "----" };
		private static string[] _horizontalStart = { "----", "2]  ", "  1]", "----" };
		private readonly static string[] _vertical = { "|  |", "|  |", "|  |", "|  |" };
        private readonly static string[] _northToEast = { "|  \\", "\\   ", " \\  ", "  \\-" };
        private readonly static string[] _eastToSouth = { "  /-", " /  ", "/   ", "|  /" };
        private readonly static string[] _southToWest = { "-\\  ", "  \\ ", "   \\", "\\  |" };
        private readonly static string[] _westToNorth = { "/  |", "   /", "  / ", "-/  " };
        #endregion


        public static void DrawTrack(Track track) {
			foreach (Section section in track.Sections) {
				switch (section.SectionType) {
					case SectionTypes.Straight:
						if (direction == 0 || direction == 2) {
							TrackToConsole(_vertical);
						} else if (direction == 1 || direction == 3) {
							TrackToConsole(_horizontal);
						}
						break;
					case SectionTypes.LeftCorner:
						if (direction == 0) {
							TrackToConsole(_southToWest);
						} else if (direction == 1) {
							TrackToConsole(_westToNorth);
						} else if (direction == 2) {
							TrackToConsole(_northToEast);
						} else if (direction == 3) {
							TrackToConsole(_eastToSouth);
						}
						direction = direction == 0 ? 3 : direction - 1;
						break;
					case SectionTypes.RightCorner:
						if (direction == 0) {
							TrackToConsole(_eastToSouth);
						} else if (direction == 1) {
							TrackToConsole(_southToWest);
						} else if (direction == 2) {
							TrackToConsole(_westToNorth);
						} else if (direction == 3) {
							TrackToConsole(_northToEast);
						}
						direction = (direction + 1) % 4;
						break;
					case SectionTypes.StartGrid:
						TrackToConsole(DrawParticipants(_horizontalStart.ToArray(), Data.CurrentRace.GetSectionData(section).Right, Data.CurrentRace.GetSectionData(section).Left));
						break;
					case SectionTypes.Finish:
						TrackToConsole(_horizontalFinnish);
						break;
					default:
						break;
				}
				if (direction == 0)
					y -= 4;
				if (direction == 1)
					x += 4;
				if (direction == 2)
					y += 4;
				if (direction == 3)
					x -= 4;
			}
		}

		private static void TrackToConsole(string[] track) {
			for (int tempY = 0; tempY < track.Length; tempY++) {
				for (int tempX = 0; tempX < track[tempY].Length; tempX++) {
					Console.SetCursorPosition(x + tempX, y + tempY);
					Console.Write(track[tempY][tempX]);
				}
			}
		}

		public static string[] DrawParticipants(string[] trackSection, IParticipant participant1, IParticipant participant2) {
			string sParticipant2 = participant2 != null ? participant2.Name[0].ToString() + "]" : "  ";
			trackSection[1] = trackSection[1].Replace("2]", sParticipant2);
			trackSection[2] = trackSection[2].Replace("1", participant1.Name[0].ToString());
			return trackSection;
		}

		public static void DriversChanged(object Sender, DriversChangedEventArgs args) {
			DrawTrack(args.track);
		}
	}
}
