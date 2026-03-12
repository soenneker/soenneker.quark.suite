using System;

namespace Soenneker.Quark;

/// <summary>
/// Marks a static method as a build-time service configurator for headless Blazor rendering
/// (e.g. Tailwind class extraction). The method must have signature
/// <c>static void Method(IServiceCollection services)</c>.
/// Discovered and invoked by Soenneker.Quark.Gen.Tailwind BuildTasks.
/// </summary>
[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public sealed class ConfigureBuildTimeServicesAttribute : Attribute
{
}
