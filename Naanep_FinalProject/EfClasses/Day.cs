using System.Collections.Generic;

namespace DataLayer.EfClasses
{
    public class Day
    {
        public string DayId { get; set; }
        public string Acronym { get; set; }
        public string DayName { get; set; }

        public ICollection<Schedule> Schedules { get; set; }
    }
}