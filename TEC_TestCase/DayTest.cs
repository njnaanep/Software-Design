using System.Collections.Generic;
using System.IO;
using System.Net.WebSockets;
using DataLayer.EfClasses;
using NUnit.Framework;

namespace TEC_TestCase
{
    [TestFixture]
    public class DayTest
    {
        [Test]
        public static void GenerateDays()
        {
            var days =
                Day_ReadFormCsv(@"C:\Users\Admin\Desktop\2nd Sem '19\Software Design\Project\Data\Day.CSV");

            using var context = new TecContext();
            foreach (var day in days)
            {
                context.Add(day);
                context.SaveChanges();
            }
        }

        public static IList<Day> Day_ReadFormCsv(string location)
        {
            var sr = new StreamReader(location);

            var dayList = new List<Day>();

            string s = sr.ReadLine();

            while (s != null)
            {
                var split = s.Split(',');

                var day = new Day
                {
                    Acronym = split[0],
                    DayName = split[1]
                };

                dayList.Add(day);

                s = sr.ReadLine();
            }

            return dayList;
        }
        
    }
}