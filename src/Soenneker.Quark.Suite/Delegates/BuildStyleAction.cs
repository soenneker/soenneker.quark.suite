using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Delegate for building style attributes with a ref PooledStringBuilder  
/// </summary>
public delegate void BuildStyleAction(ref PooledStringBuilder builder);