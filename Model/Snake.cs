using System;
using System.Collections.Generic;
using System.Text;

namespace Model {
    public class Snake : IParticipant {
        private string name;
        private int points;
        private IEquipment equiptment;
        private TeamColors teamColor;
        public string Name { get => name; set { name = value; } }
        public int Points { get => points; set { points = value; } }
        public IEquipment Equiptment { get => equiptment; set { equiptment = value; } }
        public TeamColors TeamColor { get => teamColor; set { teamColor = value; } }

        public Snake(string name, int points, IEquipment equiptment, TeamColors teamColor) {
            Name = name;
            Points = points;
            Equiptment = equiptment;
            TeamColor = teamColor;
        }
    }
}
