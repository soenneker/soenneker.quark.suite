using System;
using System.Collections.Generic;
using System.Globalization;
namespace Soenneker.Quark;
public sealed class QuarkLocalizer : IQuarkLocalizer
{
    private readonly Dictionary<string, Dictionary<string, string>> _translations = new(StringComparer.OrdinalIgnoreCase);
    private static readonly Dictionary<string, string> English = new()
    {
        ["ChooseMonth"]="Choose the Month", ["ChooseYear"]="Choose the Year", ["WeekNumber"]="Week Number", ["Hour"]="Hour", ["Minute"]="Minute", ["DateRangePresets"]="Date range presets",
        ["FirstPage"]="Go to first page", ["PreviousPage"]="Go to previous page", ["NextPage"]="Go to next page", ["LastPage"]="Go to last page",
        ["Refresh"]="Refresh", ["Undo"]="Undo", ["Redo"]="Redo", ["RemoveLink"]="Remove link",
        ["ViewAdjusted"]="This view was updated to match the current fields.", ["InvalidView"]="This saved view is invalid or uses an unsupported version.",
        ["LoadingData"] = "Loading data", ["Required"] = "This field is required.", ["InvalidEmail"] = "Please enter a valid email address.",
        ["AddRule"]="Add filter", ["AddGroup"]="Add group", ["Remove"]="Remove", ["All"]="All", ["Any"]="Any",
        ["Field"]="Field", ["Operator"]="Operator", ["Value"]="Value", ["Filters"]="Filters", ["Clear"]="Clear",
        ["Close"]="Close", ["Save"]="Save", ["Cancel"]="Cancel", ["Delete"]="Delete", ["Edit"]="Edit", ["Name"]="Name",
        ["SavedViews"]="Saved views", ["SaveView"]="Save view", ["Columns"]="Columns", ["Width"]="Width",
        ["Pin"]="Pin", ["None"]="None", ["Start"]="Start", ["End"]="End", ["MoveUp"]="Move up", ["MoveDown"]="Move down",
        ["Loading"]="Loading…", ["LoadMore"]="Load more", ["Retry"]="Retry", ["LoadError"]="Could not load data.",
        ["NoResults"]="No results", ["Search"]="Search", ["Select"]="Select", ["Expand"]="Expand", ["Collapse"]="Collapse",
        ["Notifications"]="Notifications", ["Unread"]="Unread", ["MarkRead"]="Mark read", ["MarkAllRead"]="Mark all read",
        ["NoNotifications"]="No notifications", ["Action"]="Open", ["Previous"]="Previous", ["Next"]="Next", ["Today"]="Today",
        ["Day"]="Day", ["Week"]="Week", ["Month"]="Month", ["Agenda"]="Agenda", ["NewEvent"]="New event",
        ["Title"]="Title", ["Resource"]="Resource", ["NoResource"]="Unassigned", ["InvalidDate"]="Choose valid dates; the end must follow the start.",
        ["Editor"]="Rich text editor", ["Bold"]="Bold", ["Italic"]="Italic", ["Underline"]="Underline", ["BulletList"]="Bullet list",
        ["NumberedList"]="Numbered list", ["Link"]="Link", ["Mention"]="Mention", ["Attachment"]="Attachment", ["Insert"]="Insert",
        ["Equals"]="Equals", ["NotEquals"]="Does not equal", ["Contains"]="Contains", ["StartsWith"]="Starts with",
        ["GreaterThan"]="Greater than", ["LessThan"]="Less than", ["IsEmpty"]="Is empty", ["IsNotEmpty"]="Is not empty",
        ["SelectionActions"]="Selection actions", ["Selected"]="selected", ["ClearSelection"]="Clear selection",
        ["NoContent"]="No content available", ["Page"]="Page", ["InvalidFilter"]="Enter a valid filter value.",
        ["SaveError"]="Could not save changes.", ["InvalidLink"]="Use an http, https, mailto, or relative URL.",
        ["SelectAll"]="Select all rows", ["Details"]="Details", ["Reset"]="Reset", ["True"]="True", ["False"]="False"
    };
    public CultureInfo Culture { get; private set; } = CultureInfo.CurrentUICulture;
    public event Action? Changed;
    public string this[string key]
    {
        get
        {
            for (var culture = Culture; !string.IsNullOrEmpty(culture.Name); culture = culture.Parent)
                if (_translations.TryGetValue(culture.Name, out var values) && values.TryGetValue(key, out var value)) return value;
            return English.TryGetValue(key, out var fallback) ? fallback : key;
        }
    }
    /// <summary>Changes UI culture and notifies subscribed components.</summary>
    public void SetCulture(CultureInfo culture) { Culture = culture ?? throw new ArgumentNullException(nameof(culture)); Changed?.Invoke(); }
    /// <summary>Copies overrides for a culture. Parent cultures are used when a regional translation is missing.</summary>
    public void SetTranslations(string culture, IReadOnlyDictionary<string, string> values)
    { _translations[culture] = new Dictionary<string, string>(values); Changed?.Invoke(); }
}
