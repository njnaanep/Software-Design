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
using TEC_Interface.ViewModel.Qualification;

namespace TEC_Interface.View.Qualifications
{
    /// <summary>
    /// Interaction logic for AddQualification.xaml
    /// </summary>
    public partial class AddQualificationView : Window
    {
        public AddQualificationView()
        {
            InitializeComponent();
        }

        private QualificationService _qualificationService;
        private QualificationListViewModel _qualificationListViewModel;

        private AddQualificationViewModel _qualificationToAdd;

        public AddQualificationView(QualificationService qualificationService, QualificationListViewModel qualificationListViewModel) : this()
        {
            _qualificationService = qualificationService;
            _qualificationListViewModel = qualificationListViewModel;
            _qualificationToAdd = new AddQualificationViewModel(_qualificationService);
            DataContext = _qualificationToAdd;
        }

        private void BtnAddQualification(System.Object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                _qualificationToAdd.Add();
                _qualificationListViewModel.QualificationList.Add(_qualificationToAdd.AssociatedQualification);
                MessageBox.Show($"Successfully Added Qualification ");
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Error Adding Qualification:\n{exception}");
            }
        }

        private void BtnCancelAdd(System.Object sender, System.Windows.RoutedEventArgs e) => Close();
    }
}
