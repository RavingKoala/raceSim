﻿namespace Model {
	public class Section {
		public SectionTypes SectionType { get; }
		public static int length = 120;

		public Section(SectionTypes sectionType) {
			SectionType = sectionType;
		}
	}
}
