using System.Globalization;

namespace Soenneker.Quark;

public partial class EventTimeline
{
    private sealed class ItemView
    {
        private string? _testId;
        private string? _formattedTime;
        private CultureInfo? _culture;
        internal EventTimelineItem Item { get; }
        internal string DateTimeText { get; }
        internal ItemView(EventTimelineItem item)
        {
            Item = item;
            DateTimeText = item.When.ToString("O", CultureInfo.InvariantCulture);
        }
        internal string? GetTestId(string? prefix) => prefix is not null && !string.IsNullOrWhiteSpace(Item.Key)
            ? QuarkStringCache.Concat(prefix, "", Item.Key, ref _testId) : null;
        internal string GetFormattedTime()
        {
            var culture = CultureInfo.CurrentCulture;
            if (_formattedTime is null || !culture.IsReadOnly || !ReferenceEquals(culture, _culture))
            {
                _formattedTime = Item.When.ToString("g", culture);
                _culture = culture;
            }
            return _formattedTime;
        }
    }
}
