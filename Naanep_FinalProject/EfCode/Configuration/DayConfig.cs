using System.Linq;
using System.Text;
using DataLayer.EfClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace DataLayer.EfCode.Configuration
{
    public class DayConfig : IEntityTypeConfiguration<Day>
    {
        public void Configure(EntityTypeBuilder<Day> builder)
        {
            builder.ToTable("Day");

            builder.HasKey(c => c.DayId);
            builder.Property(c => c.DayId).HasValueGenerator(typeof(DayIdGenerator));

        }
    }

    public class DayIdGenerator : ValueGenerator
    {
        protected override object NextValue(EntityEntry entry)
        {
            using var context = new TecContext();

            var st = new StringBuilder();

            st.Append($"Day-{context.Days.Count()+1}");

            return st.ToString();
        }

        public override bool GeneratesTemporaryValues => false;
    }
}