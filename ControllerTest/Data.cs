using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace ControllerTest {
    static class Data {
        static Competition competition;

        static void Initialize() {
            competition = new Competition();
            FillParticipants();
        }

        static void FillParticipants() {
            competition.Participants.Add(new Driver("steve", 0, 0, TeamColors.Blue));
            competition.Tracks.Enqueue(new Track());
        }
    }
}
