using Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ControllerTest {
	public class Model_StatLapTime_Add {

		[SetUp]
		public void Setup() { }

		[Test]
		public void Add_ToEmptyList() {
			Snake Participant = new Snake("Test", 0, new Scooter(), TeamColors.Blue);
			StatLapTime statLapTime = new StatLapTime(Participant, new TimeSpan(0, 1, 0));

			List<IParticipantStats> list = new List<IParticipantStats>();
			list.Add(statLapTime);

			List<IParticipantStats> list2 = new List<IParticipantStats>();
			statLapTime.Add(list2);

			Assert.AreEqual(list, list2);
		}

		[Test]
		public void Add_DiffrentParticipants() {
			Snake Participant = new Snake("Test", 0, new Scooter(), TeamColors.Blue);
			Snake Participant2 = new Snake("Test2", 0, new Scooter(), TeamColors.Cyan);
			StatLapTime statLapTime = new StatLapTime(Participant, new TimeSpan(0, 1, 0));
			StatLapTime statLapTime2 = new StatLapTime(Participant2, new TimeSpan(0, 1, 0));

			// result
			List<IParticipantStats> list = new List<IParticipantStats>();
			list.Add(statLapTime);
			list.Add(statLapTime2);

			List<IParticipantStats> list2 = new List<IParticipantStats>();
			statLapTime.Add(list2);
			statLapTime2.Add(list2);

			Assert.AreEqual(list, list2);
		}
		[Test]
		public void Add_SameParticipant_TwoValues() {
			Snake Participant = new Snake("Test", 0, new Scooter(), TeamColors.Blue);

			List<IParticipantStats> list = new List<IParticipantStats>();
			new StatLapTime(Participant, new TimeSpan(0, 1, 0)).Add(list);
			new StatLapTime(Participant, new TimeSpan(0, 0, 30)).Add(list);

			Assert.IsTrue(list.Count == 2);
		}
	}
}
