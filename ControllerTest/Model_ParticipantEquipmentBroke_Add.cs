using Model;
using NUnit.Framework;
using System.Collections.Generic;

namespace ControllerTest {
	public class Model_ParticipantEquipmentBroke_Add {

		[SetUp]
		public void Setup() { }

		[Test]
		public void Add_ToEmptyList() {
			Snake Participant = new Snake("Test", 0, new Scooter(), TeamColors.Blue);
			StatEquipmentBroke statEquipmentBroke = new StatEquipmentBroke(Participant);

			List<IParticipantStats> list = new List<IParticipantStats>();
			list.Add(statEquipmentBroke);

			List<IParticipantStats> list2 = new List<IParticipantStats>();
			statEquipmentBroke.Add(list2);

			Assert.AreEqual(list, list2);
		}

		[Test]
		public void Add_TwoSame_ValueIncrement() {
			Snake Participant = new Snake("Test", 0, new Scooter(), TeamColors.Blue);
			StatEquipmentBroke statEquipmentBroke = new StatEquipmentBroke(Participant);

			List<IParticipantStats> list = new List<IParticipantStats>();
			new StatEquipmentBroke(Participant).Add(list);
			new StatEquipmentBroke(Participant).Add(list);

			Assert.IsTrue(((StatEquipmentBroke)list[0]).EquipmentBroke == 2);
		}

		[Test]
		public void Add_TwoDistinct() {
			Snake Participant = new Snake("Test", 0, new Scooter(), TeamColors.Blue);
			Snake Participant2 = new Snake("Test2", 0, new Scooter(), TeamColors.Cyan);
			StatEquipmentBroke statEquipmentBroke = new StatEquipmentBroke(Participant);
			StatEquipmentBroke statEquipmentBroke2 = new StatEquipmentBroke(Participant2);

			// result
			List<IParticipantStats> list = new List<IParticipantStats>();
			list.Add(statEquipmentBroke);
			list.Add(statEquipmentBroke2);

			List<IParticipantStats> list2 = new List<IParticipantStats>();
			statEquipmentBroke.Add(list2);
			statEquipmentBroke2.Add(list2);

			Assert.AreEqual(list, list2);
		}
	}
}
