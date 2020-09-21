using System;
using System.Collections.Generic;
using System.Text;

namespace Model {
    public class Track {
        public string Name;
        LinkedList<Section> Sections;

        public Track() {
            Name = "";
            Sections = new LinkedList<Section>();
        }

        public Track(String name, LinkedList<Section> sections) {
            Name = name;
            Sections = sections;
        }
    }
}
