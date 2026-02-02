using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark
{
    /// <summary>
    /// Delegate for building both class and style attributes with ref PooledStringBuilders
    /// </summary>
    public delegate void BuildClassAndStyleAction(ref PooledStringBuilder classBuilder, ref PooledStringBuilder styleBuilder);
}
