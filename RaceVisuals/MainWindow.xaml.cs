using Controller;
using Model;
using System;
using System.Windows;
using System.Windows.Threading;

namespace RaceVisuals {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		private CompetitionStatsScreen _competitionStatsScreen = new CompetitionStatsScreen();
		private RaceStatsScreen _raceStatsScreen = new RaceStatsScreen();

		private StatsScreenModel _statsScreenModel = new StatsScreenModel();
		private RaceScreenModel _raceScreenModel = new RaceScreenModel();

		public MainWindow() {
			InitializeComponent();
			InitializeData();
			this.DataContext = _raceScreenModel;
			_competitionStatsScreen.DataContext = _statsScreenModel;
			_raceStatsScreen.DataContext = _statsScreenModel;
		}

		private void InitializeData() {
			Data.Initialize();
			Data.NextRace();
			Visuals.Initialize(Data.CurrentRace.Track);
			ConnectEvents();
			Data.CurrentRace.Start();
		}

		private void StartNextRace(object Sender, EventArgs args) {
			CreateManager.ClearCashe();
			Data.Competition.LapTime.ResetList();
			Data.Competition.FastestLap.ResetList();
			Data.Competition.EquipmentBroke.ResetList();
			Data.CurrentRace.ClearEvents();
			Visuals.Initialize(Data.CurrentRace.Track);
			Data.NextRace();
			ConnectEvents();
			Data.CurrentRace.Start();
		}

		private void ConnectEvents() {
			Data.CurrentRace.DriverChanged += OnDriverChanged;
			Data.CurrentRace.DriverChanged += _raceScreenModel.OnDriverChanged;
			Data.CurrentRace.DriverChanged += _statsScreenModel.OnDriverChanged;
			Data.CurrentRace.ParticipantFinished += Data.Competition.OnParticipantFinished;
			Data.CurrentRace.ParticipantLapped += Data.Competition.OnParticipantLapped;
			Data.CurrentRace.ParticipantPassed += Data.Competition.OnParticipantPassed;
			Data.CurrentRace.ParticipantEquipmentBroke += Data.Competition.OnParticipantEquipmentBroke;
			Data.CurrentRace.RaceFinished += StartNextRace;
		}

		public void OnDriverChanged(ParticipantChangedEventArgs participantChangedEventArgs) {
			Race.Dispatcher.BeginInvoke(
					DispatcherPriority.Render,
					new Action(() => {
						this.Race.Source = null;
						this.Race.Source = Visuals.DrawTrack(Data.CurrentRace.Track, (int)this.Width, (int)this.Height);
					})
				);
		}

		private void MenuItem_CompetitionScreen_Click(object sender, RoutedEventArgs e) {
			_competitionStatsScreen.Show();
		}

		private void MenuItem_RaceScreen_Click(object sender, RoutedEventArgs e) {
			_raceStatsScreen.Show();
		}

		private void MenuItem_Exit_Click(object sender, RoutedEventArgs e) {
			this.Close();
			Application.Current.Shutdown();
		}
	}
}
