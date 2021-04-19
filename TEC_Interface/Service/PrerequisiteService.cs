using System.Linq;
using DataLayer.EfClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace ServiceLayer
{
    public class PrerequisiteService
    {
        private TecContext _context;

        public PrerequisiteService(TecContext context) => _context = context;

        public IQueryable<Prerequisite> GetQualificationPrerequisites(string qualificationCode) =>
            _context.Prerequisites.Where(c => c.QualificationCode == qualificationCode 
                                              && c.RequirementFor.ToLower() == "qualification"
                                              && c.IsDeleted == false)
                .Include(c => c.QualificationLink);

        public IQueryable<Prerequisite> GetCoursePrerequisites(string courseId) =>
            _context.Prerequisites.Where(c => c.CourseId == courseId 
                                              && c.RequirementFor.ToLower() == "course"
                                              && c.IsDeleted == false)
                .Include(c => c.QualificationLink);

        public void AddPrerequisite(Prerequisite prerequisite)
        {
            _context.Prerequisites.Add(prerequisite);
            _context.SaveChanges();
        }

        public void UpdatePrerequisite(string prerequisiteId, string courseId, string qualificationCode, string requiredFor)
        {
            var prerequisite =_context.Prerequisites.Find(prerequisiteId);
            prerequisite.CourseId = courseId;
            prerequisite.QualificationCode = qualificationCode;
            prerequisite.RequirementFor = requiredFor;
            
            _context.SaveChanges();
        }


        public void DeletePrerequisite(string prerequisiteId)
        {
            _context.Prerequisites.Find(prerequisiteId).IsDeleted = true;
            _context.SaveChanges();
        }

    }
}