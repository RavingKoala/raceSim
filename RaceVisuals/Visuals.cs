using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace RaceVisuals {
	public static class Visuals
	{
		private static int startx, starty;
		private static int maxx, maxy;
		private static int x, y;
		private static int direction;
		private static int statsYPosition;
		private static Dictionary<string, int> statPositions = new Dictionary<string, int>();

		#region graphics

		//tracks
		private const string _horizontalFinnish = "Media/Tracks/FinishH.png";
		private const string _horizontal = "Media/Tracks/StraightH.png";
		private const string _horizontalStart = "Media/Tracks/StartH.png";
		private const string _vertical = "Media/Tracks/StraightV.png";
		private const string _northToEast = "Media/Tracks/CornerNE.png";
		private const string _eastToSouth = "Media/Tracks/CornerSE.png";
		private const string _southToWest = "Media/Tracks/CornerSW.png";

		private const string _westToNorth = "Media/Tracks/CornerNW.png";

		//scooter
		private const string _brokenScooter = "Media/Scooters/BrokenScooter.png";

		private const string _scooter = "Media/Scooters/DefaultScooter.png";

		//snakes
		private const string _blueSnake = "Media/Snakes/BlueSnake.png";
		private const string _cyanSnake = "Media/Snakes/CyanSnake.png";
		private const string _limeSnake = "Media/Snakes/LimeSnake.png";
		private const string _orangeSnake = "Media/Snakes/OrangeSnake.png";
		private const string _purpleSnake = "Media/Snakes/PurpleSnake.png";
		private const string _redSnake = "Media/Snakes/RedSnake.png";
		private const string _violetSnake = "Media/Snakes/VioletSnake.png";

		#endregion

		public static void Initialize(Track track)
		{
			statsYPosition = 0;
			PrepareDrawStartValue(track);
		}

		public static void PrepareDrawStartValue(Track track)
		{
			startx = starty = maxx = maxy = x = y = 0;
			direction = 1;
			foreach (Section section in track.Sections)
			{
				// draw track by type
				switch (section.SectionType)
				{
					case SectionTypes.LeftCorner:
						direction = direction == 0 ? 3 : direction - 1;
						break;
					case SectionTypes.RightCorner:
						direction = (direction + 1) % 4;
						break;
					default:
						break;
				}

				if (direction == 0)
				{
					y -= 1;
					if (y < starty)
						starty = y * -1;
				}

				if (direction == 1)
				{
					x += 1;
					if (x > maxx - 1)
						maxx = x + 1;
				}

				if (direction == 2)
				{
					y += 1;
					if (y > maxy - 1)
						maxy = y + 1;
				}

				if (direction == 3)
				{
					x -= 1;
					if (x < startx)
						startx = x * -1;
				}
			}
		}

		public static BitmapSource DrawTrack(Track track, int width, int height)
		{
			Bitmap baseTrack = CreateManager.EmptyBitmap(width, height);
			x = 0;
			y = 0;
			direction = 1;
			using (Graphics gr = Graphics.FromImage(baseTrack))
			{
				foreach (Section section in track.Sections)
				{
					SectionData sectionData = Data.CurrentRace.GetSectionData(section);
					// setup Participants
					IParticipant participant1 = sectionData?.Right;
					IParticipant participant2 = sectionData?.Left;
					// draw track by type
					switch (section.SectionType)
					{
						case SectionTypes.Straight:
							if (direction == 0 || direction == 2)
							{
								DrawTrackOnBackground(gr,
									drawParticipants(CreateManager.GetImage(_vertical, Track.Width, Track.Height),
										participant1, participant2));
							}
							else if (direction == 1 || direction == 3)
							{
								DrawTrackOnBackground(gr,
									drawParticipants(CreateManager.GetImage(_horizontal, Track.Width, Track.Height),
										participant1, participant2));
							}

							break;
						case SectionTypes.LeftCorner:
							if (direction == 0)
							{
								DrawTrackOnBackground(gr,
									drawParticipants(CreateManager.GetImage(_southToWest, Track.Width, Track.Height),
										participant1, participant2));
							}
							else if (direction == 1)
							{
								DrawTrackOnBackground(gr,
									drawParticipants(CreateManager.GetImage(_westToNorth, Track.Width, Track.Height),
										participant1, participant2));
							}
							else if (direction == 2)
							{
								DrawTrackOnBackground(gr,
									drawParticipants(CreateManager.GetImage(_northToEast, Track.Width, Track.Height),
										participant1, participant2));
							}
							else if (direction == 3)
							{
								DrawTrackOnBackground(gr,
									drawParticipants(CreateManager.GetImage(_eastToSouth, Track.Width, Track.Height),
										participant1, participant2));
							}

							direction = direction == 0 ? 3 : direction - 1;
							break;
						case SectionTypes.RightCorner:
							if (direction == 0)
							{
								DrawTrackOnBackground(gr,
									drawParticipants(CreateManager.GetImage(_eastToSouth, Track.Width, Track.Height),
										participant1, participant2));
							}
							else if (direction == 1)
							{
								DrawTrackOnBackground(gr,
									drawParticipants(CreateManager.GetImage(_southToWest, Track.Width, Track.Height),
										participant1, participant2));
							}
							else if (direction == 2)
							{
								DrawTrackOnBackground(gr,
									drawParticipants(CreateManager.GetImage(_westToNorth, Track.Width, Track.Height),
										participant1, participant2));
							}
							else if (direction == 3)
							{
								DrawTrackOnBackground(gr,
									drawParticipants(CreateManager.GetImage(_northToEast, Track.Width, Track.Height),
										participant1, participant2));
							}

							direction = (direction + 1) % 4;
							break;
						case SectionTypes.StartGrid:
							DrawTrackOnBackground(gr,
								drawParticipants(CreateManager.GetImage(_horizontalStart, Track.Width, Track.Height),
									participant1, participant2));
							break;
						case SectionTypes.Finish:
							DrawTrackOnBackground(gr,
								drawParticipants(CreateManager.GetImage(_horizontalFinnish, Track.Width, Track.Height),
									participant1, participant2));
							break;
						default:
							break;
					}

					if (direction == 0)
						y -= 1;
					if (direction == 1)
						x += 1;
					if (direction == 2)
						y += 1;
					if (direction == 3)
						x -= 1;
				}
			}
			return CreateManager.CreateBitmapSourceFromGdiBitmap(baseTrack);
		}

		private static void DrawTrackOnBackground(Graphics background, Bitmap image)
		{
			background.DrawImage(image, new Point((startx * Track.Width) + (x * Track.Width),
				(starty * Track.Height) + (y * Track.Height))
			);
		}

		private static void DrawParticipantOnTrack(Bitmap image1, Bitmap image2, int x, int y)
		{
			using (Graphics gr = Graphics.FromImage(image1))
			{
				gr.DrawImage(image2, new Point(x, y));
			}
		}

		public static string GetSnakeImagePath(TeamColors teamcolor)
		{
			switch (teamcolor)
			{
				case TeamColors.Blue:
					return _blueSnake;
				case TeamColors.Cyan:
					return _cyanSnake;
				case TeamColors.Lime:
					return _limeSnake;
				case TeamColors.Orange:
					return _orangeSnake;
				case TeamColors.Purple:
					return _purpleSnake;
				case TeamColors.Red:
					return _redSnake;
				case TeamColors.Violet:
					return _violetSnake;
				default:
					return "";
			}
		}

		private static Bitmap drawParticipants(Bitmap trackSection, IParticipant participant1,
			IParticipant participant2)
		{
			if (participant1 != null)
			{
				if (!participant1.Equiptment.IsBroken)
					DrawParticipantOnTrack(trackSection,
						RotateBitmap(CreateManager.GetImage(_scooter, 50, 50), direction * 90), 58, 12);
				else
					DrawParticipantOnTrack(trackSection,
						RotateBitmap(CreateManager.GetImage(_brokenScooter, 50, 50), direction * 90), 58, 12);
				DrawParticipantOnTrack(trackSection,
					RotateBitmap(CreateManager.GetImage(GetSnakeImagePath(participant1.TeamColor), 50, 50),
						direction * 90), 58, 12);
			}

			if (participant2 != null)
			{
				if (!participant1.Equiptment.IsBroken)
					DrawParticipantOnTrack(trackSection,
						RotateBitmap(CreateManager.GetImage(_scooter, 50, 50), direction * 90), 12, 58);
				else
					DrawParticipantOnTrack(trackSection,
						RotateBitmap(CreateManager.GetImage(_brokenScooter, 50, 50), direction * 90), 12, 58);
				DrawParticipantOnTrack(trackSection,
					RotateBitmap(CreateManager.GetImage(GetSnakeImagePath(participant2.TeamColor), 50, 50),
						direction * 90), 12, 58);
			}

			return trackSection;
		}

		private static Bitmap RotateBitmap(Bitmap source, float angle)
		{
			//Create a new empty bitmap to hold rotated image.
			Bitmap returnBitmap = new Bitmap(source.Width, source.Height);
			//Make a graphics object from the empty bitmap.
			Graphics g = Graphics.FromImage(returnBitmap);
			//move rotation point to center of image.
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			g.TranslateTransform((float) source.Width / 2, (float) source.Height / 2);
			g.RotateTransform(angle);
			//Move image back.
			g.TranslateTransform(-(float) source.Width / 2, -(float) source.Height / 2);
			//Draw passed in image onto graphics object.
			g.DrawImage(source, new Point(0, 0));
			return returnBitmap;
		}
	}
}