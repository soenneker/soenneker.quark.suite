using Intellenum;

namespace Soenneker.Quark.Enums;

[Intellenum<string>]
public partial class ListGroupMode
{
    public static readonly ListGroupMode Static = new("static");
    public static readonly ListGroupMode Selectable = new("selectable");
}
