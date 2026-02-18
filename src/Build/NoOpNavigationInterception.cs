using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Routing;

namespace Soenneker.Quark.Build;

/// <summary>
/// No-op implementation of <see cref="INavigationInterception"/> for headless build-time
/// Blazor rendering (e.g. Tailwind class extraction). Use When registering build-time
/// services via <see cref="ConfigureBuildTimeServicesAttribute"/>.
/// </summary>
public sealed class NoOpNavigationInterception : INavigationInterception
{
    /// <inheritdoc />
    public Task EnableNavigationInterceptionAsync() => Task.CompletedTask;
}
