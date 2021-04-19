using System;
using System.Collections.ObjectModel;
using System.Linq;
using ServiceLayer;
using TEC_Interface.ViewModel.Opening;

namespace TEC_Interface.ViewModel.OpeningVM
{
    public class AddOpeningViewModel
    {
        private OpeningService _openingService;

        private readonly CompanyService _companyService;
        private readonly QualificationService _qualificationService;

        public AddOpeningViewModel(OpeningService openingService, CompanyService companyService, QualificationService qualificationService)
        {
            _openingService = openingService;
            _companyService = companyService;
            _qualificationService = qualificationService;

            Companies = new ObservableCollection<DataLayer.EfClasses.Company>(_companyService.GetCompanies());
            Qualifications = new ObservableCollection<DataLayer.EfClasses.Qualification>(_qualificationService.GetQualifications());

            SelectedCompany = Companies.First();
            SelectedQualification = Qualifications.First();
        }

        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime EndDate { get; set; } = DateTime.Now.AddYears(1);

        public string HourlyPay { get; set; } 

        public ObservableCollection<DataLayer.EfClasses.Company> Companies { get; set; }

        public ObservableCollection<DataLayer.EfClasses.Qualification> Qualifications { get; set; }

        public DataLayer.EfClasses.Company SelectedCompany { get; set; }

        public DataLayer.EfClasses.Qualification SelectedQualification { get; set; }

        public OpeningViewModel AssociatedOpening { get; set; } 

        public void Add()
        {
            var opening = new DataLayer.EfClasses.Opening
            {
                CompanyId = SelectedCompany.CompanyId,
                QualificationId = SelectedQualification.QualificationId,
                StartingDate = StartDate,
                AnticipatedEndDate = EndDate,
                HourlyPay = double.Parse(HourlyPay)
            };


            _openingService.AddOpening(opening);

            AssociatedOpening = new OpeningViewModel(opening);
        }
    }
}