using System;
using System.Collections.Generic;
using System.Text;

namespace Model {
	public class DriversChangedEventArgs : EventArgs {
		public Track track { get; set; }

		public DriversChangedEventArgs(Track track) {
			this.track = track;
		}
	}
}
