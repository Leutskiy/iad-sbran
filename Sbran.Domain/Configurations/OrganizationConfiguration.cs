using Sbran.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sbran.Domain.Configurations
{
    public sealed class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
    {
        public OrganizationConfiguration(string schemaName)
        {
            SchemaName = schemaName;
        }

        public string TableName => "Organizations";

        public string SchemaName { get; private set; }

        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.ToTable(TableName, SchemaName);

            builder.HasKey(organization => organization.Id);

            builder.Property(organization => organization.Id)
                .HasColumnName("Uid")
                .ValueGeneratedNever();

            builder.Property(organization => organization.StateRegistrationId).IsRequired(false).HasColumnName("StateRegistrationUid");
            builder.Property(organization => organization.Name).IsRequired(false).HasColumnName("Name");
            builder.Property(organization => organization.ShortName).IsRequired(false).HasColumnName("ShortName");
            builder.Property(organization => organization.LegalAddress).IsRequired(false).HasColumnName("LegalAddress");
            builder.Property(organization => organization.ScientificActivity).IsRequired(false).HasColumnName("ScientificActivity");

            builder
                .HasOne(organization => organization.StateRegistration)
                .WithOne()
                .HasForeignKey<Organization>(organization => organization.StateRegistrationId)
                .HasPrincipalKey<StateRegistration>(stateRegistration => stateRegistration.Id);
        }
    }
}