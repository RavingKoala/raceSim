using Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ControllerTest {
	public class Model_StatFastestLap_Add {

		[SetUp]
		public void Setup() { }

		[Test]
		public void Add_ToEmptyList() {
			Snake Participant = new Snake("Test", 0, new Scooter(), TeamColors.Blue);
			StatFastestLap statFastestLap = new StatFastestLap(Participant, new TimeSpan(0, 1, 0));

			List<IParticipantStats> list = new List<IParticipantStats>();
			list.Add(statFastestLap);

			List<IParticipantStats> list2 = new List<IParticipantStats>();
			statFastestLap.Add(list2);

			Assert.AreEqual(list, list2);
		}

		[Test]
		public void Add_DiffrentParticipants() {
			Snake Participant = new Snake("Test", 0, new Scooter(), TeamColors.Blue);
			Snake Participant2 = new Snake("Test2", 0, new Scooter(), TeamColors.Green);
			StatFastestLap statFastestLap = new StatFastestLap(Participant, new TimeSpan(0, 1, 0));
			StatFastestLap statFastestLap2 = new StatFastestLap(Participant2, new TimeSpan(0, 1, 0));

			// result
			List<IParticipantStats> list = new List<IParticipantStats>();
			list.Add(statFastestLap);
			list.Add(statFastestLap2);

			List<IParticipantStats> list2 = new List<IParticipantStats>();
			statFastestLap.Add(list2);
			statFastestLap2.Add(list2);

			Assert.AreEqual(list, list2);
		}

		[Test]
		public void Add_SameParticipant_WorseValue() {
			Snake Participant = new Snake("Test", 0, new Scooter(), TeamColors.Blue);

			List<IParticipantStats> list = new List<IParticipantStats>();
			new StatFastestLap(Participant, new TimeSpan(0, 1, 0)).Add(list);
			new StatFastestLap(Participant, new TimeSpan(0, 0, 30)).Add(list);

			Assert.AreEqual(((StatFastestLap) list[0]).FastestLapTime, new TimeSpan(0, 1, 0));
		}

		[Test]
		public void Add_SameParticipant_SameValue() {
			Snake Participant = new Snake("Test", 0, new Scooter(), TeamColors.Blue);

			List<IParticipantStats> list = new List<IParticipantStats>();
			new StatFastestLap(Participant, new TimeSpan(0, 1, 0)).Add(list);
			new StatFastestLap(Participant, new TimeSpan(0, 1, 0)).Add(list);

			Assert.AreEqual(((StatFastestLap)list[0]).FastestLapTime, new TimeSpan(0, 1, 0));
		}

		[Test]
		public void Add_SameParticipant_BetterValue() {
			Snake Participant = new Snake("Test", 0, new Scooter(), TeamColors.Blue);

			List<IParticipantStats> list = new List<IParticipantStats>();
			new StatFastestLap(Participant, new TimeSpan(0, 1, 0)).Add(list);
			new StatFastestLap(Participant, new TimeSpan(0, 2, 0)).Add(list);

			Assert.AreEqual(((StatFastestLap)list[0]).FastestLapTime, new TimeSpan(0, 2, 0));
		}
	}
}
