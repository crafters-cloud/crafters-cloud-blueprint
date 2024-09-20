using System.Runtime.CompilerServices;
using CraftersCloud.Blueprint.Common.Tests;
using CraftersCloud.Blueprint.Infrastructure.Api.Init;

namespace CraftersCloud.Blueprint.Api.Tests;

public static class VerifierSettingsInitializer
{
    [ModuleInitializer]
    public static void Init() => CommonVerifierSettingsInitializer.Init(AssemblyFinder.ApiAssembly);
}