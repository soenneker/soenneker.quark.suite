using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

public sealed class BootstrapBreadcrumbItemCssVariables : IBootstrapCssVariableGroup
{
	public string? Divider { get; set; }

    public string GetSelector()
    {
        return ".breadcrumb-item + .breadcrumb-item::before";
    }

    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (Divider.HasContent())
            yield return ("--bs-breadcrumb-divider", Divider);
    }
}