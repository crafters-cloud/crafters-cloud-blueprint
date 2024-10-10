using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CraftersCloud.Blueprint.Data.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class EditedCompanyEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Company_Id",
                table: "Company");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Company_Id",
                table: "Company",
                column: "Id",
                unique: true);
        }
    }
}
