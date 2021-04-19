namespace DataLayer.EfClasses
{
    public class Prerequisite
    {
        public string PrerequisiteId { get; set; }

        public string CourseId { get; set; }
        public string QualificationId { get; set; }

        public string RequirementFor { get; set; }    

        public Course CourseLink { get; set; }
        public Qualification QualificationLink { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}