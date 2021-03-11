using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sbran.Domain.Entities.Chat;
using Sbran.Domain.Entities.System;

namespace Sbran.Domain.Configurations
{

    public sealed class ChatRoomListConfiguration : IEntityTypeConfiguration<ChatRoomList>
    {
        public ChatRoomListConfiguration(string schemaName)
        {
            SchemaName = schemaName;
        }

        public string TableName => "ChatRoomLists";

        public string SchemaName { get; private set; }

        public void Configure(EntityTypeBuilder<ChatRoomList> builder)
        {
            builder.ToTable(TableName, SchemaName);

            builder.HasKey(chatRoomList => chatRoomList.Id);

            builder.Property(chatRoomList => chatRoomList.Id).HasColumnName("ChatRoomListUid").ValueGeneratedNever();

            builder.Property(chatRoomList => chatRoomList.ChatRoomId).HasColumnName("ChatRoomUid");
            builder.Property(chatRoomList => chatRoomList.UserId).HasColumnName("UserUid");

            builder
                .HasOne(chatRoomList => chatRoomList.ChatRoom)
                .WithMany();

        }
    }
}
