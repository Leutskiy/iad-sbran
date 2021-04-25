using Sbran.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sbran.Domain.Configurations
{
    public sealed class AlienConfiguration : IEntityTypeConfiguration<Alien>
    {
        public AlienConfiguration(string schemaName)
        {
            SchemaName = schemaName;
        }

        public string TableName => "Aliens";

        public string SchemaName { get; private set; }

        public void Configure(EntityTypeBuilder<Alien> builder)
        {
            builder.ToTable(TableName, SchemaName);

            builder.HasKey(alien => alien.Id);

            builder.Property(alien => alien.Id).HasColumnName("AlienUid").ValueGeneratedNever();

            builder.Property(alien => alien.ContactId).HasColumnName("ContactUid");
            builder.Property(alien => alien.PassportId).IsRequired(false).HasColumnName("PassportUid");
            builder.Property(alien => alien.OrganizationId).IsRequired(false).HasColumnName("OrganizationUid");
            builder.Property(alien => alien.StateRegistrationId).IsRequired(false).HasColumnName("StateRegistrationUid");
            builder.Property(alien => alien.Position).IsRequired(false).HasColumnName("Position");
            builder.Property(alien => alien.WorkPlace).IsRequired(false).HasColumnName("WorkPlace");
            builder.Property(alien => alien.WorkAddress).IsRequired(false).HasColumnName("WorkAddress");
            builder.Property(alien => alien.StayAddress).IsRequired(false).HasColumnName("StayAddress");

            builder
                .HasOne(alien => alien.Contact)
                .WithOne()
                .HasForeignKey<Alien>(alien => alien.ContactId)
                .HasPrincipalKey<Contact>(contact => contact.Id);

            builder
                .HasOne(alien => alien.Passport)
                .WithOne()
                .HasForeignKey<Alien>(alien => alien.PassportId)
                .HasPrincipalKey<Passport>(passport => passport.Id);

            builder
                .HasOne(alien => alien.Organization)
                .WithOne()
                .HasForeignKey<Alien>(alien => alien.OrganizationId)
                .HasPrincipalKey<Organization>(organization => organization.Id);

            builder
                .HasOne(alien => alien.StateRegistration)
                .WithOne()
                .HasForeignKey<Alien>(alien => alien.StateRegistrationId)
                .HasPrincipalKey<StateRegistration>(stateRegistration => stateRegistration.Id);
        }
    }
}