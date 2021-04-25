using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sbran.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sbran.Domain.Configurations
{
    public sealed class VoteConfiguration : IEntityTypeConfiguration<Vote>
    {
        public VoteConfiguration(string schemaName)
        {
            SchemaName = schemaName;
        }

        public string TableName => "Votes";

        public string SchemaName { get; private set; }

        public void Configure(EntityTypeBuilder<Vote> builder)
        {
            builder.ToTable(TableName, SchemaName);

            builder.HasKey(vote => vote.Id);

            builder.Property(vote => vote.Id)
                .HasColumnName("Uid")
                .ValueGeneratedNever();

            builder.Property(vote => vote.Name).HasColumnName("Name");

            builder
                .HasMany(vote => vote.voteLists)
                .WithOne();
        }
    }
}
