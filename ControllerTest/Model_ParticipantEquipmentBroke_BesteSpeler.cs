using Model;
using NUnit.Framework;
using System.Collections.Generic;

namespace ControllerTest {
	public class Model_ParticipantEquipmentBroke_BesteSpeler {

		[SetUp]
		public void Setup() { }

		[Test]
		public void BesteSpeler_EmptyList() {
			Snake Participant = new Snake("Test", 0, new Scooter(), TeamColors.Blue);
			StatEquipmentBroke statEquipmentBroke = new StatEquipmentBroke(Participant);

			List<IParticipantStats> list = new List<IParticipantStats>();
			string result = statEquipmentBroke.BesteSpeler(list);

			Assert.AreEqual(result, "");
		}

		[Test]
		public void BesteSpeler_OneInList() {
			Snake Participant = new Snake("Test", 0, new Scooter(), TeamColors.Blue);
			StatEquipmentBroke statEquipmentBroke = new StatEquipmentBroke(Participant);

			List<IParticipantStats> list = new List<IParticipantStats>();
			statEquipmentBroke.Add(list);
			string result = statEquipmentBroke.BesteSpeler(list);

			Assert.AreEqual(result, Participant.Name);
		}

		[Test]
		public void BesteSpeler_TwoInList_FirstIsBest() {
			Snake Participant = new Snake("Test", 0, new Scooter(), TeamColors.Blue);
			Snake Participant2 = new Snake("Test2", 0, new Scooter(), TeamColors.Blue);
			StatEquipmentBroke statEquipmentBroke = new StatEquipmentBroke(Participant);
			StatEquipmentBroke statEquipmentBroke2 = new StatEquipmentBroke(Participant2);

			List<IParticipantStats> list = new List<IParticipantStats>();
			statEquipmentBroke.Add(list);
			statEquipmentBroke.Add(list);
			statEquipmentBroke2.Add(list);
			string result = statEquipmentBroke.BesteSpeler(list);

			Assert.AreEqual(result, Participant.Name);
		}

		[Test]
		public void BesteSpeler_TwoInList_SecondIsBest() {
			Snake Participant = new Snake("Test", 0, new Scooter(), TeamColors.Blue);
			Snake Participant2 = new Snake("Test2", 0, new Scooter(), TeamColors.Blue);
			StatEquipmentBroke statEquipmentBroke = new StatEquipmentBroke(Participant);
			StatEquipmentBroke statEquipmentBroke2 = new StatEquipmentBroke(Participant2);

			List<IParticipantStats> list = new List<IParticipantStats>();
			statEquipmentBroke.Add(list);
			statEquipmentBroke2.Add(list);
			statEquipmentBroke2.Add(list);
			string result = statEquipmentBroke.BesteSpeler(list);

			Assert.AreEqual(result, Participant2.Name);
		}
	}
}
