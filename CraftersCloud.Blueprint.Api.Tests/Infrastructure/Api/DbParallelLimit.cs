using TUnit.Core.Interfaces;

namespace CraftersCloud.Blueprint.Api.Tests.Infrastructure.Api;

public record DbParallelLimit : IParallelLimit
{
    public int Limit => 1;
}