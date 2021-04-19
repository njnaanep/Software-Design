using System;
using System.Linq;
using DataLayer.EfClasses;
using Microsoft.EntityFrameworkCore;

namespace ServiceLayer
{
    public class OpeningService
    {
        private TecContext _context;

        public OpeningService(TecContext context) => _context = context;

        public IQueryable<Opening> GetOpenings()
        {
            return _context.Openings.Where(c => c.IsDeleted ==false)
                .Include(c => c.CompanyLink)
                .Include(c => c.QualificationLink);
        }

        public IQueryable<Opening> SearchOpenings(string searchString)
        {
            return _context.Openings.Where(c => c.IsDeleted == false
                && (c.CompanyLink.CompanyName.Contains(searchString) || c.QualificationLink.QualificationCode.Contains(searchString)))
                .Include(c => c.CompanyLink)
                .Include(c => c.QualificationLink);
        }
        public IQueryable<Opening> GetQualificationOpenings(string qualificationId)
        {
            return _context.Openings.Where(c => c.IsDeleted == false
                && c.QualificationId == qualificationId)
                .Include(c => c.CompanyLink);
        }

        public IQueryable<Opening> GetCompanyOpenings(string companyId)
        {
            return _context.Openings.Where(c => c.IsDeleted == false
                                                && c.CompanyId == companyId)
                .Include(c => c.QualificationLink);
        }


        public void AddOpening(Opening opening)
        {
            _context.Openings.Add(opening);
            _context.SaveChanges();
        }

        public void UpdateOpening(Opening opening)
        {
            var updatedOpening = _context.Openings.Find(opening.OpeningNumber);
            updatedOpening.CompanyId = opening.CompanyId;
            updatedOpening.QualificationId = opening.QualificationId;
            updatedOpening.StartingDate = opening.StartingDate;
            updatedOpening.AnticipatedEndDate = opening.AnticipatedEndDate;
            updatedOpening.HourlyPay = opening.HourlyPay;

            _context.SaveChanges();
        }
        public void DeleteOpening(string openingNum)
        {
            var placements = _context.Placements
                .Where(c => c.OpeningNumber == openingNum);
            foreach (var placement in placements) 
                placement.IsDeleted = true;

            _context.Openings.Find(openingNum).IsDeleted = true;
            _context.SaveChanges();
        }

    }
}