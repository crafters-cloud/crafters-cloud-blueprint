using CraftersCloud.Blueprint.Domain.Companies;
using CraftersCloud.Blueprint.Domain.Companies.Commands;
using CraftersCloud.Core.Entities;

namespace CraftersCloud.Blueprint.Domain.Tests.Companies;

public class CompanyBuilder
{
    private string _name = string.Empty;
    private Guid _id = SequentialGuidGenerator.Generate();

    public CompanyBuilder WithName(string value)
    {
        _name = value;
        return this;
    }

    public CompanyBuilder WithId(Guid value) 
    { 
        _id = value; 
        return this; 
    }

    public Company Build()
    {
        var result = Company.Create(new CreateOrUpdateCompany.Command
        {
            Name = _name,
            Id = _id
        });

        return result;
    }

    public static Company ToCompany(CompanyBuilder builder) => builder.Build();

    public static implicit operator Company(CompanyBuilder builder) => ToCompany(builder);
}
