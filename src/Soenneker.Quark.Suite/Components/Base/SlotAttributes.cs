using System;
using System.Collections.Generic;
namespace Soenneker.Quark;

internal static class SlotAttributes
{
    public const string Name = "SlotAttributes";

    public static void MergeInto(Dictionary<string, object> attributes, IReadOnlyDictionary<string, object>? slotAttributes)
    {
        if (slotAttributes is null || slotAttributes.Count == 0)
            return;

        if (slotAttributes is Dictionary<string, object> dictionary)
        {
            foreach (var pair in dictionary)
                MergePair(attributes, pair);

            return;
        }

        foreach (var pair in slotAttributes)
            MergePair(attributes, pair);
    }

    private static void MergePair(Dictionary<string, object> attributes, KeyValuePair<string, object> pair)
    {
        if (pair.Key.Equals("class", StringComparison.OrdinalIgnoreCase))
        {
            var slotClass = pair.Value?.ToString();
            if (!string.IsNullOrWhiteSpace(slotClass))
            {
                attributes.TryGetValue("class", out var existingClassObj);
                var existingClass = existingClassObj?.ToString();
                attributes["class"] = string.IsNullOrWhiteSpace(existingClass) ? slotClass : $"{slotClass} {existingClass}";
            }

            return;
        }

        if (pair.Key.Equals("style", StringComparison.OrdinalIgnoreCase))
        {
            var slotStyle = pair.Value?.ToString();
            if (!string.IsNullOrWhiteSpace(slotStyle))
            {
                attributes.TryGetValue("style", out var existingStyleObj);
                var existingStyle = existingStyleObj?.ToString();
                attributes["style"] = string.IsNullOrWhiteSpace(existingStyle) ? slotStyle : $"{slotStyle}; {existingStyle}";
            }

            return;
        }

        if (!attributes.ContainsKey(pair.Key) && pair.Value is not null)
            attributes[pair.Key] = pair.Value;
    }
}
