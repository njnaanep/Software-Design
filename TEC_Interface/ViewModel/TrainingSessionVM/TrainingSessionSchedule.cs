using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DataLayer.EfClasses;
using TEC_Interface.Annotations;

namespace TEC_Interface.ViewModel.TrainingSessionVM
{
    public class TrainingSessionSchedule : INotifyPropertyChanged
    {
        private string _scheduleId;
        private string _day;
        private string _startTime;
        private string _endTime;
        private string _sessionId;

        public string ScheduleId
        {
            get => _scheduleId;
            internal set
            {
                _scheduleId = value;
                OnPropertyChanged(nameof(ScheduleId));
            }
        }

        public string SessionId
        {
            get => _sessionId;
            internal set
            {
                _sessionId = value;
                OnPropertyChanged(nameof(SessionId));
            }
        }

        public string Day
        {
            get => _day;
            set
            {
                _day = value;
                OnPropertyChanged(nameof(Day));
            }
        }

        public string StartTime
        {
            get => _startTime;
            set
            {
                _startTime = value;
                OnPropertyChanged(nameof(StartTime));
                OnPropertyChanged(nameof(Duration));
            }
        }

        public string EndTime
        {
            get => _endTime;
            set
            {
                _endTime = value;
                OnPropertyChanged(nameof(EndTime));
                OnPropertyChanged(nameof(Duration));
            }
        }

        public string Duration
        {
            get
            {
                var duration = DateTime.Parse(EndTime).Subtract(DateTime.Parse(StartTime));

                return $"{duration.Hours} hrs and {duration.Minutes} mins";
            }
        }

        public TrainingSessionSchedule(Schedule schedule)
        {
            SessionId = schedule.SessionId;
            ScheduleId = schedule.ScheduleId;
            Day = schedule.DayLink.DayName;
            StartTime = schedule.StartTime.ToShortTimeString();
            EndTime = schedule.EndTime.ToShortTimeString();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}