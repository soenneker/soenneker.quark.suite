# Quark rendering benchmarks

Run the rendering comparison in Release mode:

```powershell
dotnet run --project test/Soenneker.Quark.Suite.Benchmarks/Soenneker.Quark.Suite.Benchmarks.csproj -c Release -- --filter "*RenderingBenchmarks*"
```

The benchmark compares plain Blazor rendering, Quark rendering with suppression disabled (`AlwaysRender = true`), and Quark's default hash-based render suppression. It covers stable complex parameters, where Blazor must conservatively revisit a child, and changing values, where every child must render.

`ChartRenderingBenchmarks` separately measures stable chart parameters, changed data references, and hover-only updates at 100 and 1,000 points. This keeps expensive geometry work and interaction rendering visible as independent performance signals.

`CascadingRenderingBenchmarks` compares unchanged children receiving a known immutable cascade with children receiving a mutable reference context, which intentionally retains detailed render-key evaluation for correctness.

On the first optimized .NET 10 run, stable 1,000-point delivery took approximately 4 microseconds and allocated 6 KB. Replacing the data reference and rebuilding the complete geometry took approximately 4.93 milliseconds and allocated 10.94 MB. A hover-only update retained the static SVG, legend, and accessibility table and took approximately 1.13 milliseconds with 3.15 MB allocated in bUnit—about 77% less time and 71% less allocation than a geometry rebuild. bUnit synchronizes its parsed DOM after an event, so treat the interaction result as a conservative signal rather than isolated browser render cost.

For 500 unchanged children, a known immutable cascading value used the fast path in approximately 143 microseconds, versus approximately 352 microseconds for a mutable reference context that correctly retained detailed render-key evaluation. The immutable path was about 2.5 times faster; allocation differences include the different generic `CascadingValue<T>` representations and are not directly comparable.

On the initial .NET 10 workstation run, suppression offered essentially no improvement for unchanged immutable scalar parameters because Blazor already skipped those children. With stable complex parameters, the optimized suppression path reduced a 500-component update from approximately 481 microseconds to 77 microseconds and reduced allocations from approximately 93 KB to 27 KB. The earlier detailed-key implementation took approximately 135 microseconds for the same suppressed update. The latest full run measured approximately 39–40 milliseconds when all 500 child values changed, with suppression and normal rendering remaining effectively equivalent. Treat results as machine-specific and rerun them when changing the rendering pipeline.
