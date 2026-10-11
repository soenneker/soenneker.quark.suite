using System.Linq;
using System.Threading.Tasks;
using AwesomeAssertions;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Soenneker.Blazor.Utils.ResourceLoader.Registrars;

namespace Soenneker.Quark.Suite.Tests;

public sealed class AssetLoadingTests : BunitContext
{
    [Test]
    [Arguments(false, false)]
    [Arguments(true, false)]
    [Arguments(false, true)]
    [Arguments(true, true)]
    public async Task JavaScript_and_css_can_be_selected_independently(bool minifyJavaScript, bool minifyCss)
    {
        Services.AddLogging();
        Services.AddQuarkOptionsAsScoped(new QuarkOptions { UseMinifiedJavaScript = minifyJavaScript, UseMinifiedCss = minifyCss });
        Services.AddResourceLoaderAsScoped().AddQuarkSonnerAsScoped();
        string script = "./_content/Soenneker.Quark.Suite/js/sonnerinterop" + (minifyJavaScript ? ".min.js" : ".js");
        string style = "_content/Soenneker.Quark.Suite/css/sonner" + (minifyCss ? ".min.css" : ".css");
        JSInterop.SetupModule(script);
        var loader = JSInterop.SetupModule("./_content/Soenneker.Blazor.Utils.ResourceLoader/js/resourceloader.js");
        loader.SetupVoid("loadStyle", _ => true).SetVoidResult();

        await Services.GetRequiredService<ISonnerInterop>().Initialize();

        JSInterop.Invocations["import"].Any(call => Equals(call.Arguments[0], script)).Should().BeTrue();
        loader.Invocations["loadStyle"].Any(call => Equals(call.Arguments[0], style)).Should().BeTrue();
    }
}
