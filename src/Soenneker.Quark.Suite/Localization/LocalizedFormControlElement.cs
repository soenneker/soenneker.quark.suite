using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
namespace Soenneker.Quark;
/// <summary>Form control behavior with shared localized text and live culture updates.</summary>
public abstract class LocalizedFormControlElement : FormControlElement
{
    [Inject] private IServiceProvider LocalizationServices { get; set; } = null!;
    protected IQuarkLocalizer Localizer { get; private set; } = null!;
    private int _localizationVersion;
    private bool _localizationDisposed;
    protected override void OnInitialized()
    {
        base.OnInitialized();
        var fallback = new QuarkLocalizer(); fallback.SetCulture(CultureInfo.CurrentCulture);
        Localizer = LocalizationServices.GetService(typeof(IQuarkLocalizer)) as IQuarkLocalizer ?? fallback;
        Localizer.Changed += CultureChanged;
    }
    private void CultureChanged() => _ = InvokeAsync(() =>
    {
        if (_localizationDisposed) return;
        _localizationVersion++; OnLocalizationChanged(); Refresh();
    });
    protected virtual void OnLocalizationChanged() { }
    protected override void ComputeRenderKeyCore(ref HashCode hash) { base.ComputeRenderKeyCore(ref hash); hash.Add(_localizationVersion); }
    public override async ValueTask DisposeAsync()
    {
        if (_localizationDisposed) return;
        _localizationDisposed = true;
        if (Localizer is not null) Localizer.Changed -= CultureChanged;
        await base.DisposeAsync();
    }
}
