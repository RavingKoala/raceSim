using System;
using System.Collections.Generic;
using System.Text;

namespace Model {
    public class Track {
        string Name;
        LinkedList<Section> Sections;

        Track NextTrack(string name, SectionTypes[] Sections) {
            return new Track();
        }

        public Track() {
            Name = "";
            Sections = new LinkedList<Section>();
        }
    }
}
