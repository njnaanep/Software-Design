using System.Linq;
using DataLayer.EfClasses;
using Microsoft.EntityFrameworkCore;

namespace ServiceLayer
{
    public class ScheduleService
    {
        private TecContext _context;

        public ScheduleService(TecContext context) => _context = context;

        public static IQueryable<Schedule> GetSchedule
            => new TecContext().Schedules.Where(c => !c.IsDeleted)
                .Include(c => c.TrainingSessionLink)
                .Include(c => c.DayLink);

        public IQueryable<Day> GetDays() => _context.Days;

        public static IQueryable<Schedule> GetTrainingSchedule(string sessionId)
            => GetSchedule.Where(c => c.SessionId.Equals(sessionId));


        public void AddSchedule(Schedule schedule)
        {
            _context.Schedules.Add(schedule);
            _context.SaveChanges();
        }


        public void UpdateSchedule(Schedule updatedSchedule)
        {
            var newSchedule = _context.Schedules.Find(updatedSchedule.ScheduleId);
            newSchedule.DayId = updatedSchedule.DayId;
            newSchedule.StartTime = updatedSchedule.StartTime;
            newSchedule.EndTime = updatedSchedule.EndTime;
        }


        public void DeleteSchedule(string scheduleId)
        {
            _context.Schedules.Find(scheduleId).IsDeleted = true;
            _context.SaveChanges();
        }
    }
}