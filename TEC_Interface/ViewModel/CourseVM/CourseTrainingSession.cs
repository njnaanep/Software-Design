namespace TEC_Interface.ViewModel.Course
{
    public class CourseTrainingSession      
    {
        public string SessionId { get; set; }
        public string SessionDate { get; set; }
        public string Fee { get; set; }

        public CourseTrainingSession(DataLayer.EfClasses.TrainingSession session)
        {
            SessionId = session.SessionId;
            Fee = $"USD {session.SessionFee}";
        }
    }
}