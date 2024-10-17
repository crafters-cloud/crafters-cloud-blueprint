using CraftersCloud.Blueprint.Domain.Categories;
using CraftersCloud.Blueprint.Domain.Categories.Commands;
using CraftersCloud.Blueprint.Domain.Companies;
using CraftersCloud.Core.Entities;
using NUnit.Framework.Internal.Execution;

namespace CraftersCloud.Blueprint.Domain.Tests.Categories;

public class CategoryBuilder
{
    private Guid _id = SequentialGuidGenerator.Generate();
    private string _name = string.Empty;

    public CategoryBuilder WithName(string value)
    {
        _name = value;
        return this;
    }

    public CategoryBuilder WithId(Guid value) 
    {
        _id = value; 
        return this;
    }

    public Category Build()
    {
        var result = Category.Create(new CreateOrUpdateCategory.Command
        {
            Name = _name,
            Id = _id
        });
        return result;
    }

    public static Category ToCategory(CategoryBuilder builder) => builder.Build();
    public static implicit operator Category(CategoryBuilder builder) => ToCategory(builder);
}