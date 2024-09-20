using Autofac;
using CraftersCloud.Blueprint.Domain.Identity;
using CraftersCloud.Blueprint.Infrastructure.Identity;
using JetBrains.Annotations;

namespace CraftersCloud.Blueprint.Infrastructure.Autofac.Modules;

[UsedImplicitly]
public class IdentityModule<T> : Module where T : ICurrentUserProvider
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ClaimsProvider>().AsImplementedInterfaces().InstancePerLifetimeScope();
        builder.RegisterType<T>().AsImplementedInterfaces().InstancePerLifetimeScope();
    }
}