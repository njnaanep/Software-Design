using System.Windows;
using TEC_Interface.Annotations;

namespace TEC_Interface.ViewModel.Candidate
{
    public class CandidatePlacementViewModel
    {
        public string PlacementId { get; set; }
        public string OpeningNumber { get; set; }
        public string Company { get; set; }
        public string Status { get; set; }

        public CandidatePlacementViewModel(DataLayer.EfClasses.Placement placement)
        {
            PlacementId = placement.PlacementId;
            OpeningNumber = placement.OpeningNumber;
            Company = placement.OpeningLink.CompanyLink.CompanyName;
            Status = placement.PlacementStatus;
        }
    }
}