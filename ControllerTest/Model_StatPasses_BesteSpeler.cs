using Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ControllerTest {
	public class Model_StatPasses_BesteSpeler {

		[SetUp]
		public void Setup() { }

		[Test]
		public void BesteSpeler_EmptyList() {
			Snake Participant = new Snake("Test", 0, new Scooter(), TeamColors.Blue);
			StatPasses statPasses = new StatPasses(Participant);

			List<IParticipantStats> list = new List<IParticipantStats>();
			string result = statPasses.BesteSpeler(list);

			Assert.AreEqual(result, "");
		}

		[Test]
		public void BesteSpeler_OneInList() {
			Snake Participant = new Snake("Test", 0, new Scooter(), TeamColors.Blue);
			StatPasses statPasses = new StatPasses(Participant);

			List<IParticipantStats> list = new List<IParticipantStats>();
			statPasses.Add(list);
			string result = statPasses.BesteSpeler(list);

			Assert.AreEqual(result, Participant.Name);
		}

		[Test]
		public void BesteSpeler_TwoInList_FirstIsBest() {
			Snake Participant = new Snake("Test", 0, new Scooter(), TeamColors.Blue);
			Snake Participant2 = new Snake("Test2", 0, new Scooter(), TeamColors.Blue);
			StatPasses statPasses = new StatPasses(Participant);
			StatPasses statPasses2 = new StatPasses(Participant2);

			List<IParticipantStats> list = new List<IParticipantStats>();
			statPasses.Add(list);
			statPasses.Add(list);
			statPasses2.Add(list);
			string result = statPasses.BesteSpeler(list);

			Assert.AreEqual(result, Participant.Name);
		}

		[Test]
		public void BesteSpeler_TwoInList_SecondIsBest() {
			Snake Participant = new Snake("Test", 0, new Scooter(), TeamColors.Blue);
			Snake Participant2 = new Snake("Test2", 0, new Scooter(), TeamColors.Blue);
			StatPasses statPasses = new StatPasses(Participant);
			StatPasses statPasses2 = new StatPasses(Participant2);

			List<IParticipantStats> list = new List<IParticipantStats>();
			statPasses.Add(list);
			statPasses2.Add(list);
			statPasses2.Add(list);
			string result = statPasses.BesteSpeler(list);

			Assert.AreEqual(result, Participant2.Name);
		}
	}
}
