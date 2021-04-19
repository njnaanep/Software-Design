using System.Linq;
using System.Text;
using DataLayer.EfClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace DataLayer.EfCode.Configuration
{
    public class PrerequisiteConfig : IEntityTypeConfiguration<Prerequisite>
    {
        public void Configure(EntityTypeBuilder<Prerequisite> builder)
        {
            builder.ToTable("Prerequisite");

            builder.HasKey(c => c.PrerequisiteId);
            builder.Property(c => c.PrerequisiteId)
                .HasValueGenerator(typeof(PrerequisiteIdGenerator));

            builder.HasOne(c => c.QualificationLink)
                .WithMany(c => c.Prerequisites)
                .HasForeignKey(c => c.QualificationId);

            builder.HasOne(c => c.CourseLink)
                .WithMany(c => c.Prerequisites)
                .HasForeignKey(c => c.CourseId);

            builder.Property(c => c.IsDeleted).HasDefaultValue(false);
        }

        public class PrerequisiteIdGenerator : ValueGenerator
        {
            protected override object NextValue(EntityEntry entry)
            {
                using var context = new TecContext();

                var st = new StringBuilder();

                var idNumSequence = (context.Prerequisites.Count() + 1).ToString();

                st.Append("PRE-");
                st.Append($"{idNumSequence.PadLeft(6, '0')}");

                return st.ToString();
            }

            public override bool GeneratesTemporaryValues => false;
        }
    }
}