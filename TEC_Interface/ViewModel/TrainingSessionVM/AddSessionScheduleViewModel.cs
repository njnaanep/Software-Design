using System;
using System.Collections.ObjectModel;
using System.Linq;
using DataLayer.EfClasses;
using DataLayer.EfCode.Configuration;
using ServiceLayer;
using TEC_Interface.ViewModel.TrainingSession;

namespace TEC_Interface.ViewModel.TrainingSessionVM
{
    public class AddSessionScheduleViewModel
    {
        private ScheduleService _scheduleService;

        public TrainingSessionViewModel SelectedSession { get; set; }

        public AddSessionScheduleViewModel( ScheduleService scheduleService, TrainingSessionViewModel selectedSession)
        {
            _scheduleService = scheduleService;
            SelectedSession = selectedSession;

            DayList = new ObservableCollection<Day>(scheduleService.GetDays());

            SelectedDay = DayList.First();
        }

        public Day SelectedDay { get; set; }

        public ObservableCollection<Day> DayList { get; set; }


        public DateTime StartTime { get; set; } = DateTime.Parse("8:00 AM");

        public DateTime EndTime { get; set; } = DateTime.Parse("5:30 PM");

        public void Add()
        {
            var schedule = new Schedule
            {
                SessionId = SelectedSession.SessionId,
                DayId = SelectedDay.DayId,
                StartTime = StartTime,
                EndTime = EndTime
            };

            _scheduleService.AddSchedule(schedule);

            AssociatedSchedule = new TrainingSessionSchedule(schedule);
        }

        public TrainingSessionSchedule AssociatedSchedule { get; set; }
    }
}