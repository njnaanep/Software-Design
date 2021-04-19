namespace TEC_Interface.ViewModel.Course
{
    public class CoursePrerequisite
    {
        public string Qualification { get; set; }
        public string PrerequisiteId { get; set; }

        public CoursePrerequisite(DataLayer.EfClasses.Prerequisite prerequisite)
        {
            PrerequisiteId = prerequisite.PrerequisiteId;
            Qualification = prerequisite.QualificationLink.QualificationCode;
        }
    }
}