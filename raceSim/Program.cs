using System;
using System.Threading;
using ControllerTest;

namespace raceSim {
    class Program {
        static void Main(string[] args) {
            Data.Initialize();
            Data.NextRace();
            for ( ; ; ){
                Thread.Sleep(100);
            }
        }
    }
}
