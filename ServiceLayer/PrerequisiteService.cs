using System.Collections.Generic;
using System.Linq;
using DataLayer.EfClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace ServiceLayer
{
    public class PrerequisiteService
    {
        public static IQueryable<Prerequisite> GetPrerequisites() => new TecContext().Prerequisites.Where(c => !c.IsDeleted)
            .Include(c => c.CourseLink)
            .Include(c => c.QualificationLink);

        private TecContext _context;

        public PrerequisiteService(TecContext context) => _context = context;

        public static IQueryable<Prerequisite> GetQualificationPrerequisites(string qualificationId) => GetPrerequisites()
            .Where(c => c.QualificationId == qualificationId && c.RequirementFor.ToLower() == "qualification");

        public static IQueryable<Prerequisite> GetCoursePrerequisites(string courseId) => GetPrerequisites()
            .Where(c => c.CourseId == courseId && c.RequirementFor.ToLower() == "course");

        public void AddPrerequisite(Prerequisite prerequisite)
        {
            _context.Prerequisites.Add(prerequisite);
            _context.SaveChanges();
        }

        public void UpdatePrerequisite(Prerequisite prerequisite)
        {
            var updatedPrerequisite = _context.Prerequisites.Find(prerequisite.PrerequisiteId);
            updatedPrerequisite.CourseId = prerequisite.CourseId;
            updatedPrerequisite.QualificationId= prerequisite.QualificationId;
            updatedPrerequisite.RequirementFor = prerequisite.RequirementFor;

            _context.SaveChanges();
        }

        public void DeletePrerequisite(string prerequisiteId)
        {
            _context.Prerequisites.Find(prerequisiteId).IsDeleted = true;
            _context.SaveChanges();
        }

    }
}