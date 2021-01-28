using System;
using System.Collections.Generic;

namespace Model {
	public class Competition {
		public List<IParticipant> Participants { get; }
		public Queue<Track> Tracks { get; }
		public Stats<StatPoints> Points { get; }
		public Stats<StatPasses> Passes { get; }
		public Stats<StatFastestLap> FastestLap { get; }
		public Stats<StatFinishTime> FinishTime { get; }
		public Stats<StatLapTime> LapTime { get; }
		public Stats<StatEquipmentBroke> EquipmentBroke { get; }
		private int _participantsFinished { get; set; }
		public static int Laps = 3;

		public event UpdateStats UpdateStats;

		public Competition() {
			Participants = new List<IParticipant>();
			Tracks = new Queue<Track>();
			Points = new Stats<StatPoints>();
			Passes = new Stats<StatPasses>();
			FastestLap = new Stats<StatFastestLap>();
			FinishTime = new Stats<StatFinishTime>();
			LapTime = new Stats<StatLapTime>();
			EquipmentBroke = new Stats<StatEquipmentBroke>();
		}

		public void OnParticipantLapped(ParticipantLappedEventArgs ParticipantLappedEventArgs) {
			IParticipant participant = ParticipantLappedEventArgs.Participant;
			TimeSpan lapTime = ParticipantLappedEventArgs.LapTime;
			LapTime.Add(new StatLapTime(participant, lapTime));
			FastestLap.Add(new StatFastestLap(participant, lapTime));
		}

		public void OnParticipantFinished(ParticipantFinishedEventArgs participantFinishedEventArgs) {
			IParticipant participant = participantFinishedEventArgs.Participant;
			int points = Participants.Count - _participantsFinished;
			Points.Add(new StatPoints(participant, points));
			_participantsFinished++;
		}

		public void OnParticipantPassed(ParticipantPassedEventArgs participantPassedEventArgs) {
			IParticipant participant = participantPassedEventArgs.Participant;
			Passes.Add(new StatPasses(participant));
			UpdateStats?.Invoke(new UpdateStatsEventArgs(Passes.BesteSpeler(), StatTypes.Passes));
		}

		public void OnParticipantEquipmentBroke(ParticipantEquipmentBrokeEventArgs participantEquipmentBrokeEventArgs) {
			IParticipant participant = participantEquipmentBrokeEventArgs.Participant;
			EquipmentBroke.Add(new StatEquipmentBroke(participant));
			UpdateStats?.Invoke(new UpdateStatsEventArgs(EquipmentBroke.BesteSpeler(), StatTypes.EquipmentBroke));
		}

		public Track NextTrack() {
			_participantsFinished = 0;
			return Tracks.Count > 0 ? Tracks.Dequeue() : null;
		}
	}
}
