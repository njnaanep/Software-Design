using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace DataLayer.EfClasses
{
    public class TrainingSession
    {
        public string SessionId { get; set; }

        public string CourseId { get; set; }
        public Course CourseLink { get; set; }

        public double SessionFee { get; set; }
        public uint SessionCapacity { get; set; }    


        public ICollection<RegisteredCandidate> RegisteredCandidates { get; set; }
        public ICollection<Schedule> Schedules { get; set; }    

        public bool IsDeleted { get; set; } = false;
    }
}