using Model;
using NUnit.Framework;
using System.Collections.Generic;

namespace ControllerTest {
	public class Model_StatPasses_Add {

		[SetUp]
		public void Setup() { }

		[Test]
		public void Add_ToEmptyList() {
			Snake Participant = new Snake("Test", 0, new Scooter(), TeamColors.Blue);
			StatPasses statPasses = new StatPasses(Participant);

			List<IParticipantStats> list = new List<IParticipantStats>();
			list.Add(statPasses);

			List<IParticipantStats> list2 = new List<IParticipantStats>();
			statPasses.Add(list2);

			Assert.AreEqual(list, list2);
		}

		[Test]
		public void Add_TwoSame_ValueIncrement() {
			Snake Participant = new Snake("Test", 0, new Scooter(), TeamColors.Blue);
			StatPasses statPasses = new StatPasses(Participant);

			List<IParticipantStats> list = new List<IParticipantStats>();
			new StatPasses(Participant).Add(list);
			new StatPasses(Participant).Add(list);

			Assert.IsTrue(((StatPasses)list[0]).Passes == 2);
		}

		[Test]
		public void Add_TwoDistinct() {
			Snake Participant = new Snake("Test", 0, new Scooter(), TeamColors.Blue);
			Snake Participant2 = new Snake("Test2", 0, new Scooter(), TeamColors.Green);
			StatPasses statPasses = new StatPasses(Participant);
			StatPasses statPasses2 = new StatPasses(Participant2);

			// result
			List<IParticipantStats> list = new List<IParticipantStats>();
			list.Add(statPasses);
			list.Add(statPasses2);

			List<IParticipantStats> list2 = new List<IParticipantStats>();
			statPasses.Add(list2);
			statPasses2.Add(list2);

			Assert.AreEqual(list, list2);
		}
	}
}
