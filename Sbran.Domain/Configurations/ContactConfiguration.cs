using Sbran.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sbran.Domain.Configurations
{
    public sealed class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public ContactConfiguration(string schemaName)
        {
            SchemaName = schemaName;
        }

        public string TableName => "Contacts";

        public string SchemaName { get; private set; }

        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.ToTable(TableName, SchemaName);

            builder.HasKey(contact => contact.Id);

            builder.Property(contact => contact.Id)
                .HasColumnName("Uid")
                .ValueGeneratedNever();

            builder.Property(contact => contact.Email).IsRequired(false).HasColumnName("Email");
            builder.Property(contact => contact.Postcode).IsRequired(false).HasColumnName("Postcode");
            builder.Property(contact => contact.HomePhoneNumber).IsRequired(false).HasColumnName("HomePhoneNumber");
            builder.Property(contact => contact.WorkPhoneNumber).IsRequired(false).HasColumnName("WorkPhoneNumber");
            builder.Property(contact => contact.MobilePhoneNumber).IsRequired(false).HasColumnName("MobilePhoneNumber");
        }
    }
}