using Microsoft.EntityFrameworkCore.Migrations;

using RealtimeChat.Persistence.DB;
using RealtimeChat.Persistence.DB.Entities;

#nullable disable

namespace RealtimeChat.Infrastructure.DB.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class ChangeMessageContentToJson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "content_json",
                table: "messages");

            migrationBuilder.DropColumn(
                name: "chat_room_participant_id",
                table: "chat_room_participants");

            migrationBuilder.AddColumn<MessageContentEntity>(
                name: "content",
                table: "messages",
                type: "jsonb",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "content",
                table: "messages");

            migrationBuilder.AddColumn<string>(
                name: "content_json",
                table: "messages",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "chat_room_participant_id",
                table: "chat_room_participants",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
