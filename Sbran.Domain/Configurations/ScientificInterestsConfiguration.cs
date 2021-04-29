using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sbran.Domain.Entities;

namespace Sbran.Domain.Configurations
{
	public sealed class ScientificInterestsConfiguration : IEntityTypeConfiguration<ScientificInterests>
    {
        public ScientificInterestsConfiguration(string schemaName)
        {
            SchemaName = schemaName;
        }

        public string TableName => "ScientificInterestss";

        public string SchemaName { get; private set; }

        public void Configure(EntityTypeBuilder<ScientificInterests> builder)
        {
            builder.ToTable(TableName, SchemaName);

            builder.HasKey(scientificInterests => scientificInterests.Id);

            builder.Property(scientificInterests => scientificInterests.Id)
                .HasColumnName("Uid")
                .ValueGeneratedNever();

            builder.Property(scientificInterests => scientificInterests.EmployeeId).HasColumnName("EmployeeUid");
            builder.Property(scientificInterests => scientificInterests.NameOfScientificInterests).IsRequired(false).HasColumnName("ScientificAdvisor");

            builder
                .HasOne(scientificInterests => scientificInterests.Employee)
                .WithMany()
                .HasForeignKey(scientificInterests => scientificInterests.EmployeeId);
        }
    }
}
