using CraftersCloud.Core.Helpers;

namespace CraftersCloud.Blueprint.Domain.Companies;

public static class CompanyQueryableExtensions
{
    public static IQueryable<Company> QueryByName(this IQueryable<Company> query, string? name) =>
        !string.IsNullOrWhiteSpace(name) ?
            query.Where(x => x.Name.Contains(name, StringComparison.CurrentCultureIgnoreCase))
            : query;
}
