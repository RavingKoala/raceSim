using ControllerTest;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace raceSim {
    public static class Visuals {
		private static int x, y;
		private static int direction;

        #region graphics
        private readonly static string[] _horizontalFinnish = { "----", "  2#", "  1#", "----" };
        private readonly static string[] _horizontal = { "----", " 2  ", "  1 ", "----" };
		private readonly static string[] _horizontalStart = { "----", " 2] ", "  1]", "----" };
		private readonly static string[] _vertical = { "|  |", "| 2|", "|1 |", "|  |" };
        private readonly static string[] _northToEast = { "|  \\", "\\ 1 ", " \\2 ", "  \\-" };
        private readonly static string[] _eastToSouth = { "  /-", " /2 ", "/ 1 ", "|  /" };
        private readonly static string[] _southToWest = { "-\\  ", " 2\\ ", " 1 \\", "\\  |" };
        private readonly static string[] _westToNorth = { "/  |", " 1 /", " 2/ ", "-/  " };
        #endregion


        public static void DrawTrack(Track track) {
			x = 0;
			y = 0;
			direction = 0;
			foreach (Section section in track.Sections) {
				SectionData sectionData = Data.CurrentRace.GetSectionData(section);
				// setup players
				IParticipant participant1 = null;
				IParticipant participant2 = null;
				if (sectionData != null) {
					participant1 = sectionData.Right;
					if (sectionData.Left != null) {
						participant2 = sectionData.Left;
					}
				}
				// draw track by type
				switch (section.SectionType) {
					case SectionTypes.Straight:
						if (direction == 0 || direction == 2) {
							TrackToConsole(DrawParticipants(_vertical.ToArray(), participant1, participant2));
						} else if (direction == 1 || direction == 3) {
							TrackToConsole(DrawParticipants(_horizontal.ToArray(), participant1, participant2));
						}
						break;
					case SectionTypes.LeftCorner:
						if (direction == 0) {
							TrackToConsole(DrawParticipants(_southToWest.ToArray(), participant1, participant2));
						} else if (direction == 1) {
							TrackToConsole(DrawParticipants(_westToNorth.ToArray(), participant1, participant2));
						} else if (direction == 2) {
							TrackToConsole(DrawParticipants(_northToEast.ToArray(), participant1, participant2));
						} else if (direction == 3) {
							TrackToConsole(DrawParticipants(_eastToSouth.ToArray(), participant1, participant2));
						}
						direction = direction == 0 ? 3 : direction - 1;
						break;
					case SectionTypes.RightCorner:
						if (direction == 0) {
							TrackToConsole(DrawParticipants(_eastToSouth.ToArray(), participant1, participant2));
						} else if (direction == 1) {
							TrackToConsole(DrawParticipants(_southToWest.ToArray(), participant1, participant2));
						} else if (direction == 2) {
							TrackToConsole(DrawParticipants(_westToNorth.ToArray(), participant1, participant2));
						} else if (direction == 3) {
							TrackToConsole(DrawParticipants(_northToEast.ToArray(), participant1, participant2));
						}
						direction = (direction + 1) % 4;
						break;
					case SectionTypes.StartGrid:
							TrackToConsole(DrawParticipants(_horizontalStart.ToArray(), participant1, participant2));
						break;
					case SectionTypes.Finish:
							TrackToConsole(DrawParticipants(_horizontalFinnish.ToArray(), participant1, participant2));
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

		private static void TrackToConsole(string[] trackSection) {
			for (int tempY = 0; tempY < trackSection.Length; tempY++) {
				for (int tempX = 0; tempX < trackSection[tempY].Length; tempX++) {
					Console.SetCursorPosition(x + tempX, y + tempY);
					Console.Write(trackSection[tempY][tempX]);
				}
			}
		}

		public static string[] DrawParticipants(string[] trackSection, IParticipant participant1, IParticipant participant2) {
			for (int i = 0; i < trackSection.Length; i++) {
				trackSection[i] = trackSection[i].Replace("2", participant2 == null ? " " : participant2.Name[0].ToString());
				trackSection[i] = trackSection[i].Replace("1", participant1 == null ? " " : participant1.Name[0].ToString());
			}
			return trackSection;
		}

		public static void DriversChanged(object Sender, DriversChangedEventArgs args) {
			DrawTrack(args.track);
		}
	}
}
