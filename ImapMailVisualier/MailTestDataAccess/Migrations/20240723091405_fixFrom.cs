using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MailTestDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class fixFrom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "From",
                table: "EmailMessages",
                newName: "FromName");

            migrationBuilder.AddColumn<string>(
                name: "FromAddress",
                table: "EmailMessages",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromAddress",
                table: "EmailMessages");

            migrationBuilder.RenameColumn(
                name: "FromName",
                table: "EmailMessages",
                newName: "From");
        }
    }
}
