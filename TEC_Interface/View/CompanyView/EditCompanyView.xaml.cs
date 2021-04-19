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
    /// Interaction logic for EditCompany.xaml
    /// </summary>
    public partial class EditCompanyView : Window
    {
        public EditCompanyView()
        {
            InitializeComponent();
        }

        private EditCompanyViewModel _editCompany;

        public EditCompanyView(CompanyViewModel companyToEdit, CompanyService companyService) : this()
        {
            _editCompany = new EditCompanyViewModel(companyToEdit, companyService);
            DataContext = _editCompany;
        }

        private void BtnCompanySaveChanges(object sender, RoutedEventArgs e)
        {
            try
            {
                _editCompany.Edit();
                MessageBox.Show($"Company Successfully Edited");
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show($"There was an error editing the company:\n{exception}");
            }
        }

        private void BtnCancelChange(object sender, RoutedEventArgs e) => Close();
    }
}
