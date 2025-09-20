using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealtimeChat.Infrastructure.DB.Migrations
{
    /// <inheritdoc />
    public partial class AddTrgmIndexToTextMessageContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("CREATE EXTENSION IF NOT EXISTS pg_trgm;");
            
            migrationBuilder.Sql(@"
                CREATE INDEX IF NOT EXISTS idx_messages_content_text_trgm
                ON messages
                USING GIN (
                    jsonb_extract_path_text(content, 'Text') gin_trgm_ops
                )
                WHERE content->>'Type' = 'text';
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Удаляем индекс
            migrationBuilder.Sql(@"
                DROP INDEX IF EXISTS idx_messages_content_text_trgm;
            ");
        }
    }
}