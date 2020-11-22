using Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ControllerTest {
	public class Model_StatPoints_BesteSpeler {

		[Test]
		public void BesteSpeler_EmptyList() {
			Snake Participant = new Snake("Test", 0, new Scooter(), TeamColors.Blue);
			StatPoints statPoints = new StatPoints(Participant, 2);

			List<IParticipantStats> list = new List<IParticipantStats>();
			string result = statPoints.BesteSpeler(list);

			Assert.AreEqual(result, "");
		}

		[Test]
		public void BesteSpeler_OneInList() {
			Snake Participant = new Snake("Test", 0, new Scooter(), TeamColors.Blue);
			StatPoints statPoints = new StatPoints(Participant, 2);

			List<IParticipantStats> list = new List<IParticipantStats>();
			statPoints.Add(list);
			string result = statPoints.BesteSpeler(list);

			Assert.AreEqual(result, Participant.Name);
		}

		[Test]
		public void BesteSpeler_TwoInList_FirstIsBest() {
			Snake Participant = new Snake("Test", 0, new Scooter(), TeamColors.Blue);
			Snake Participant2 = new Snake("Test2", 0, new Scooter(), TeamColors.Blue);
			StatPoints statPoints = new StatPoints(Participant, 4);
			StatPoints statPoints2 = new StatPoints(Participant2, 2);

			List<IParticipantStats> list = new List<IParticipantStats>();
			statPoints.Add(list);
			statPoints2.Add(list);
			string result = statPoints.BesteSpeler(list);

			Assert.AreEqual(result, Participant.Name);
		}

		[Test]
		public void BesteSpeler_TwoInList_BothAreEqual() {
			Snake Participant = new Snake("Test", 0, new Scooter(), TeamColors.Blue);
			Snake Participant2 = new Snake("Test2", 0, new Scooter(), TeamColors.Blue);
			StatPoints statPoints = new StatPoints(Participant, 3);
			StatPoints statPoints2 = new StatPoints(Participant2, 3);

			List<IParticipantStats> list = new List<IParticipantStats>();
			statPoints.Add(list);
			statPoints2.Add(list);
			string result = statPoints.BesteSpeler(list);

			Assert.AreEqual(result, Participant.Name);
		}

		[Test]
		public void BesteSpeler_TwoInList_SecondIsBest() {
			Snake Participant = new Snake("Test", 0, new Scooter(), TeamColors.Blue);
			Snake Participant2 = new Snake("Test2", 0, new Scooter(), TeamColors.Blue);
			StatPoints statPoints = new StatPoints(Participant, 5);
			StatPoints statPoints2 = new StatPoints(Participant2, 7);

			List<IParticipantStats> list = new List<IParticipantStats>();
			statPoints.Add(list);
			statPoints2.Add(list);
			string result = statPoints.BesteSpeler(list);

			Assert.AreEqual(result, Participant2.Name);
		}
	}
}
