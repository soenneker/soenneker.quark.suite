using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
namespace Soenneker.Quark;
/// <summary>A persistent inbox presentation. Applications own storage, delivery, and action routing.</summary>
public interface INotificationCenter : IElement
{
    /// <summary>Reloads the first page, superseding pending loads while preserving successful local reads.</summary>
    Task RefreshItems();
    /// <summary>Loaded notifications, with stable unique IDs.</summary>
    IReadOnlyList<QuarkNotification> Items { get; set; }
    /// <summary>Receives replacement loaded items after paging or read-state changes.</summary>
    EventCallback<IReadOnlyList<QuarkNotification>> ItemsChanged { get; set; }
    /// <summary>Optional paged backend; a null token requests the first page.</summary>
    Func<string?, CancellationToken, ValueTask<NotificationPage>>? Load { get; set; }
    /// <summary>Persists read state for supplied IDs. Mark all read affects loaded notifications only.</summary>
    Func<IReadOnlyList<string>, CancellationToken, ValueTask>? MarkRead { get; set; }
    /// <summary>Handles an action and marks the notification read after successful completion.</summary>
    EventCallback<QuarkNotification> Action { get; set; }
    /// <summary>Optional server unread total, including unloaded notifications.</summary>
    int? UnreadCount { get; set; }
    /// <summary>Receives a changed server unread total.</summary>
    EventCallback<int?> UnreadCountChanged { get; set; }
}
