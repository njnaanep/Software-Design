using System.Linq;
using DataLayer.EfClasses;
using DataLayer.Migrations;

namespace ServiceLayer
{
    public class CompanyService
    {
        private TecContext _context;

        public CompanyService(TecContext context) => _context = context;

        public IQueryable<Company> GetCompanies() => _context.Companies.Where(c => c.IsDeleted == false);

        public void AddCompany( Company company)
        {
            _context.Companies.Add(company);
            _context.SaveChanges();
        }

        public void UpdateCompany(string companyId, string name, string address)
        {
            var company = _context.Companies.Find(companyId);
            company.CompanyName = name;
            company.CompanyAddress = address;
            _context.SaveChanges();
        }

        public void DeleteCompany(string companyId)
        {
            var jobHistories = _context.JobHistories.Where(c => c.CompanyId == companyId);
            foreach (var jobHistory in jobHistories) 
                jobHistory.IsDeleted = true;

            var openings = _context.Openings.Where(c => c.CompanyId == companyId);
            foreach (var opening in openings)
            {
                var placements = _context.Placements.Where(c => c.OpeningNumber == opening.OpeningNumber);
                foreach (var placement in placements) 
                    placement.IsDeleted = true;

                opening.IsDeleted = true;
            }


            _context.Candidates.Find(companyId).IsDeleted=true;
            _context.SaveChanges();
        }
    }
}