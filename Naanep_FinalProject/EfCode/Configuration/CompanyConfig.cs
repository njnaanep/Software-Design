using System;
using System.Linq;
using System.Text;
using DataLayer.EfClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace DataLayer.EfCode.Configuration
{
    public class CompanyConfig : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Company");

            builder.HasKey(c => c.CompanyId);
            builder.Property(c => c.CompanyId)
                .HasValueGenerator(typeof(CompanyIdGenerator));

            builder.Property(c => c.IsDeleted).HasDefaultValue(false);
        }
    }


    public class CompanyIdGenerator : ValueGenerator
    {
        public override bool GeneratesTemporaryValues => false;
        protected override object NextValue(EntityEntry entry)
        {
            using var context = new TecContext();

            var stringId = new StringBuilder();

            var idNumSequence = (context.Companies.Count() + 1).ToString();
            
            stringId.Append("CMP-");
            stringId.Append($"{idNumSequence.PadLeft(6, '0')}");

            return stringId.ToString();
        }
    }
}