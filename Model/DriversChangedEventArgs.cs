using System;
using System.Collections.Generic;
using System.Text;

namespace Model {
	public delegate void DriversChanged(DriversChangedEventArgs driversChangedEventArgs);
	public class DriversChangedEventArgs : EventArgs {
		public Track track { get; set; }

		public DriversChangedEventArgs(Track track) {
			this.track = track;
		}
	}
}
