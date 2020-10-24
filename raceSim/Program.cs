using System;
using System.Threading;
using ControllerTest;
using Model;

namespace raceSim {
    class Program {
        static void Main(string[] args) {
			Data.Initialize();
			Data.NextRace();
			Data.CurrentRace.driverChanged += Visuals.DriversChanged;
			Data.CurrentRace.raceFinished += StartNextRace;
			Visuals.Initialize(Data.CurrentRace.Track);
			Visuals.DrawTrack(Data.CurrentRace.Track);
			Data.CurrentRace.Start();
			for (; ; ) {
				Thread.Sleep(100);
			}
		}

		static void StartNextRace(object Sender, EventArgs args) {
			Data.CurrentRace.ClearEvents();
			Console.Clear();
			
			Data.NextRace();
			Data.CurrentRace.driverChanged += Visuals.DriversChanged;
			Data.CurrentRace.raceFinished += StartNextRace;
			Visuals.Initialize(Data.CurrentRace.Track);
			Visuals.DrawTrack(Data.CurrentRace.Track);
			Data.CurrentRace.Start();
		}
    }
}
