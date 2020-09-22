using System;
using System.Collections.Generic;
using System.Text;

namespace raceSim
{

    public static class Visuals {
        #region graphics
        private static string[] _finnishHorizontal = { "----", "  # ", "  # ", "----" };
        private static string[] _horizontal = { "----", "    ", "    ", "----" };
        private static string[] _vertical = { "|  |", "|  |", "|  |", "|  |" };
        private static string[] _northToEast = { "|  \\", "\\   ", " \\  ", "  \\-" };
        private static string[] _eastToSouth = { "  /-", " /  ", "/   ", "|  /" };
        private static string[] _southToWest = { "-\\  ", "  \\ ", "   \\", "\\  |" };
        private static string[] _westToNorth = { "/  |", "  / ", " /  ", "-/  " };
        #endregion


        public static void Initialize() { }
        public static void DrawTrack() {

        }
    }

}
