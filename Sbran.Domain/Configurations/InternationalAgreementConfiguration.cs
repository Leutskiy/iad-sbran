using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sbran.Domain.Entities;

namespace Sbran.Domain.Configurations
{
	public sealed class InternationalAgreementConfiguration : IEntityTypeConfiguration<InternationalAgreement>
    {
        public InternationalAgreementConfiguration(string schemaName)
        {
            SchemaName = schemaName;
        }

        public string TableName => "InternationalAgreements";

        public string SchemaName { get; private set; }

        public void Configure(EntityTypeBuilder<InternationalAgreement> builder)
        {
            builder.ToTable(TableName, SchemaName);

            builder.HasKey(internationalAgreement => internationalAgreement.Id);

            builder.Property(internationalAgreement => internationalAgreement.Id)
                .HasColumnName("Uid")
                .ValueGeneratedNever();

            builder.Property(internationalAgreement => internationalAgreement.EmployeeId).HasColumnName("EmployeeUid");
            builder.Property(internationalAgreement => internationalAgreement.TheNameOfTheAgreement).IsRequired(false).HasColumnName("TheNameOfTheAgreement");
            builder.Property(internationalAgreement => internationalAgreement.TheFirstPartyToTheAgreement).IsRequired(false).HasColumnName("TheFirstPartyToTheAgreement");
            builder.Property(internationalAgreement => internationalAgreement.PlaceOfSigning).IsRequired(false).HasColumnName("PlaceOfSigning");
            builder.Property(internationalAgreement => internationalAgreement.TheSecondPartyToTheAgreement).IsRequired(false).HasColumnName("TheSecondPartyToTheAgreement");
            builder.Property(internationalAgreement => internationalAgreement.DateOfEntry).IsRequired(false).HasColumnName("DateOfEntry");
            builder.Property(internationalAgreement => internationalAgreement.TextOfTheAgreement).IsRequired(false).HasColumnName("TextOfTheAgreement");

            builder
                .HasOne(internationalAgreement => internationalAgreement.Employee)
                .WithMany()
                .HasForeignKey(internationalAgreement => internationalAgreement.EmployeeId);
        }
    }
}
