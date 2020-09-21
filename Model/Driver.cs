using System;
using System.Collections.Generic;
using System.Text;

namespace Model {
    public class Driver : IParticipant {
        private string name;
        public int points;
        public IEquipment equiptment;
        public TeamColors teamColor;
        public string Name { get => name; set { name = value; } }
        public int Points { get => points; set { points = value; } }
        public IEquipment Equiptment { get => equiptment; set { equiptment = value; } }
        public TeamColors TeamColor { get => teamColor; set { teamColor = value; } }

        public Driver(string name, int points, IEquipment equiptment, TeamColors teamColor) {
            Name = name;
            Points = 0;
            Equiptment = equiptment;
            TeamColor = teamColor;
        }
    }
}
