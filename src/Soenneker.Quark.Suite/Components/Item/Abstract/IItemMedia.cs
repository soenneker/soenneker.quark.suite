using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

public interface IItemMedia : IElement
{
    ItemMediaVariant Variant { get; set; }
}
