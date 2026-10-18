using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
namespace Soenneker.Quark;
/// <summary>Validates persisted view state and adapts known field renames and removed fields to the current schema.</summary>
public static class QuarkViewValidation
{
    public static QuarkViewState Normalize(QuarkViewState state, IReadOnlyList<FilterField>? fields = null, IReadOnlyDictionary<string, string>? aliases = null)
    {
        if (state is null || state.Version is < 0 or > 1 || state.Filters is null || state.Columns is null || state.Sorts is null)
            throw new JsonException("Unsupported or incomplete saved view.");
        string Key(string key) => aliases is not null && aliases.TryGetValue(key, out var replacement) ? replacement : key;
        bool Known(string key) => fields is null || fields.Any(f => f.Key == key);
        FilterGroup Group(FilterGroup group, int depth)
        {
            if (depth > 32 || group is null || group.Rules is null || group.Groups is null || !Enum.IsDefined(group.Junction))
                throw new JsonException("Invalid filter group.");
            var rules = new List<FilterRule>();
            foreach (var rule in group.Rules)
            {
                if (rule is null || string.IsNullOrWhiteSpace(rule.Field) || !Enum.IsDefined(rule.Operator)) throw new JsonException("Invalid filter rule.");
                var updated = rule with { Field = Key(rule.Field) };
                if (!Known(updated.Field)) continue;
                var field = fields?.FirstOrDefault(f => f.Key == updated.Field);
                if (field is not null && !FilterEvaluator.IsValid(updated, field)) throw new JsonException("Invalid saved filter value.");
                rules.Add(updated);
            }
            return group with { Rules = rules, Groups = group.Groups.Select(g => Group(g, depth + 1)).Where(g => g.Rules.Count > 0 || g.Groups.Count > 0).ToArray() };
        }
        if (state.Columns.Any(c => c is null || string.IsNullOrWhiteSpace(c.Key) || !Enum.IsDefined(c.Pin)) || state.Sorts.Any(s => s is null || string.IsNullOrWhiteSpace(s.Key)))
            throw new JsonException("Invalid saved column state.");
        return state with
        {
            Version = 1, Search = state.Search ?? "", PageSize = Math.Clamp(state.PageSize, 1, 10000), Filters = Group(state.Filters, 0),
            Columns = state.Columns.Select(c => c with { Key = Key(c.Key), Width = Math.Clamp(c.Width, 64, 1200) }).Where(c => Known(c.Key)).DistinctBy(c => c.Key).ToArray(),
            Sorts = state.Sorts.Select(s => s with { Key = Key(s.Key) }).Where(s => Known(s.Key)).DistinctBy(s => s.Key).ToArray()
        };
    }
    public static IReadOnlyList<QuarkSavedView> Validate(IReadOnlyList<QuarkSavedView> views)
    {
        if (views.Any(v => v is null || string.IsNullOrWhiteSpace(v.Id) || string.IsNullOrWhiteSpace(v.Name)) || views.Select(v => v.Id).Distinct().Count() != views.Count)
            throw new JsonException("Invalid saved views.");
        return views.Select(v => v with { State = Normalize(v.State) }).ToArray();
    }
}
