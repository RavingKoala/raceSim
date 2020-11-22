using System;
using System.Threading;
using ControllerTest;

namespace raceSim {
	class Program {
		//TODO: perfect race.movePlayers() method
		//TODO: make all variables either dutch or english
		//TODO: make all private properties, fields
		//TODO: fix known bug participant lapped has to skip the first finish line
		static void Main(string[] args) {
			Data.Initialize();
			Data.NextRace();
			ConnectEvents();
			Data.Competition.UpdateStats += Visuals.DrawStats;
			Visuals.Initialize(Data.CurrentRace.Track);
			Visuals.DrawTrack(Data.CurrentRace.Track);
			Data.CurrentRace.Start();
			for (;;) {
				Thread.Sleep(100);
			}
		}

		private static void StartNextRace(object Sender, EventArgs args) {
			Data.CurrentRace.ClearEvents();
			Visuals.ClearTrack();

			Data.NextRace();
			ConnectEvents();
			Visuals.Initialize(Data.CurrentRace.Track);
			Visuals.DrawTrack(Data.CurrentRace.Track);
			Data.CurrentRace.Start();
		}

		private static void ConnectEvents() {
			Data.CurrentRace.DriverChanged += Visuals.DriversChanged;
			Data.CurrentRace.ParticipantFinished += Data.Competition.OnParticipantFinished;
			Data.CurrentRace.ParticipantLapped += Data.Competition.OnParticipantLapped;
			Data.CurrentRace.ParticipantPassed += Data.Competition.OnParticipantPassed;
			Data.CurrentRace.ParticipantEquipmentBroke += Data.Competition.OnParticipantEquipmentBroke;
			Data.CurrentRace.RaceFinished += StartNextRace;

		}
	}
}
