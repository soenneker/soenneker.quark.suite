using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark
{
    /// <summary>
    /// Delegate for building class attributes with a ref PooledStringBuilder
    /// </summary>
    public delegate void BuildClassAction(ref PooledStringBuilder builder);
}
