using System.Reflection;

namespace CraftersCloud.Blueprint.Infrastructure.Api.Init;

public static class AssemblyFinder
{
    private const string ProjectPrefix = "CraftersCloud.Blueprint";
    public static Assembly ApplicationServicesAssembly => FindAssembly("ApplicationServices");
    public static Assembly ApiAssembly => FindAssembly("Api");
    public static Assembly DomainAssembly => FindAssembly("Domain");
    public static Assembly InfrastructureAssembly => FindAssembly("Infrastructure");
    public static Assembly SchedulerAssembly => FindAssembly("Scheduler");

    private static Assembly FindAssembly(string projectSuffix) => Find($"{ProjectPrefix}.{projectSuffix}");

    public static Assembly Find(string assemblyName) => Assembly.Load(assemblyName);
}