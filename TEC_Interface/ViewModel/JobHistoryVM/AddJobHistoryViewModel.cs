using System;
using System.CodeDom;
using System.Collections.ObjectModel;
using System.Linq;
using DataLayer.EfClasses;
using ServiceLayer;

namespace TEC_Interface.ViewModel.Company
{
    public class AddJobHistoryViewModel
    {
        private JobHistoryService _jobHistoryService;

        private readonly CompanyService _companyService;
        private readonly CandidateService _candidateService;
        private string _searchCandidateString;
        private string _searchCompanyString;

        public AddJobHistoryViewModel(JobHistoryService jobHistoryService)
        {
            _jobHistoryService = jobHistoryService;
            _candidateService = new CandidateService(MainWindow.MainContext);
            _companyService = new CompanyService(MainWindow.MainContext);


            Candidates = new ObservableCollection<DataLayer.EfClasses.Candidate>(_candidateService.GetCandidate());
            Companies = new ObservableCollection<DataLayer.EfClasses.Company>(_companyService.GetCompanies());
        }


        public ObservableCollection<DataLayer.EfClasses.Candidate> Candidates { get; set; }
        public DataLayer.EfClasses.Candidate SelectedCandidate { get; set; }

        public ObservableCollection<DataLayer.EfClasses.Company> Companies { get; set; }
        public DataLayer.EfClasses.Company SelectedCompany { get; set; }


        public DateTime WorkedFrom { get; set; } = DateTime.Now; 

        public DateTime? WorkedTo { get; set; }

        public double? WorkedHours { get; set; }

        public JobHistoryViewModel AssociatedJobHistory { get; set; }

        public void Add()
        {
            var jobHistory = new JobHistory
            {
                CandidateNumber = SelectedCandidate.CandidateNumber,
                CompanyId = SelectedCompany.CompanyId,
                WorkedFrom = WorkedFrom
            };

            if (WorkedTo!= null) jobHistory.WorkedTo = WorkedTo;
            if (WorkedHours!= null) jobHistory.WorkedHours = WorkedHours;

            _jobHistoryService.AddJobHistory(jobHistory);

            AssociatedJobHistory = new JobHistoryViewModel(jobHistory);
        }

        public string SearchCandidateString
        {
            get => _searchCandidateString;
            set
            {
                _searchCandidateString = value;
                SearchCandidate(_searchCandidateString);
            }
        }

        private void SearchCandidate(string searchCandidateString)
        {
            Candidates.Clear();

            var candidates = new CandidateService(MainWindow.MainContext).SearchCandidate(searchCandidateString);

            foreach (var candidate in candidates)
                Candidates.Add(candidate);
        }

        public string SearchCompanyString
        {
            get => _searchCompanyString;
            set
            {
                _searchCompanyString = value;
                SearchCompany(_searchCompanyString);

            }
        }

        private void SearchCompany(string searchCompanyString)
        {
            Companies.Clear();

            var companies = new CompanyService(MainWindow.MainContext).SearchCompanies(searchCompanyString);

            foreach (var company in companies) 
                Companies.Add(company);
        }
    }
}