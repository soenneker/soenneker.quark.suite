using System;
using System.Globalization;
namespace Soenneker.Quark;
/// <summary>Shared scoped UI text and formatting culture. Register a replacement to use application resources.</summary>
public interface IQuarkLocalizer
{
    /// <summary>Current UI culture.</summary>
    CultureInfo Culture { get; }
    /// <summary>Returns translated text, with English fallback and then the key itself.</summary>
    string this[string key] { get; }
    /// <summary>Raised when culture or translations change.</summary>
    event Action? Changed;
}
