using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CraftersCloud.Blueprint.Data.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedUermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 200, "CategoriesRead" },
                    { 201, "CategoriesWrite" },
                    { 202, "CategoriesDelete" }
                });

            migrationBuilder.InsertData(
                table: "RolePermission",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 200, new Guid("028e686d-51de-4dd9-91e9-dfb5ddde97d0") },
                    { 201, new Guid("028e686d-51de-4dd9-91e9-dfb5ddde97d0") },
                    { 202, new Guid("028e686d-51de-4dd9-91e9-dfb5ddde97d0") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 200, new Guid("028e686d-51de-4dd9-91e9-dfb5ddde97d0") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 201, new Guid("028e686d-51de-4dd9-91e9-dfb5ddde97d0") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 202, new Guid("028e686d-51de-4dd9-91e9-dfb5ddde97d0") });

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 200);

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 201);

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 202);
        }
    }
}
