using System;
using System.Linq;
using DataLayer.EfClasses;
using Microsoft.EntityFrameworkCore;

namespace ServiceLayer
{
    public class TrainingSessionService
    {
        private TecContext _context;

        public TrainingSessionService(TecContext context)
        {
            _context = context;
        }

        public IQueryable<TrainingSession> GetTrainingSessions()
        {
            return _context.TrainingSessions.Where(c => c.IsDeleted == false)
                .Include(c => c.CourseLink);
        }

        public IQueryable<TrainingSession> GetCourseTrainingSessions(string courseId)
        {
            return _context.TrainingSessions.Where(c => c.IsDeleted == false
                && c.CourseId == courseId)
                .Include(c => c.CourseLink);
        }


        public void AddTrainingSession(TrainingSession trainingSession)
        {
            _context.TrainingSessions.Add(trainingSession);
            _context.SaveChanges();
        }

        public void UpdateTrainingSession(string sessionId, string courseId, DateTime date, double fee)
        {
            var session = _context.TrainingSessions.Find(sessionId);

            session.CourseId = courseId;
            session.SessionDate = date;
            session.SessionFee = fee;
        }

        public void DeleteTrainingSessions(string sessionId)
        {
            var registrations = _context.TrainingSessions.Where(c => c.SessionId == sessionId);
            foreach (var trainingSession in registrations)
            {
                trainingSession.IsDeleted = true;
            }

            _context.TrainingSessions.Find(sessionId).IsDeleted = true;
            _context.SaveChanges();
        }
    }
}   