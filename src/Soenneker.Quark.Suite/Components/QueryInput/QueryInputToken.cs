namespace Soenneker.Quark;

internal sealed record QueryInputToken(int Start, int End, string Text, int Colon, bool Closed, bool HasQuotes);
