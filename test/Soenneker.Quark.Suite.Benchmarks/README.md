# Suite performance benchmarks

Run from the Quark repository root in Release mode, using the sibling Bradix checkout:

```powershell
dotnet run --project test/Soenneker.Quark.Suite.Benchmarks -c Release -p:UseLocalBradixProject=true -- --filter "*RenderingBenchmarks*" "*SelectFocusBenchmarks*"
dotnet run --project test/Soenneker.Quark.Suite.Benchmarks -c Release -p:UseLocalBradixProject=true -- --filter "*AttributeBenchmarks*" "*RealtimeAppendBenchmarks*"
```

`UseLocalBradixProject` switches only Bradix to a project reference; other dependencies remain on NuGet. `Program.cs` forwards this setting into BenchmarkDotNet's generated builds. The usual sibling Bradix checkout is expected; override `LocalBradixProject` if necessary, including in the benchmark build arguments.

For a short diagnostic run, append `--warmupCount 2 --iterationCount 4`. Use longer runs for small timing differences, and profile browser/WASM workloads separately before making application-level claims.

| Benchmark | What it measures |
| --- | --- |
| `GlobalRenderingBenchmarks` | Plain Blazor, Quark buffered and Quark suppressed; both unchanged and changing children at 100/500 components. Compare matching scenarios rather than the default ratio column across all methods. |
| `ChartRenderingBenchmarks` | Stable parameters, changed geometry, moving hover, and repeated hover on the same point at 100/1,000 points. |
| `CascadingRenderingBenchmarks` | Immutable versus mutable cascading contexts; mutable contexts retain detailed key evaluation. |
| `SelectFocusBenchmarks` | Repeated and changing focused items at 100/500 items, using real Bradix components with mocked JS. |
| `BradixAttributeBenchmarks` | Nine-attribute calls through an explicit tuple array and the params span overload. No renderer or JS. |
| `QuarkAttributeBenchmarks` | Attribute rebuilds with unchanged or changing class output. No renderer or JS. |
| `RealtimeAppendBenchmarks` | Explicit arrays, params spans, and batches of 16 samples. Batch results are normalized per sample. Labels are not read. |

bUnit updates its parsed DOM after renders. Its allocations include the harness, DOM processing, event dispatch and parameter construction; they are not isolated library allocations. The plain-Blazor changing baseline reproduces most of the large allocation reported for changing Quark leaves. The isolated benchmarks deliberately exclude this overhead, as well as initial component construction and dictionary capacity growth.

[September 4, 2026 results and implementation notes](PERFORMANCE-2026-09-04.md) record the current measurements and validation. The August 28 artifacts predate these changes and are not a controlled before/after comparison.
