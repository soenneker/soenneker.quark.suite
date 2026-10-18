using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
namespace Soenneker.Quark;
/// <summary>Shared Quark element behavior and live localization for stateful workflow controls.</summary>
public abstract class LocalizedComponentBase : Element, IDisposable
{
    [Inject] protected IQuarkLocalizer Text { get; set; } = null!;
    protected new bool IsDisposed { get; private set; }
    protected override bool AlwaysRender => true;
    protected override void OnInitialized() { base.OnInitialized(); Text.Changed += RefreshLocalized; }
    private void RefreshLocalized() { if (!IsDisposed) RefreshOffThread(); }
    protected Dictionary<string, object> WorkflowAttributes(string slot, string classes)
    {
        var attributes = new Dictionary<string, object>(BuildAttributes());
        attributes.TryAdd("data-slot", slot);
        PrependClassAttribute(attributes, classes);
        return attributes;
    }
    protected void ReportError(Exception exception, string operation) => Logger.LogWarning(exception, "Quark {Component} failed during {Operation}", GetType().Name, operation);
    public virtual void Dispose() { if (IsDisposed) return; IsDisposed = true; Text.Changed -= RefreshLocalized; }
    public override async ValueTask DisposeAsync() { Dispose(); await base.DisposeAsync(); }
}
