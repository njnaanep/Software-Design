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
            return _context.TrainingSessions.Where(c => !c.IsDeleted)
                .Include(c => c.CourseLink);
        }

        public IQueryable<TrainingSession> GetCourseTrainingSessions(string courseId) 
            => GetTrainingSessions().Where(c => c.CourseId == courseId);


        public void AddTrainingSession(TrainingSession trainingSession)
        {
            _context.TrainingSessions.Add(trainingSession);
            _context.SaveChanges();
        }

        public void UpdateTrainingSession(TrainingSession session)
        {
            var updatedSession = _context.TrainingSessions.Find(session.SessionId);

            updatedSession.CourseId = session.CourseId;
            updatedSession.SessionFee = session.SessionFee;
            updatedSession.SessionCapacity = session.SessionCapacity;
            _context.SaveChanges();
        }
        public void DeleteTrainingSessions(string sessionId)
        {
            _context.TrainingSessions.Find(sessionId).IsDeleted = true;
            _context.SaveChanges();
        }
    }
}   