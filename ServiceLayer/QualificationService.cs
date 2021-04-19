using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using DataLayer.EfClasses;

namespace ServiceLayer
{
    public class QualificationService
    {
        private TecContext _context;

        public QualificationService(TecContext context) => _context = context;

        public IQueryable<Qualification> GetQualifications()
            => _context.Qualifications.Where(c => !c.IsDeleted);

        public IQueryable<Qualification> GetUnrelatedQualifications(string courseId)
        {
            var requiredQualifications = _context.Prerequisites
                .Where(c => !c.IsDeleted && c.RequirementFor.ToLower() == "course"
                            && c.CourseId == courseId)
                .Select(c => c.QualificationId).ToList();

            
            return GetQualifications().Where(q  => !requiredQualifications.Any(c =>
                    c.Equals(q.QualificationId))).AsQueryable();

        }


        public IQueryable<Qualification> SearchQualificationsQuery(IQueryable<Qualification> queryable, string search)
            => queryable.Where(c => c.QualificationCode.Contains(search) ||
                                    c.Description.Contains(search));

        public IQueryable<Qualification> SearchQualification(string search) =>
            SearchQualificationsQuery(GetQualifications(), search);

        public IQueryable<Qualification> SearchUnrelatedQualification(string courseId, string search) =>
            SearchQualificationsQuery(GetUnrelatedQualifications(courseId), search);

        public void AddQualification(Qualification qualification)
        {
            _context.Qualifications.Add(qualification);
            _context.SaveChanges();
        }



        public void UpdateQualification(Qualification qualification)
        {
            var updatedQualification = _context.Qualifications.Find(qualification.QualificationId);
            updatedQualification.QualificationCode = qualification.QualificationCode;
            updatedQualification.Description = qualification.Description;

            _context.SaveChanges();
        }
        
        public void DeleteQualification(string qualificationId)
        {
            _context.Qualifications.Find(qualificationId).IsDeleted = true;
            _context.SaveChanges();
        }
    }
}