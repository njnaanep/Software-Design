using System;
using System.Linq;
using DataLayer.EfClasses;
using Microsoft.EntityFrameworkCore;

namespace TEC_Interface.Services
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

        public IQueryable<Opening> GetQualificationOpenings(string qualificationCode)
        {
            return _context.Openings.Where(c => c.IsDeleted == false
                && c.QualificationCode == qualificationCode)
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

        public void UpdateOpening(string openingNumber, string companyId, string qualificationCode, DateTime startingDate, DateTime anticipatedEndDate, double hourlyPay)
        {
            var opening = _context.Openings.Find(openingNumber);
            opening.CompanyId = companyId;
            opening.QualificationCode= qualificationCode;
            opening.StartingDate= startingDate;
            opening.AnticipatedEndDate= anticipatedEndDate;
            opening.HourlyPay = hourlyPay;

            _context.SaveChanges();
        }

        public void DeleteOpening(string openingNum)
        {
            var placements = _context.Placements.Where(c => c.OpeningNumber == openingNum);
            foreach (var placement in placements) 
                placement.IsDeleted = true;

            _context.Openings.Find(openingNum).IsDeleted = true;
            _context.SaveChanges();
        }

    }
}