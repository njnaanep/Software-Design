using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ServiceLayer;
using TEC_Interface.ViewModel.Company;

namespace TEC_Interface.View.Company
{
    /// <summary>
    /// Interaction logic for AddCompany.xaml
    /// </summary>
    public partial class AddCompanyView : Window
    {
        public AddCompanyView()
        {
            InitializeComponent();
        }

        private CompanyListViewModel _companyListViewModel;
        private CompanyService _companyService;

        private AddCompanyViewModel _companyToAdd;

        public AddCompanyView(CompanyListViewModel companyListViewModel, CompanyService companyService) : this()
        {
            _companyListViewModel = companyListViewModel;
            _companyService = companyService;
            _companyToAdd = new AddCompanyViewModel(_companyService);
            DataContext = _companyToAdd;
        }

        private void BtnAddCompany(object sender, RoutedEventArgs e)
        {
            try
            {
                _companyToAdd.Add();
                _companyListViewModel.CompanyList.Add(_companyToAdd.AssociatedCompany);
                MessageBox.Show($"Successfully Added Company ");
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Error Adding Company:\n{exception}");
            }
        }

        private void BtnCancelAdd(object sender, RoutedEventArgs e) => Close();
    }
}
