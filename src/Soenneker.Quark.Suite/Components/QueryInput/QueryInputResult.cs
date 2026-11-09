using System.Collections.Generic;

namespace Soenneker.Quark;

/// <summary>Parsed query text. Applications must validate and translate filters before executing queries.</summary>
public sealed record QueryInputResult(string Text, IReadOnlyList<QueryInputFilter> Filters, IReadOnlyList<string> Errors);
