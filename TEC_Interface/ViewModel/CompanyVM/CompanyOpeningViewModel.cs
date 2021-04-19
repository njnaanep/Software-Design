namespace TEC_Interface.ViewModel.CompanyVM
{
    public class CompanyOpeningViewModel
    {
        public string OpeningId { get; set; }
        public string Qualification { get; set; }
        public string StartingDate { get; set; }
        public string AnticipatedEndDate { get; set; }
        public string HourlyPay { get; set; }

        public CompanyOpeningViewModel(DataLayer.EfClasses.Opening opening)
        {
            OpeningId = opening.OpeningNumber;
            Qualification = opening.QualificationLink.QualificationCode;
            StartingDate = opening.StartingDate.ToShortDateString();
            AnticipatedEndDate = opening.AnticipatedEndDate.ToShortDateString();
            HourlyPay = $"USD {opening.HourlyPay}";
        }
    }
}