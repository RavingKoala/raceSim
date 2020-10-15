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
			Visuals.DrawTrack(Data.CurrentRace.Track);
			Data.CurrentRace.Start();
			for (; ; ) {
				Thread.Sleep(100);
			}
		}
    }
}
