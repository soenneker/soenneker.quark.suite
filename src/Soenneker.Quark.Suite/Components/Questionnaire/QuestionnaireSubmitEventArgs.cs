using System;
using System.Collections.Generic;

namespace Soenneker.Quark;

/// <summary>
/// Contains the answers submitted by a questionnaire.
/// </summary>
public sealed class QuestionnaireSubmitEventArgs : EventArgs
{
    internal QuestionnaireSubmitEventArgs(IReadOnlyDictionary<string, IReadOnlyList<string>> answers)
    {
        Answers = answers;
    }

    /// <summary>
    /// Gets submitted answers keyed by item name. Skipped items are omitted.
    /// </summary>
    public IReadOnlyDictionary<string, IReadOnlyList<string>> Answers { get; }

    /// <summary>
    /// Gets the first submitted answer for an item, or <see langword="null"/>.
    /// </summary>
    /// <param name="name">Optional name used to scope the generated variant.</param>
    /// <returns>The requested text.</returns>
    public string? Get(string name) => Answers.TryGetValue(name, out var values) && values.Count > 0 ? values[0] : null;

    /// <summary>
    /// Gets every submitted answer for an item.
    /// </summary>
    /// <param name="name">Optional name used to scope the generated variant.</param>
    /// <returns>The requested collection.</returns>
    public IReadOnlyList<string> GetAll(string name) => Answers.TryGetValue(name, out var values) ? values : [];
}
