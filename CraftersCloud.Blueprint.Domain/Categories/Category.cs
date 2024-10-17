using CraftersCloud.Blueprint.Core.Entities;
using CraftersCloud.Blueprint.Domain.Categories.Commands;
using CraftersCloud.Blueprint.Domain.Categories.DomainEvents;

namespace CraftersCloud.Blueprint.Domain.Categories;

public class Category : EntityWithCreatedUpdated
{
    public const int NameMaxLength = 200;
    public static readonly Guid SystemId = new Guid("51113C28-4A04-490F-87B4-1384CC9BE46D");
    public string Name { get; private set; } = string.Empty;

    public static Category Create(CreateOrUpdateCategory.Command command)
    {
        var result = new Category { Name = command.Name };
        result.AddDomainEvent(new CategoryCreatedDomainEvent(result.Name));
        return result; 
    }

    public void Update(CreateOrUpdateCategory.Command command)
    {
        Name = command.Name;
        AddDomainEvent(new CategoryUpdatedDomainEvent(Name));
    }

}

