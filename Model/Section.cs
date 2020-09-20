using System;
using System.Collections.Generic;
using System.Text;

namespace Model {
    public class Section {
        SectionTypes SectionType;

        public Section(SectionTypes sectionType) {
            SectionType = sectionType;
        }

        public Section() {
            SectionType = SectionTypes.Straight;
        }
    }
}
