using Controller;
using Model;
using System.ComponentModel;

namespace RaceVisuals {
	public class RaceScreenModel : INotifyPropertyChanged {
		public event PropertyChangedEventHandler PropertyChanged;
		public string RaceName { get => Data.CurrentRace?.Track?.Name; }

		public void OnDriverChanged(ParticipantChangedEventArgs participantChangedEventArgs) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
		}
	}
}
