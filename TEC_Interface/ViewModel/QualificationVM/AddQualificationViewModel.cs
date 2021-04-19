using System.Transactions;
using ServiceLayer;

namespace TEC_Interface.ViewModel.Qualification
{
    public class AddQualificationViewModel
    {
        private QualificationService _qualificationService;

        public AddQualificationViewModel(QualificationService qualificationService)
        {
            _qualificationService = qualificationService;
        }

        public string Code { get; set; } 
        public string Description { get; set; }

        public QualificationViewModel AssociatedQualification { get; set; }

        public void Add()
        {
            var qualification = new DataLayer.EfClasses.Qualification
            {
                QualificationCode = Code,
                Description = Description
            };

            _qualificationService.AddQualification(qualification);

            AssociatedQualification = new QualificationViewModel(qualification);
        }

    }
}