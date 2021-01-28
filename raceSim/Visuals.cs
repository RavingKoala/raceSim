using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace raceSim {
	public static class Visuals {
		private static int startx, starty;
		private static int maxx, maxy;
		private static int x, y;
		private static int direction;
		private static int statsYPosition;
		private static Dictionary<string, int> statPositions = new Dictionary<string, int>();

		#region graphics
		private readonly static string[] _horizontalFinnish = { "----", "  2#", "  1#", "----" };
		private readonly static string[] _horizontal = { "----", " 2  ", "  1 ", "----" };
		private readonly static string[] _horizontalStart = { "----", " 2] ", "  1]", "----" };
		private readonly static string[] _vertical = { "|  |", "| 2|", "|1 |", "|  |" };
		private readonly static string[] _northToEast = { "|  \\", "\\  1", " \\2 ", "  \\-" };
		private readonly static string[] _eastToSouth = { "  /-", " /2 ", "/ 1 ", "|  /" };
		private readonly static string[] _southToWest = { "-\\  ", " 2\\ ", " 1 \\", "\\  |" };
		private readonly static string[] _westToNorth = { "/  |", " 1 /", " 2/ ", "-/  " };
		#endregion

		public static void Initialize(Track track) {
			statsYPosition = 0;
			Console.CursorVisible = false;
			PrepareDrawStartValue(track);
		}

		public static void PrepareDrawStartValue(Track track) {
			startx = starty = maxx = maxy = x = y = 0;
			direction = 1;
			foreach (Section section in track.Sections) {
				// draw track by type
				switch (section.SectionType) {
					case SectionTypes.LeftCorner:
						direction = direction == 0 ? 3 : direction - 1;
						break;
					case SectionTypes.RightCorner:
						direction = (direction + 1) % 4;
						break;
					default:
						break;
				}
				if (direction == 0) {
					y -= 4;
					if (y < starty)
						starty = y * -1;
				}
				if (direction == 1) {
					x += 4;
					if (x > maxx - 4)
						maxx = x + 4;
				}
				if (direction == 2) {
					y += 4;
					if (y > maxy - 4)
						maxy = y + 4;
				}
				if (direction == 3) {
					x -= 4;
					if (x < startx)
						startx = x * -1;
				}
			}
		}

		public static void ClearTrack() {
			string tracklongstring = "";
			for (int i = 0; i < maxx; i++)
				tracklongstring = String.Concat(tracklongstring, " ");
			for (int i = 0; i < maxy; i++) {
				Console.SetCursorPosition(0, i);
				Console.Write(tracklongstring);
			}
		}

		public static void DrawTrack(Track track) {
			x = 0;
			y = 0;
			direction = 1;
			foreach (Section section in track.Sections) {
				SectionData sectionData = Data.CurrentRace.GetSectionData(section);
				// setup Participants
				IParticipant participant1 = sectionData?.Right;
				IParticipant participant2 = sectionData?.Left;
				// draw track by type
				switch (section.SectionType) {
					case SectionTypes.Straight:
						if (direction == 0 || direction == 2) {
							trackToConsole(drawParticipants(_vertical.ToArray(), participant1, participant2));
						} else if (direction == 1 || direction == 3) {
							trackToConsole(drawParticipants(_horizontal.ToArray(), participant1, participant2));
						}
						break;
					case SectionTypes.LeftCorner:
						if (direction == 0) {
							trackToConsole(drawParticipants(_southToWest.ToArray(), participant1, participant2));
						} else if (direction == 1) {
							trackToConsole(drawParticipants(_westToNorth.ToArray(), participant1, participant2));
						} else if (direction == 2) {
							trackToConsole(drawParticipants(_northToEast.ToArray(), participant1, participant2));
						} else if (direction == 3) {
							trackToConsole(drawParticipants(_eastToSouth.ToArray(), participant1, participant2));
						}
						direction = direction == 0 ? 3 : direction - 1;
						break;
					case SectionTypes.RightCorner:
						if (direction == 0) {
							trackToConsole(drawParticipants(_eastToSouth.ToArray(), participant1, participant2));
						} else if (direction == 1) {
							trackToConsole(drawParticipants(_southToWest.ToArray(), participant1, participant2));
						} else if (direction == 2) {
							trackToConsole(drawParticipants(_westToNorth.ToArray(), participant1, participant2));
						} else if (direction == 3) {
							trackToConsole(drawParticipants(_northToEast.ToArray(), participant1, participant2));
						}
						direction = (direction + 1) % 4;
						break;
					case SectionTypes.StartGrid:
						trackToConsole(drawParticipants(_horizontalStart.ToArray(), participant1, participant2));
						break;
					case SectionTypes.Finish:
						trackToConsole(drawParticipants(_horizontalFinnish.ToArray(), participant1, participant2));
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

		private static void trackToConsole(string[] trackSection) {
			for (int tempY = 0; tempY < trackSection.Length; tempY++) {
				for (int tempX = 0; tempX < trackSection[tempY].Length; tempX++) {
					Console.SetCursorPosition(startx + x + tempX, starty + y + tempY);
					Console.Write(trackSection[tempY][tempX]);
				}
			}
		}

		private static string[] drawParticipants(string[] trackSection, IParticipant participant1, IParticipant participant2) {

			for (int i = 0; i < trackSection.Length; i++) {
				trackSection[i] = trackSection[i].Replace("2", participant2 == null ? " " : (participant2.Equiptment.IsBroken ? "■" : participant2.Name[0].ToString()));
				trackSection[i] = trackSection[i].Replace("1", participant1 == null ? " " : (participant1.Equiptment.IsBroken ? "■" : participant1.Name[0].ToString()));
			}
			return trackSection;
		}

		private static void drawStat(string statName, string participantName, int yPos) {
			if (statsYPosition <= maxy) {
				Console.SetCursorPosition(maxx + 6, yPos);
				Console.Write($"{statName}: {participantName}");
				/*statsYPosition++;*/
			}
		}

		public static void DrawStats(UpdateStatsEventArgs updateStatsEventArgs) {
			string statEnumName = Enum.GetName(typeof(StatTypes), updateStatsEventArgs.Stat);
			if (!statPositions.ContainsKey(statEnumName)) {
				statPositions.Add(statEnumName, statsYPosition++);
			}
			drawStat(statEnumName, updateStatsEventArgs.ParticipantName, statPositions[statEnumName]);
		}

		public static void DriversChanged(ParticipantChangedEventArgs args) {
			DrawTrack(args.Track);
		}
	}
}
