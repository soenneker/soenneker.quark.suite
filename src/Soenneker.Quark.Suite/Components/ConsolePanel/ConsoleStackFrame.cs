using System;

namespace Soenneker.Quark;

internal readonly record struct ConsoleStackFrame(string Prefix, string? Method, string? Arguments, string? Directory, string? File)
{
    internal static ConsoleStackFrame Parse(string line)
    {
        if (!line.AsSpan().TrimStart().StartsWith("at ", StringComparison.Ordinal))
            return new(line, null, null, null, null);
        int location = line.LastIndexOf(" in ", StringComparison.Ordinal);
        ReadOnlySpan<char> method = location >= 0 ? line.AsSpan(0, location) : line.AsSpan();
        int args = method.IndexOf('(');
        ReadOnlySpan<char> signature = args >= 0 ? method[..args] : method;
        int lastDot = signature.LastIndexOf('.');
        int namespaceEnd = lastDot > 0 ? signature[..lastDot].LastIndexOf('.') + 1 : 0;
        string? directory = null;
        string? file = null;
        if (location >= 0)
        {
            ReadOnlySpan<char> path = line.AsSpan(location + 4);
            int separator = Math.Max(path.LastIndexOf('/'), path.LastIndexOf('\\'));
            directory = separator >= 0 ? path[..(separator + 1)].ToString() : "";
            file = path[(separator + 1)..].ToString();
        }
        return new(signature[..namespaceEnd].ToString(), signature[namespaceEnd..].ToString(),
            args >= 0 ? method[args..].ToString() : "", directory, file);
    }
}
