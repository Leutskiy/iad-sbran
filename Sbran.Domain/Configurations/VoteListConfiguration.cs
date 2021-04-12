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
    public sealed class VoteListConfiguration : IEntityTypeConfiguration<VoteList>
    {
        public VoteListConfiguration(string schemaName)
        {
            SchemaName = schemaName;
        }

        public string TableName => "VoteLists";

        public string SchemaName { get; private set; }

        public void Configure(EntityTypeBuilder<VoteList> builder)
        {
            builder.ToTable(TableName, SchemaName);

            builder.HasKey(voteList => voteList.Id);

            builder.Property(voteList => voteList.Id)
                .HasColumnName("Uid")
                .ValueGeneratedNever();

            builder.Property(voteList => voteList.Name).HasColumnName("Name");
            builder.Property(voteList => voteList.Count).HasColumnName("Count");

            builder
                .HasOne(voteList => voteList.Vote)
                .WithMany()
                .HasForeignKey(voteList => voteList.VoteId);

        }
    }
}
