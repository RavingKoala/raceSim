using System;
using System.Threading;
using ControllerTest;

namespace raceSim {
    class Program {
        static void Main(string[] args) {
            Visuals.DrawTrack();
            Data.NextRace();
            for ( ; ; ){
                Thread.Sleep(100);
            }
        }
    }
}
