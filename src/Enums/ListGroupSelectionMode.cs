using Intellenum;

namespace Soenneker.Quark.Enums;

[Intellenum<string>]
public partial class ListGroupSelectionMode
{
    public static readonly ListGroupSelectionMode Single = new("single");
    public static readonly ListGroupSelectionMode Multiple = new("multiple");
}
