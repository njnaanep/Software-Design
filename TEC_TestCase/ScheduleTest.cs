using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using DataLayer.EfClasses;
using NUnit.Framework;

namespace TEC_TestCase
{
    public class ScheduleTest
    {
        public static void GenerateSchedules()
        {
            var schedules =
                Schedule_ReadFormCsv(@"C:\Users\Admin\Desktop\2nd Sem '19\Software Design\Project\Data\Schedule.CSV");

            using var context = new TecContext();
            foreach (var schedule in schedules)
            {
                context.Add(schedule);
                context.SaveChanges();
            }
        }

        public static IList<Schedule> Schedule_ReadFormCsv(string location)
        {
            var sr = new StreamReader(location);

            var schedules = new List<Schedule>();

            string s = sr.ReadLine();
            var foreignKey = new ForeignKeys();

            while (s != null)
            {
                var split = s.Split(',');

                var schedule = new Schedule
                {
                    SessionId = foreignKey.GetRandomSessions(),
                    DayId = foreignKey.GetRandomDay(),
                    StartTime = DateTime.Parse(split[0]),
                    EndTime = DateTime.Parse(split[1]),
                };

                schedules.Add(schedule);

                s = sr.ReadLine();
            }

            return schedules;
        }

        [Test]
        public void Schedule_DisplayDuration()
        {
            var start = "8:00 AM";
            var end = "11:30 PM";

            var today = DateTime.Now;
            var next = today.AddDays(3).AddHours(4).AddMinutes(24);

            var duration1 = DateTime.Parse(end).Subtract(DateTime.Parse(start));
            var duration2 = next.Subtract(today);

            Console.WriteLine($"{duration1.Days} days, {duration1.Hours} Hrs and {duration1.Minutes} mins");
            Console.WriteLine($"{duration2.Days} days, {duration2.Hours} Hrs and {duration2.Minutes} mins");
        }

        [Test]
        public void Schedule_DisplayTime()
        {
            var start = DateTime.Now;
            //var start = new TimeSpan(7,20,00);
            var end = DateTime.Now.AddHours(6).AddMinutes(15);

            Console.WriteLine($"Start at  {start.ToShortTimeString()} and end at {end.ToShortTimeString()}");
        }
    }
}