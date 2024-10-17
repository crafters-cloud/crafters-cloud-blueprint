using CraftersCloud.Core.Helpers;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CraftersCloud.Blueprint.Data.Migrations;

public static class MigrationBuilderExtensions
{
    public static void ResourceSql(this MigrationBuilder migrationBuilder, string scriptName)
    {
        var sql = EmbeddedResource.ReadResourceContent(typeof(DbInitializer).Assembly,
            $"CraftersCloud.Blueprint.Data.Migrations.Scripts.{scriptName}");
        if (!sql.HasContent())
        {
            throw new InvalidOperationException($"Script could not be loaded: {scriptName}");
        }

        migrationBuilder.Sql(sql);
    }
}