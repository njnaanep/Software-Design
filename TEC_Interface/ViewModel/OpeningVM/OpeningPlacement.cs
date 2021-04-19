namespace TEC_Interface.ViewModel.Opening
{
    public class OpeningPlacement
    {
        public string PlacementId { get; set; }
        public string Candidate { get; set; }
        public string Status { get; set; }

        public OpeningPlacement(DataLayer.EfClasses.Placement placement)
        {
            PlacementId = placement.PlacementId;
            Candidate = placement.CandidateLink.CandidateFullName;
            Status = placement.PlacementStatus;
        }
    }
}