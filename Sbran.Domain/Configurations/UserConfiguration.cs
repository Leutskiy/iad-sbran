using Sbran.Domain.Entities.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sbran.Domain.Configurations
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public UserConfiguration(string schemaName)
        {
            SchemaName = schemaName;
        }

        public string TableName => "Users";

        public string SchemaName { get; private set; }

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(TableName, SchemaName);

            builder.HasKey(user => user.Id);

            builder.Property(user => user.Id).HasColumnName("Uid").ValueGeneratedNever();

            builder.Property(user => user.Account).HasColumnName("Account");
            builder.Property(user => user.Password).HasColumnName("Password");
            builder.Property(user => user.ProfileId).HasColumnName("ProfileUid");

            builder
                .HasOne(user => user.Profile)
                .WithOne()
                .HasForeignKey<User>(user => user.ProfileId)
                .HasPrincipalKey<Profile>(profile => profile.Id);
        }
    }
}