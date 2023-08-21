using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class userMail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MailAddress",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MailAddress",
                table: "User");
        }
    }
}
