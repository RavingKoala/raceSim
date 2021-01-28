using Controller;
using Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace RaceVisuals {
	public class StatsScreenModel : INotifyPropertyChanged {
		public event PropertyChangedEventHandler PropertyChanged;

		public IEnumerable<string> PassesList { get => Data.Competition.Passes.GetList().Select(i => $"{((StatPasses)i).Passes} - {i.Name}"); }
		public IEnumerable<string> PointsList { get => Data.Competition.Points.GetList().Select(i => $"{((StatPoints)i).Points} - {i.Name}"); }
		public IEnumerable<string> BrokenVehicleList { get => Data.Competition.EquipmentBroke.GetList().Select(i => $"{((StatEquipmentBroke)i).EquipmentBroke} - {i.Name}"); }
		public IEnumerable<string> FastestLapList { get => Data.Competition.FastestLap.GetList().Select(i => $"{((StatFastestLap)i).FastestLapTime} - {i.Name}"); }
		public IEnumerable<string> LapTimeList { get => Data.Competition.LapTime.GetList().Select(i => $"{((StatLapTime)i).LapTime} - {i.Name}"); }

		public void OnDriverChanged(ParticipantChangedEventArgs participantChangedEventArgs) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
		}
	}
}
