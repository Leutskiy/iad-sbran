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
    public sealed class NewsConfiguration : IEntityTypeConfiguration<News>
    {
        public NewsConfiguration(string schemaName)
        {
            SchemaName = schemaName;
        }

        public string TableName => "News";

        public string SchemaName { get; private set; }

        public void Configure(EntityTypeBuilder<News> builder)
        {
            builder.ToTable(TableName, SchemaName);

            builder.HasKey(news => news.Id);

            builder.Property(news => news.Id)
                .HasColumnName("Uid")
                .ValueGeneratedNever();

            builder.Property(news => news.Name).HasColumnName("Name");
            builder.Property(news => news.DateTime).HasColumnName("DateTime");
        }
    }
}
