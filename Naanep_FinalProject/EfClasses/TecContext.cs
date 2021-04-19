using DataLayer.EfCode.Configuration;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.EfClasses
{
    public class TecContext : DbContext
    {
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Certification> Certifications { get; set; }
        public DbSet<Qualification> Qualifications { get; set; }
        public DbSet<Prerequisite> Prerequisites { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<TrainingSession> TrainingSessions { get; set; }
        public DbSet<RegisteredCandidate> RegisteredCandidates { get; set; }
        public DbSet<Opening> Openings { get; set; }
        public DbSet<Placement> Placements { get; set; }
        public DbSet<JobHistory> JobHistories { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<Schedule> Schedules { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TemporaryEmploymentCorp;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CandidateConfig());
            modelBuilder.ApplyConfiguration(new JobHistoryConfig());
            modelBuilder.ApplyConfiguration(new CompanyConfig());
            modelBuilder.ApplyConfiguration(new QualificationConfig());
            modelBuilder.ApplyConfiguration(new OpeningConfig());
            modelBuilder.ApplyConfiguration(new PlacementConfig());
            modelBuilder.ApplyConfiguration(new CourseConfig());
            modelBuilder.ApplyConfiguration(new PrerequisiteConfig());
            modelBuilder.ApplyConfiguration(new CertificationConfig());
            modelBuilder.ApplyConfiguration(new TrainingSessionConfig());
            modelBuilder.ApplyConfiguration(new RegisteredCandidateConfig());
            modelBuilder.ApplyConfiguration(new ScheduleConfig());
            modelBuilder.ApplyConfiguration(new DayConfig());
        }
    }
}