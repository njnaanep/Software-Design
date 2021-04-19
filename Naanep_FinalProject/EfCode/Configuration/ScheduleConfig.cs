using System.Linq;
using System.Text;
using DataLayer.EfClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace DataLayer.EfCode.Configuration
{
    public class ScheduleConfig : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder.ToTable("Schedule");

            builder.HasKey(c => c.ScheduleId);
            builder.Property(c => c.ScheduleId).HasValueGenerator(typeof(ScheduleIdGenerator));

            builder.HasOne(c => c.TrainingSessionLink)
                .WithMany(c => c.Schedules)
                .HasForeignKey(c => c.SessionId);

            builder.HasOne(c => c.DayLink)
                .WithMany(c => c.Schedules)
                .HasForeignKey(c => c.DayId);


            builder.Property(c => c.IsDeleted).HasDefaultValue(false);
        }
    }

    public class ScheduleIdGenerator : ValueGenerator
    {
        protected override object NextValue(EntityEntry entry)
        {
            using var context = new TecContext();

            var st = new StringBuilder();

            var idNumSequence = (context.Schedules.Count() + 1).ToString().PadLeft(6,'0');

            st.Append($"SCD-{idNumSequence}");

            return st.ToString();
        }

        public override bool GeneratesTemporaryValues => false;
    }
}