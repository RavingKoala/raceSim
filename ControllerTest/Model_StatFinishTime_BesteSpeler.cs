using Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ControllerTest {
	public class Model_StatFinishTime_BesteSpeler {

		[SetUp]
		public void Setup() { }

		[Test]
		public void BesteSpeler_EmptyList() {
			Snake Participant = new Snake("Test", 0, new Scooter(), TeamColors.Blue);
			StatFinishTime statFinishTime = new StatFinishTime(Participant, new TimeSpan(0, 1, 0));

			List<IParticipantStats> list = new List<IParticipantStats>();
			string result = statFinishTime.BesteSpeler(list);

			Assert.AreEqual(result, "");
		}

		[Test]
		public void BesteSpeler_OneInList() {
			Snake Participant = new Snake("Test", 0, new Scooter(), TeamColors.Blue);
			StatFinishTime statFinishTime = new StatFinishTime(Participant, new TimeSpan(0, 1, 0));

			List<IParticipantStats> list = new List<IParticipantStats>();
			statFinishTime.Add(list);
			string result = statFinishTime.BesteSpeler(list);

			Assert.AreEqual(result, Participant.Name);
		}

		[Test]
		public void BesteSpeler_TwoInList_FirstIsBest() {
			Snake Participant = new Snake("Test", 0, new Scooter(), TeamColors.Blue);
			Snake Participant2 = new Snake("Test2", 0, new Scooter(), TeamColors.Blue);
			StatFinishTime statFinishTime = new StatFinishTime(Participant, new TimeSpan(0, 0, 30));
			StatFinishTime statFinishTime2 = new StatFinishTime(Participant2, new TimeSpan(0, 1, 0));

			List<IParticipantStats> list = new List<IParticipantStats>();
			statFinishTime.Add(list);
			statFinishTime2.Add(list);
			string result = statFinishTime.BesteSpeler(list);

			Assert.AreEqual(result, Participant.Name);
		}



		[Test]
		public void BesteSpeler_TwoInList_SecondIsBest() {
			Snake Participant = new Snake("Test", 0, new Scooter(), TeamColors.Blue);
			Snake Participant2 = new Snake("Test2", 0, new Scooter(), TeamColors.Blue);
			StatFinishTime statFinishTime = new StatFinishTime(Participant, new TimeSpan(0, 2, 0));
			StatFinishTime statFinishTime2 = new StatFinishTime(Participant2, new TimeSpan(0, 1, 0));

			List<IParticipantStats> list = new List<IParticipantStats>();
			statFinishTime.Add(list);
			statFinishTime2.Add(list);
			string result = statFinishTime.BesteSpeler(list);

			Assert.AreEqual(result, Participant2.Name);
		}
	}
}
