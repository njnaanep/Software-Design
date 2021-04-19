using System.Linq;
using DataLayer.EfClasses;
using Microsoft.EntityFrameworkCore;

namespace TEC_Interface.Services
{
    public class PlacementService
    {
        private TecContext _context;

        public PlacementService(TecContext context) => _context = context;

        public IQueryable<Placement> GetPlacements()
        {
            return _context.Placements.Where(c => c.IsDeleted == false)
                .Include(c => c.CandidateLink)
                .Include(c => c.OpeningLink)
                .ThenInclude(c => c.CompanyLink);
        }

        public IQueryable<Placement> GetQualifiedPlacements()
        {
            return _context.Placements.Where(c => c.IsDeleted == false && c.PlacementStatus.ToLower().Equals("qualified"))
                .Include(c => c.CandidateLink)
                .Include(c => c.OpeningLink);
        }

        public IQueryable<Placement> GetPendingPlacements()
        {
            return _context.Placements.Where(c => c.IsDeleted == false && c.PlacementStatus.ToLower().Equals("pending"))
                .Include(c => c.CandidateLink)
                .Include(c => c.OpeningLink);
        }

        public IQueryable<Placement> GetOpeningPlacements(string openingNum) =>
            _context.Placements.Where(c => c.IsDeleted == false && c.OpeningNumber == openingNum)
                .Include(c => c.CandidateLink);


        public IQueryable<Placement> GetCandidatePlacements(string candidateNum) =>
         _context.Placements.Where(c => c.IsDeleted == false && c.CandidateNumber == candidateNum)
             .Include(c => c.OpeningLink)
             .ThenInclude(c => c.CompanyLink);


        public void AddPlacement(Placement placement)
        {
            _context.Placements.Add(placement);
            _context.SaveChanges();
        }

        public void QualifyPlacement(string placementId)
        {
            _context.Placements.Find(placementId).PlacementStatus = "Qualified";
            _context.SaveChanges();
        }
        public void UpdatePlacement(string placementId,string candidateNum, string openingNum,double workedHours)
        {
            var placement = _context.Placements.Find(placementId);
            placement.CandidateNumber = candidateNum;
            placement.OpeningNumber = openingNum;
            placement.TotalWorkedHours= workedHours;

            _context.SaveChanges();
        }
        public void DeletePlacement(string placementId)
        {   
            _context.Placements.Find(placementId).IsDeleted = true;
            _context.SaveChanges();
        }
    }
}