using System;
using System.Collections.Generic;
using System.Text;

namespace Model {
    public class Driver : IParticipant {

        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Points { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Equiptment { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public TeamColors TeamColor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Driver(string name, int points, int equiptment, TeamColors teamColor) {
            Name = name;
            Points = 0;
            Equiptment = equiptment;
            TeamColor = teamColor;
        }
    }
}
