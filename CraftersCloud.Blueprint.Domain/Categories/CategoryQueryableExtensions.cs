namespace CraftersCloud.Blueprint.Domain.Categories;

public static class CategoryQueryableExtensions
{
    public static IQueryable<Category> QueryByName(this IQueryable<Category> query,  string? categoryName) => 
    !string.IsNullOrWhiteSpace(categoryName) 
        ? query.Where(c =>c.Name.Contains(categoryName, StringComparison.CurrentCultureIgnoreCase)) 
        : query;
}
