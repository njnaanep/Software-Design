using System.Linq;
using DataLayer.EfClasses;

namespace ServiceLayer
{
    public class CompanyService
    {
        private TecContext _context;

        public CompanyService(TecContext context) => _context = context;

        public IQueryable<Company> GetCompanies() 
            => _context.Companies.Where(c => !c.IsDeleted);
        //public IQueryable<Company> SearchCompanies(string search) => 
        //    _context.Companies.Where(c => c.IsDeleted == false
        //    && c.CompanyName.Contains(search));

        public IQueryable<Company> SearchCompanies(string search) =>
            GetCompanies().Where(c => c.CompanyName.Contains(search));


        public void AddCompany( Company company)
        {
            _context.Companies.Add(company);
            _context.SaveChanges();
        }

        public void UpdateCompany(Company company)
        {
            var updatedCompany = _context.Companies.Find(company.CompanyId);
            updatedCompany.CompanyName = company.CompanyName;
            updatedCompany.CompanyAddress = company.CompanyAddress;
            _context.SaveChanges();
        }

        public void DeleteCompany(string companyId)
        {
            _context.Companies.Find(companyId).IsDeleted = true;
            _context.SaveChanges();
        }
    }
} 