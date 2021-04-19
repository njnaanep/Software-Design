using System;

namespace DataLayer.EfClasses
{
    public class Schedule
    {
        public string ScheduleId { get; set; }
        public string SessionId { get; set; }
        public TrainingSession TrainingSessionLink { get; set; }
        public string DayId { get; set; }
        public Day DayLink { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}