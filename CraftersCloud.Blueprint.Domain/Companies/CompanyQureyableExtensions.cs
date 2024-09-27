namespace CraftersCloud.Blueprint.Domain.Companies;

public static class CompanyQureyableExtensions
{
    public static IQueryable<Company> QueryByName(this IQueryable<Company> query, string? name) =>
        query.Where(x => x.Name == name);
}
