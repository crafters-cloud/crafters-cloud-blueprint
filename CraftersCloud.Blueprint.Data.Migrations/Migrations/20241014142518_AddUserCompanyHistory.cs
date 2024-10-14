using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CraftersCloud.Blueprint.Data.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AddUserCompanyHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserCompanyHistories",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EnrollmentDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EnrollmentDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCompanyHistories", x => new { x.CompanyId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserCompanyHistories_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCompanyHistories_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserCompanyHistories_UserId",
                table: "UserCompanyHistories",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCompanyHistories");
        }
    }
}
