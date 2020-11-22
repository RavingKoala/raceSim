using Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControllerTest {
	public class Model_StatFinishTime_Add {

		[SetUp]
		public void Setup() { }

		[Test]
		public void Add_ToEmptyList() {
			Snake Participant = new Snake("Test", 0, new Scooter(), TeamColors.Blue);
			StatFinishTime statFinishTime = new StatFinishTime(Participant, new TimeSpan(0, 1, 0));

			List<IParticipantStats> list = new List<IParticipantStats>();
			list.Add(statFinishTime);

			List<IParticipantStats> list2 = new List<IParticipantStats>();
			statFinishTime.Add(list2);

			Assert.AreEqual(list, list2);
		}

		[Test]
		public void Add_DiffrentParticipants() {
			Snake Participant = new Snake("Test", 0, new Scooter(), TeamColors.Blue);
			Snake Participant2 = new Snake("Test2", 0, new Scooter(), TeamColors.Green);
			StatFinishTime statFinishTime = new StatFinishTime(Participant, new TimeSpan(0, 1, 0));
			StatFinishTime statFinishTime2 = new StatFinishTime(Participant2, new TimeSpan(0, 1, 0));

			// result
			List<IParticipantStats> list = new List<IParticipantStats>();
			list.Add(statFinishTime);
			list.Add(statFinishTime2);

			List<IParticipantStats> list2 = new List<IParticipantStats>();
			statFinishTime.Add(list2);
			statFinishTime2.Add(list2);

			Assert.AreEqual(list, list2);
		}

		[Test]
		public void Add_SameParticipant_TwoValues() {
			Snake Participant = new Snake("Test", 0, new Scooter(), TeamColors.Blue);

			List<IParticipantStats> list = new List<IParticipantStats>();
			new StatFinishTime(Participant, new TimeSpan(0, 1, 0)).Add(list);
			new StatFinishTime(Participant, new TimeSpan(0, 0, 30)).Add(list);

			Assert.IsTrue(list.Count == 2);
		}
	}
}
