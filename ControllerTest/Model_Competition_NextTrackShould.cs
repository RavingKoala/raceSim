﻿using NUnit.Framework.Internal;
using Model;
using NUnit.Framework;
using System.Collections.Generic;

namespace ControllerTest
{
    
    public class Model_Competition_NextTrackShould {
        private TestFixture testFixture { get; }
        private Competition _Competition { get; set; }

        [SetUp]
        public void Setup() {
            _Competition = new Competition();
        }

        [Test]
        public void NextTrack_EmptyQueue_ReturnNull() {
            Track result = _Competition.NextTrack();
            Assert.IsNull(result);
        }

        [Test]
        public void NextTrack_OneInQueue_ReturnTrack() {
            Competition testCompetitie = new Competition();

            Track track = new Track("expiriment track", new SectionTypes[] { SectionTypes.Straight });
            testCompetitie.Tracks.Enqueue(track);
            Track result = testCompetitie.NextTrack();
            Assert.AreEqual(track, result);
        }

        [Test]
        public void NextTrack_OneInQueue_RemoveTrackFromQueue() {
            Competition testCompetitie = new Competition();

            Track track = new Track("expiriment track", new SectionTypes[] { SectionTypes.Straight });
            testCompetitie.Tracks.Enqueue(track);

            Track result;
            result = testCompetitie.NextTrack();
            result = testCompetitie.NextTrack();
            Assert.IsNull(result);
        }
    }
}