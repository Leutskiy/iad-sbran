using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sbran.Domain.Entities.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sbran.Domain.Configurations
{
    public sealed class ChatMessageFileConfiguration : IEntityTypeConfiguration<ChatMessageFile>
    {
        public ChatMessageFileConfiguration(string schemaName)
        {
            SchemaName = schemaName;
        }

        public string TableName => "ChatMessageFiles";

        public string SchemaName { get; private set; }

        public void Configure(EntityTypeBuilder<ChatMessageFile> builder)
        {
            builder.ToTable(TableName, SchemaName);

            builder.HasKey(chatroom => chatroom.Id);

            builder.Property(chatroom => chatroom.Id).HasColumnName("ChatMessageFileUid").ValueGeneratedNever();
            builder.Property(messages => messages.ChatMessageId).HasColumnName("ChatMessageUid");
            builder.Property(messages => messages.FileBinary).HasColumnName("File");
            builder.Property(messages => messages.fileName).HasColumnName("FileName");

            builder
                  .HasOne(messages => messages.ChatMessage)
              .WithMany();
        }
    }
}
