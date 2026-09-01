using System.Threading.Tasks;
using AwesomeAssertions;
using Microsoft.Playwright;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
[NotInParallel]
public sealed class QuarkSpinnerPlaywrightTests : QuarkPlaywrightTest
{
    public QuarkSpinnerPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Spinner_uses_material_clipped_circle_structure_without_permanent_compositor_hints()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAsync($"{BaseUrl}components/spinner", new PageGotoOptions { WaitUntil = WaitUntilState.DOMContentLoaded });
        await page.WaitForFunctionAsync("() => typeof window.getDotnetRuntime === 'function'");
        await page.Locator(".quark-spinner").First.WaitForAsync();
        await page.WaitForFunctionAsync(
            "() => { const circle = document.querySelector('.quark-spinner-circle'); return circle && Number.parseFloat(getComputedStyle(circle).strokeWidth) > 0; }");

        var result = await page.EvaluateAsync<string>(
            """
            () => {
                const expectedSizes = new Set([12, 16, 24, 32]);
                const matchedWidths = new Set();

                for (const spinner of document.querySelectorAll('.quark-spinner')) {
                    const circle = spinner.querySelector('.quark-spinner-circle');
                    if (!circle) continue;

                    const size = Math.round(spinner.getBoundingClientRect().width);
                    if (!expectedSizes.has(size)) continue;

                    const rootStyle = getComputedStyle(spinner);
                    const circleStyle = getComputedStyle(circle);

                    if (rootStyle.contentVisibility !== 'visible')
                        return `size ${size}: content-visibility is ${rootStyle.contentVisibility}`;

                    if (circleStyle.willChange !== 'auto')
                        return `size ${size}: will-change is ${circleStyle.willChange}`;

                    if (!(circle instanceof SVGCircleElement))
                        return `size ${size}: active indicator is not an SVG circle`;

                    if (spinner.querySelectorAll('.quark-spinner-circle-graphic').length !== 3)
                        return `size ${size}: expected Material's three circle graphics`;

                    if (spinner.querySelectorAll('.quark-spinner-clipper').length !== 2 ||
                        spinner.querySelectorAll('.quark-spinner-gap-patch').length !== 1)
                        return `size ${size}: clipped-circle structure is incomplete`;

                    if (Number.parseFloat(circleStyle.strokeWidth) !== 8)
                        return `size ${size}: vector stroke is ${circleStyle.strokeWidth}, expected 8 view-box units`;

                    matchedWidths.add(size);
                }

                const missingWidths = [...expectedSizes].filter(size => !matchedWidths.has(size));
                return missingWidths.length === 0 ? 'ok' : `missing sizes: ${missingWidths.join(', ')}`;
            }
            """);

        result.Should().Be("ok");
    }

    [Test]
    public async ValueTask Spinner_stops_motion_when_reduced_motion_is_requested()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.EmulateMediaAsync(new PageEmulateMediaOptions { ReducedMotion = ReducedMotion.Reduce });
        await page.GotoAsync($"{BaseUrl}components/spinner", new PageGotoOptions { WaitUntil = WaitUntilState.DOMContentLoaded });
        await page.WaitForFunctionAsync("() => typeof window.getDotnetRuntime === 'function'");

        var spinner = page.Locator(".quark-spinner").First;
        await spinner.WaitForAsync();

        var animationState = await spinner.EvaluateAsync<string>(
            """
            spinner => {
                const animated = [...spinner.querySelectorAll('.quark-spinner-indeterminate, .quark-spinner-layer, .quark-spinner-circle-graphic, .quark-spinner-circle')]
                    .map(element => `${element.className.baseVal ?? element.className}: ${getComputedStyle(element).animationName}`)
                    .filter(value => !value.endsWith(': none'));
                return animated.length === 0 ? 'ok' : animated.join(', ');
            }
            """);

        animationState.Should().Be("ok");
    }

    [Test]
    public async ValueTask Spinner_uses_material_animation_names_durations_and_easing()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAsync($"{BaseUrl}components/spinner", new PageGotoOptions { WaitUntil = WaitUntilState.DOMContentLoaded });
        await page.WaitForFunctionAsync("() => typeof window.getDotnetRuntime === 'function'");

        var spinner = page.Locator(".quark-spinner").First;
        await spinner.WaitForAsync();

        var result = await spinner.EvaluateAsync<string>(
            """
            spinner => {
                const container = getComputedStyle(spinner.querySelector('.quark-spinner-indeterminate'));
                const layer = getComputedStyle(spinner.querySelector('.quark-spinner-layer'));
                const left = getComputedStyle(spinner.querySelector('.quark-spinner-left .quark-spinner-circle-graphic'));
                const right = getComputedStyle(spinner.querySelector('.quark-spinner-right .quark-spinner-circle-graphic'));

                if (container.animationName !== 'quark-spinner-container-rotate' || container.animationDuration !== '1.568s' || container.animationTimingFunction !== 'linear')
                    return `container: ${container.animationName} ${container.animationDuration} ${container.animationTimingFunction}`;
                if (layer.animationName !== 'quark-spinner-layer-rotate' || layer.animationDuration !== '5.332s' || layer.animationTimingFunction !== 'cubic-bezier(0.4, 0, 0.2, 1)')
                    return `layer: ${layer.animationName} ${layer.animationDuration} ${layer.animationTimingFunction}`;
                if (left.animationName !== 'quark-spinner-left-spin' || right.animationName !== 'quark-spinner-right-spin')
                    return `halves: ${left.animationName}, ${right.animationName}`;
                if (left.animationDuration !== '1.333s' || right.animationDuration !== '1.333s')
                    return `half durations: ${left.animationDuration}, ${right.animationDuration}`;
                return 'ok';
            }
            """);

        result.Should().Be("ok");
    }
}
