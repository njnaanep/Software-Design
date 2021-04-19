using System.Runtime.InteropServices.ComTypes;
using ServiceLayer;

namespace TEC_Interface.ViewModel.Company
{
    public class AddCompanyViewModel
    {
        private CompanyService _companyService;

        public AddCompanyViewModel(CompanyService companyService)
        {
            _companyService = companyService;
        }

        public string Name { get; set; } 
        public string Address { get; set; } 
        public CompanyViewModel AssociatedCompany { get; set; }

        public void Add()
        {
            var company = new DataLayer.EfClasses.Company
            {
                CompanyName = Name,
                CompanyAddress = Address
            };

            _companyService.AddCompany(company);

            AssociatedCompany = new CompanyViewModel(company);
        }
    }
}