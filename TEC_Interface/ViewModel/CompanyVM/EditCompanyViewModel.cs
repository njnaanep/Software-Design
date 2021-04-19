using DataLayer.EfCode.Configuration;
using ServiceLayer;

namespace TEC_Interface.ViewModel.Company
{
    public class EditCompanyViewModel
    {
        private CompanyService _companyService;
        public CompanyViewModel CompanyToEdit { get; set; }

        public EditCompanyViewModel( CompanyViewModel companyToEdit, CompanyService companyService)
        {
            _companyService = companyService;
            CompanyToEdit = companyToEdit;

            CompanyId = companyToEdit.CompanyId;
            CopyEditableFields(CompanyToEdit);
        }

        private void CopyEditableFields(CompanyViewModel companyToEdit)
        {
            Name = companyToEdit.CompanyName;
            Address= companyToEdit.CompanyAddress;
        }
            
        public string CompanyId { get; }

        public string Name { get; set; }
        public string Address { get; set; }

        public void Edit()
        {
            var updateCompany = new DataLayer.EfClasses.Company
            {
                CompanyId = CompanyId,
                CompanyName = Name,
                CompanyAddress = Address
            };

            CompanyToEdit.CompanyName = Name;
            CompanyToEdit.CompanyAddress = Address;

            _companyService.UpdateCompany(updateCompany);
        }
    }
}