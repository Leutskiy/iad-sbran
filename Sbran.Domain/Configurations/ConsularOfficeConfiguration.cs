using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sbran.Domain.Entities;

namespace Sbran.Domain.Configurations
{
	public sealed class ConsularOfficeConfiguration : IEntityTypeConfiguration<ConsularOffice>
    {
        public ConsularOfficeConfiguration(string schemaName)
        {
            SchemaName = schemaName;
        }

        public string TableName => "ConsularOffices";

        public string SchemaName { get; private set; }

        public void Configure(EntityTypeBuilder<ConsularOffice> builder)
        {
            builder.ToTable(TableName, SchemaName);

            builder.HasKey(consularOffice => consularOffice.Id);

            builder.Property(consularOffice => consularOffice.Id)
                .HasColumnName("Uid")
                .ValueGeneratedNever();

            builder.Property(consularOffice => consularOffice.EmployeeId).HasColumnName("EmployeeUid");
            builder.Property(consularOffice => consularOffice.NameOfTheConsularPost).IsRequired(false).HasColumnName("NameOfTheConsularPost");
            builder.Property(consularOffice => consularOffice.CountryOfLocation).IsRequired(false).HasColumnName("CountryOfLocation");
            builder.Property(consularOffice => consularOffice.CityOfLocation).IsRequired(false).HasColumnName("CityOfLocation");
            builder.Property(consularOffice => consularOffice.TextOfAgreement).IsRequired(false).HasColumnName("TextOfAgreement");

            builder
                .HasOne(consularOffice => consularOffice.Employee)
                .WithMany()
                .HasForeignKey(consularOffice => consularOffice.EmployeeId);
        }
    }
}
