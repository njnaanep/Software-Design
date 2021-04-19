using System.Linq;
using DataLayer.EfClasses;

namespace TEC_Interface.Services
{
    public class QualificationService
    {
        private TecContext _context;

        public QualificationService(TecContext context) => _context = context;

        public IQueryable<Qualification> GetQualifications() => _context.Qualifications.Where(c => c.IsDeleted == false);

        public void AddQualification(Qualification qualification)
        {
            _context.Qualifications.Add(qualification);
            _context.SaveChanges();
        }

        public void UpdateQualification(string qualificationCode, string description)
        {
            var qualification = _context.Qualifications.Find(qualificationCode);
            qualification.Description = description;

            _context.SaveChanges();
        }

        public void DeleteQualification(string qualificationCode)
        {
            var prerequisites = _context.Prerequisites.Where(c => c.QualificationCode == qualificationCode);
            foreach (var prerequisite in prerequisites)
                prerequisite.IsDeleted = true;


            var certifications = _context.Certifications.Where(c => c.QualificationCode == qualificationCode);
            foreach (var certification in certifications) 
                certification.IsDeleted = true;


            var openings = _context.Openings.Where(c => c.QualificationCode == qualificationCode);
            foreach (var opening in openings)
            {
                var placements = _context.Placements.Where(c => c.OpeningNumber == opening.OpeningNumber);
                foreach (var placement in placements)
                    placement.IsDeleted = true;

                opening.IsDeleted = true;
            }

            _context.Qualifications.Find(qualificationCode).IsDeleted = true;
            _context.SaveChanges();
        }
    }
}