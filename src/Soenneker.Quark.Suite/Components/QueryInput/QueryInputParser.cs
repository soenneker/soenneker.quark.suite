using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soenneker.Quark;

/// <summary>Parses whitespace-separated qualifiers, quoted values, escaped quotes/backslashes, free text, and case-insensitive AND/OR operators without executing queries.</summary>
public static class QueryInputParser
{
    /// <summary>Parses a query. When fields are supplied, unknown qualifiers are reported as errors. Suggested values are not an allowlist.</summary>
    public static QueryInputResult Parse(string? query, IReadOnlyList<QueryInputField>? fields = null)
    {
        var filters = new List<QueryInputFilter>();
        var text = new List<string>();
        var errors = new List<string>();
        QueryInputExpression? conjunction = null, disjunction = null;
        QueryInputOperator? pending = null;
        foreach (QueryInputToken token in Tokenize(query ?? ""))
        {
            if (!token.Closed) errors.Add($"Unclosed quote at position {token.Start}.");
            if (IsOperator(token))
            {
                if (conjunction is null || pending is not null)
                    errors.Add($"Unexpected operator '{token.Text}' at position {token.Start}.");
                else pending = string.Equals(token.Text, "OR", StringComparison.OrdinalIgnoreCase) ? QueryInputOperator.Or : QueryInputOperator.And;
                continue;
            }
            QueryInputExpression operand;
            if (token.Colon < 0)
            {
                text.Add(token.Text);
                operand = new QueryInputTextExpression(token.Text);
            }
            else
            {
                string key = token.Text[..token.Colon];
                bool excluded = key.StartsWith('-');
                if (excluded) key = key[1..];
                string value = token.Text[(token.Colon + 1)..];
                if (key.Length == 0) errors.Add($"Missing field at position {token.Start}.");
                else if (fields is not null && !fields.Any(f => string.Equals(f.Key, key, StringComparison.OrdinalIgnoreCase)))
                    errors.Add($"Unknown field '{key}'.");
                if (value.Length == 0) errors.Add($"Missing value for '{key}'.");
                var filter = new QueryInputFilter(key, value, excluded);
                filters.Add(filter);
                operand = new QueryInputFilterExpression(filter);
            }
            if (pending == QueryInputOperator.Or && conjunction is not null)
            {
                disjunction = disjunction is null ? conjunction : new QueryInputBinaryExpression(disjunction, QueryInputOperator.Or, conjunction);
                conjunction = operand;
            }
            else conjunction = conjunction is null ? operand : new QueryInputBinaryExpression(conjunction, QueryInputOperator.And, operand);
            pending = null;
        }
        if (pending is not null) errors.Add("Missing operand after logical operator.");
        QueryInputExpression? expression = disjunction is null ? conjunction : new QueryInputBinaryExpression(disjunction, QueryInputOperator.Or, conjunction!);
        return new QueryInputResult(string.Join(" ", text), filters.ToArray(), errors.ToArray()) { Expression = expression };
    }

    internal static bool IsOperator(QueryInputToken token) => token.Colon < 0 && !token.HasQuotes &&
        (string.Equals(token.Text, "AND", StringComparison.OrdinalIgnoreCase) || string.Equals(token.Text, "OR", StringComparison.OrdinalIgnoreCase));

    /// <summary>Encodes a value for safe insertion as one query token.</summary>
    public static string Quote(string value) => value.Length == 0 || string.Equals(value, "AND", StringComparison.OrdinalIgnoreCase) || string.Equals(value, "OR", StringComparison.OrdinalIgnoreCase) || value.Any(c => char.IsWhiteSpace(c) || c is '"' or '\\' or ':')
        ? "\"" + value.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\"" : value;

    internal static IReadOnlyList<QueryInputToken> Tokenize(string query)
    {
        var tokens = new List<QueryInputToken>();
        int i = 0;
        while (i < query.Length)
        {
            if (char.IsWhiteSpace(query[i])) { i++; continue; }
            int start = i, colon = -1;
            bool quoted = false, hasQuotes = false;
            var value = new StringBuilder();
            for (; i < query.Length; i++)
            {
                char c = query[i];
                if (c == '\\' && i + 1 < query.Length && query[i + 1] is '"' or '\\') { value.Append(query[++i]); continue; }
                if (c == '"') { quoted = !quoted; hasQuotes = true; continue; }
                if (!quoted && char.IsWhiteSpace(c)) break;
                if (!quoted && c == ':' && colon < 0) colon = value.Length;
                value.Append(c);
            }
            tokens.Add(new QueryInputToken(start, i, value.ToString(), colon, !quoted, hasQuotes));
        }
        return tokens;
    }
}
