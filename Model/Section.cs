﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Model {
    public class Section {
        public SectionTypes SectionType { get; }
        public static int length = 60;

        public Section(SectionTypes sectionType) {
            SectionType = sectionType;
        }
    }
}
