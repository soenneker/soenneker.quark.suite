using System.Reflection;
using BenchmarkDotNet.Attributes;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Soenneker.Bradix;
using Soenneker.Blazor.Utils.ModuleImport.Abstract;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;

namespace Soenneker.Quark.Suite.Benchmarks;

// These include bUnit DOM processing. Use the isolated attribute benchmarks for
// library-only allocations; compare focus scaling within this harness.
[MemoryDiagnoser]
public class SelectFocusBenchmarks
{
    private BunitContext _context = null!;
    private IRenderedComponent<BradixSelect> _select = null!;
    private Func<string?, Task> _setFocus = null!;
    private string[] _values = [];
    private int _index;

    [Params(100, 500)]
    public int ItemCount { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        _values = Enumerable.Range(0, ItemCount).Select(i => $"item-{i}").ToArray();
        _context = new BunitContext();
        _context.JSInterop.SetupModule("./_content/Soenneker.Bradix.Suite/js/bradix.js").Mode = JSRuntimeMode.Loose;
        _context.Services.AddBradixSuiteAsScoped();
        _context.Services.AddScoped<IModuleImportUtil, BenchmarkModuleImportUtil>();
        _context.Services.AddScoped<IResourceLoader, BenchmarkResourceLoader>();
        _select = _context.Render<BradixSelect>(p => p.AddChildContent(builder =>
        {
            foreach (string value in _values)
            {
                builder.OpenComponent<BradixSelectItem>(0);
                builder.AddAttribute(1, nameof(BradixSelectItem.Value), value);
                builder.AddAttribute(2, nameof(BradixSelectItem.TextValue), value);
                builder.CloseComponent();
            }
        }));
        _setFocus = typeof(BradixSelect).GetMethod("SetFocusedItemFromScript", BindingFlags.Instance | BindingFlags.NonPublic)!
            .CreateDelegate<Func<string?, Task>>(_select.Instance);
        _select.InvokeAsync(() => _setFocus(_values[0])).GetAwaiter().GetResult();
    }

    [Benchmark(Baseline = true)]
    public Task SameItem() => _select.InvokeAsync(() => _setFocus(_values[0]));

    [Benchmark]
    public Task ChangeItem()
    {
        _index = (_index + 1) % ItemCount;
        return _select.InvokeAsync(() => _setFocus(_values[_index]));
    }

    [GlobalCleanup]
    public async Task Cleanup() => await _context.DisposeAsync();
}
