using System;
using System.Collections.ObjectModel;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Windows.Controls;
using ServiceLayer;

namespace TEC_Interface.ViewModel.Opening
{
    public class EditOpeningViewModel
    {
        public OpeningViewModel OpeningToEdit { get; set; }

        private OpeningService _openingService;
        private CompanyService _companyService;
        private QualificationService _qualificationService;


        public EditOpeningViewModel(OpeningViewModel openingToEdit, 
            OpeningService openingService, 
            CompanyService companyService,
            QualificationService qualificationService)
        {
            OpeningToEdit = openingToEdit;
            _openingService = openingService;
            _companyService = companyService;
            _qualificationService = qualificationService;
            
            Companies = new ObservableCollection<DataLayer.EfClasses.Company>(companyService.GetCompanies());
            Qualifications = new ObservableCollection<DataLayer.EfClasses.Qualification>(qualificationService.GetQualifications());

            SelectedCompany = Companies.First(c => c.CompanyName == openingToEdit.CompanyName);
            SelectedQualification = Qualifications.First(c => c.QualificationCode == openingToEdit.Qualification);

            CopyEditableFields(openingToEdit);
        }

        private void CopyEditableFields(OpeningViewModel openingToEdit)
        {
            StartingDate = Convert.ToDateTime(openingToEdit.StartingDate);
            EndDate = Convert.ToDateTime(openingToEdit.EndDate);
            Rate = openingToEdit.HourlyPay;
        }

        public DataLayer.EfClasses.Company SelectedCompany { get; set; }

        public DataLayer.EfClasses.Qualification SelectedQualification { get; set; }


        public DateTime StartingDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now.AddYears(1);
        public double Rate { get; set; }

        public ObservableCollection<DataLayer.EfClasses.Company> Companies { get; set; }
        public ObservableCollection<DataLayer.EfClasses.Qualification> Qualifications { get; set; }


        public void Edit()
        {
            var updatedOpening = new DataLayer.EfClasses.Opening
            {
                OpeningNumber = OpeningToEdit.OpeningNumber,
                CompanyId = SelectedCompany.CompanyId,
                QualificationId =  SelectedQualification.QualificationId,
                StartingDate = StartingDate,
                AnticipatedEndDate = EndDate,
                HourlyPay = Rate
            };

            OpeningToEdit.CompanyName = SelectedCompany.CompanyName;
            OpeningToEdit.Qualification = SelectedQualification.QualificationCode;
            OpeningToEdit.StartingDate = StartingDate.ToShortDateString();
            OpeningToEdit.StartingDate = EndDate.ToShortDateString();
            OpeningToEdit.HourlyPay = Rate;


            _openingService.UpdateOpening(updatedOpening);
        }
    }
}