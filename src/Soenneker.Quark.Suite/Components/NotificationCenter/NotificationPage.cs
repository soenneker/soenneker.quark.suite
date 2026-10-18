using System.Collections.Generic;
namespace Soenneker.Quark;
/// <summary>A notification page and opaque continuation token. UnreadCount may include unloaded notifications.</summary>
public sealed record NotificationPage(IReadOnlyList<QuarkNotification> Items, string? ContinuationToken = null, int? UnreadCount = null);
