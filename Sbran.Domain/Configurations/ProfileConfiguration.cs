using Sbran.Domain.Entities.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sbran.Domain.Configurations
{
    public sealed class ProfileConfiguration : IEntityTypeConfiguration<Profile>
    {
        public ProfileConfiguration(string schemaName)
        {
            SchemaName = schemaName;
        }

        public string TableName => "Profiles";

        public string SchemaName { get; private set; }

        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder.ToTable(TableName, SchemaName);

            builder.HasKey(profile => profile.Id);

            builder.Property(profile => profile.Id)
                .HasColumnName("Uid")
                .ValueGeneratedNever();

            builder.Property(profile => profile.Photo).IsRequired(false).HasColumnName("Avatar");
            builder.Property(profile => profile.WebPages).IsRequired(false).HasColumnName("WebPages");
        }
    }
}