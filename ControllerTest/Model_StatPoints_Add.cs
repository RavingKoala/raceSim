using Model;
using NUnit.Framework;
using System.Collections.Generic;

namespace ControllerTest {
	public class Model_StatPoints_Add {

		[SetUp]
		public void Setup() { }

		[Test]
		public void Add_ToEmptyList() {
			Snake Participant = new Snake("Test", 0, new Scooter(), TeamColors.Blue);
			StatPoints statPoints = new StatPoints(Participant, 5);

			List<IParticipantStats> list = new List<IParticipantStats>();
			list.Add(statPoints);

			List<IParticipantStats> list2 = new List<IParticipantStats>();
			statPoints.Add(list2);

			Assert.AreEqual(list, list2);
		}

		[Test]
		public void Add_TwoSame() {
			Snake Participant = new Snake("Test", 0, new Scooter(), TeamColors.Blue);
			StatPoints statPoints = new StatPoints(Participant, 1);
			StatPoints statPoints2 = new StatPoints(Participant, 2);

			List<IParticipantStats> list = new List<IParticipantStats>();
			list.Add(statPoints);
			list.Add(statPoints2);

			List<IParticipantStats> list2 = new List<IParticipantStats>();
			statPoints.Add(list2);
			statPoints2.Add(list2);

			Assert.AreEqual(list, list2);
		}

		[Test]
		public void Add_TwoDistinct() {
			Snake Participant = new Snake("Test", 0, new Scooter(), TeamColors.Blue);
			Snake Participant2 = new Snake("Test2", 0, new Scooter(), TeamColors.Green);
			StatPoints statPoints = new StatPoints(Participant, 1);
			StatPoints statPoints2 = new StatPoints(Participant2, 2);

			// result
			List<IParticipantStats> list = new List<IParticipantStats>();
			list.Add(statPoints);
			list.Add(statPoints2);

			List<IParticipantStats> list2 = new List<IParticipantStats>();
			statPoints.Add(list2);
			statPoints2.Add(list2);

			Assert.AreEqual(list, list2);
		}
	}
}
