using System;
namespace Soenneker.Quark;
/// <summary>A persistent notification supplied by the application, with a stable ID and optional group and action label.</summary>
public sealed record QuarkNotification(string Id, string Title, string? Body, DateTimeOffset CreatedAt, bool IsRead = false, string? Group = null, string? ActionLabel = null);
