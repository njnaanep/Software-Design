using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media.Imaging;
using DataLayer.EfClasses;
using Microsoft.EntityFrameworkCore;
using ServiceLayer;
using TEC_Interface.ViewModel.Candidate;
using TEC_Interface.ViewModel.CompanyVM;

namespace TEC_Interface.ViewModel.Company
{
    public class CompanyListViewModel
    {
        private CompanyService _companyService;
        private CompanyViewModel _selectedCompany;
        private string _searchText;

        public CompanyListViewModel(CompanyService companyService)
        {
            _companyService = companyService;

            CompanyList = new ObservableCollection<CompanyViewModel>(
                _companyService.GetCompanies().Select(c =>
                    new CompanyViewModel(c)));

            if (CompanyList.Count > 0) 
                SelectedCompany = CompanyList.First();
        }

        #region STORAGE
        public ObservableCollection<CompanyViewModel> CompanyList { get; set; }

        public ObservableCollection<CompanyJobHistoryViewModel> CompanyJobHistories { get; set; }
            = new ObservableCollection<CompanyJobHistoryViewModel>();

        public ObservableCollection<CompanyOpeningViewModel> CompanyOpenings { get; set; }
            = new ObservableCollection<CompanyOpeningViewModel>();

        #endregion



        public void SearchCompany(string searchString)
        {
            ClearCompanyDataGrid();

            var companies = _companyService.SearchCompanies(searchString);

            foreach (var company in companies)
            {
                var companyViewModel = new CompanyViewModel(company);

                CompanyList.Add(companyViewModel);
            }

        }

        

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                SearchCompany(_searchText);
            }
        }

        public CompanyViewModel SelectedCompany
        {
            get => _selectedCompany;
            set
            {
                _selectedCompany = value;
                if (_selectedCompany != null) 
                    DisplayCompanyDataGrid(_selectedCompany.CompanyId);

            }
        }


        #region DisplayGrid
        private void DisplayCompanyDataGrid(string companyId)
        {
            DisplayCompanyJobHistories(companyId);
            DisplayCompanyOpenings(companyId);
        }

        private void ClearCompanyDataGrid()
        {
            CompanyList.Clear();
            CompanyJobHistories.Clear();
            CompanyOpenings.Clear();
        }

        private void DisplayCompanyOpenings(string companyId)
        {
            CompanyOpenings.Clear();

            var openings = new OpeningService(MainWindow.MainContext).GetCompanyOpenings(companyId)
                .Select(c => new CompanyOpeningViewModel(c));

            foreach (var opening in openings)
                CompanyOpenings.Add(opening);
        }

        private void DisplayCompanyJobHistories(string companyId)
        {
            CompanyJobHistories.Clear();

            var histories = JobHistoryService.GetCompanyJobHistory(companyId)
                .Select(c => new CompanyJobHistoryViewModel(c));

            foreach (var history in histories) 
                CompanyJobHistories.Add(history);
        }
        #endregion

        public void DeleteCompany(CompanyViewModel company)
        {
            _companyService.DeleteCompany(company.CompanyId);
            CompanyList.Remove(company);
        }

    }
}