using System.Reflection;
using CraftersCloud.Blueprint.Infrastructure.Api.Init;
using Enigmatry.Entry.MediatR;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CraftersCloud.Blueprint.Infrastructure.Init;

public static class MediatRStartupExtensions
{
    public static void AppAddMediatR(this IServiceCollection services, params Assembly[] extraAssemblies)
    {
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(extraAssemblies);
            config.RegisterServicesFromAssemblies(
                AssemblyFinder.DomainAssembly,
                AssemblyFinder.ApplicationServicesAssembly);
        });
    }
}