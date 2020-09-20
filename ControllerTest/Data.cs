using System;
using System.Collections.Generic;
using Model;

namespace ControllerTest {
    static class Data {
        static Competition competition;

        static void Initialize() {
            competition = new Competition();
            FillParticipants();
            AddTracks();
            Console.WriteLine("test");
        }

        static void FillParticipants() {
            competition.Participants.Add(new Driver("steve", 0, 0, TeamColors.Blue));
            competition.Participants.Add(new Driver("bob", 0, 0, TeamColors.Red));
        }

        static void AddTracks(){
            LinkedList<Section> sections = new LinkedList<Section>();
            sections.AddFirst(new Section(SectionTypes.Straight));
            sections.AddFirst(new Section(SectionTypes.LeftCorner));
            competition.Tracks.Enqueue(new Track("", sections));
        }
    }
}