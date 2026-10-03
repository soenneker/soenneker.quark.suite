namespace Soenneker.Quark;

internal sealed record MessageContext(string From)
{
    internal static readonly MessageContext Assistant = new("assistant");
    internal static readonly MessageContext User = new("user");
}
