using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model {
    public class Track {
        public string Name { get; set; }
        public LinkedList<Section> Sections { get; set; }

        public Track(string name, SectionTypes[] sections) {
            Name = name;
            Sections = ConvertSectionTypes(sections);
        }

        private LinkedList<Section> ConvertSectionTypes(SectionTypes[] sections) {
            LinkedList<Section> returnValue = new LinkedList<Section>();
            foreach (SectionTypes sectionTypes in sections) {
                returnValue.AddLast(new Section(sectionTypes));
            }
            return returnValue;
        }
    }
}
