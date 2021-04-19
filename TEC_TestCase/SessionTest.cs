using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using DataLayer.EfClasses;
using NUnit.Framework;

namespace TEC_TestCase
{
    [TestFixture]
    public class SessionTest
    {
        [Test]
        public static void GenerateSessions()
        {
            var trainingSessions =
                GetSessions(@"C:\Users\Admin\Desktop\2nd Sem '19\Software Design\Project\Data\TrainingSession.CSV");

            using var context = new TecContext();
            foreach (var trainingSession in trainingSessions)
            {
                context.Add(trainingSession);
                context.SaveChanges();
            }
        }

        [Test]
        public void DeleteSession()
        {
            using var context = new TecContext();

            context.Remove(context.TrainingSessions.Find("TRA-000001"));
            context.SaveChanges();
        }

        private static IList<TrainingSession> GetSessions(string location)
        {
            var sr = new StreamReader(location);

            var sessionList = new List<TrainingSession>();
            string s = sr.ReadLine();

            while (s != null)
            {
                var split = s.Split(',');

                var newTrainingSession = new TrainingSession
                {
                    SessionFee = double.Parse(split[0]),
                    SessionCapacity = uint.Parse(split[1]),
                    CourseId = new ForeignKeys().GetRandomCourse()
                };

                sessionList.Add(newTrainingSession);

                s = sr.ReadLine();
            }

            return sessionList;
        }

        [Test]
        public void DisplayHoursWithDoubleInput()
        {
            var input = 11.51234;

            //var s = input.Split('.');

            int minutes = (int)(input * 60);

            var ts = new TimeSpan(0, minutes,0 );



            Console.WriteLine($"Hours: {ts.Hours}\tMinutes: {ts.Minutes}\tSeconds: {ts.Seconds}");
            Console.WriteLine($"{ts.Hours}:{ts.Minutes}");
        }
    }
}