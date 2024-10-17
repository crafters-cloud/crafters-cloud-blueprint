namespace CraftersCloud.Blueprint.Data.Migrations;

public static class DevelopmentConnectionsStrings
{
    private static readonly string DatabaseName = "CraftersCloud.Core.Blueprint"
        .Replace(".", "-", StringComparison.InvariantCulture).ToLowerInvariant();

    public static string MainConnectionString =>
        $"Server=.;Database={DatabaseName};Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";
}