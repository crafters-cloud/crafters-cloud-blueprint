using Autofac;
using CraftersCloud.Blueprint.Domain.Authorization;
using CraftersCloud.Blueprint.Infrastructure.Authorization;
using CraftersCloud.Core.AspNetCore.Authorization;
using JetBrains.Annotations;

namespace CraftersCloud.Blueprint.Infrastructure.Autofac.Modules;

[UsedImplicitly]
public class AuthorizationModule : Module
{
    protected override void Load(ContainerBuilder builder) => builder.RegisterType<DefaultAuthorizationProvider>().As<IAuthorizationProvider<PermissionId>>().InstancePerLifetimeScope();
}