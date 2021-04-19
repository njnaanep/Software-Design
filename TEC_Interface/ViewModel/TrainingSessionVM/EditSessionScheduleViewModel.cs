using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using DataLayer.EfClasses;
using DataLayer.EfCode.Configuration;
using ServiceLayer;
using TEC_Interface.ViewModel.TrainingSession;

namespace TEC_Interface.ViewModel.TrainingSessionVM
{
    public class EditSessionScheduleViewModel
    {
        private ScheduleService _scheduleService;


        public TrainingSessionSchedule ScheduleToEdit { get; set; }

        public EditSessionScheduleViewModel(ScheduleService scheduleService, TrainingSessionSchedule scheduleToEdit)
        {
            _scheduleService = scheduleService;
            ScheduleToEdit = scheduleToEdit;

            DayList = new ObservableCollection<Day>(scheduleService.GetDays());

            SelectedDay = DayList.First(c => c.DayName == scheduleToEdit.Day);

            CopyEditableFields(scheduleToEdit);
        }

        private void CopyEditableFields(TrainingSessionSchedule selectedSchedule)
        {
            StartTime = DateTime.Parse(selectedSchedule.StartTime);
            EndTime = DateTime.Parse(selectedSchedule.EndTime);
        }

        public Day SelectedDay { get; set; }

        public ObservableCollection<Day> DayList { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }


        public void Edit()
        {
            var updatedSchedule = new Schedule
            {
                ScheduleId = ScheduleToEdit.ScheduleId,
                SessionId = ScheduleToEdit.SessionId,
                DayId = SelectedDay.DayId,
                StartTime = StartTime,
                EndTime = EndTime
            };

            ScheduleToEdit.Day = SelectedDay.DayName;
            ScheduleToEdit.StartTime = StartTime.ToShortTimeString();
            ScheduleToEdit.EndTime = EndTime.ToShortTimeString();

            _scheduleService.UpdateSchedule(updatedSchedule);
        }
    }
}