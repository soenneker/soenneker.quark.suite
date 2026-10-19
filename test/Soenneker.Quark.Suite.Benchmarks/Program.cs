using BenchmarkDotNet.Running;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;

if (args is ["--construction-catalog", var output])
{
    Soenneker.Quark.Suite.Benchmarks.ComponentConstructionCatalog.Measure(output);
    return;
}

// Forward the local dependency setting into BenchmarkDotNet's generated builds.
var config = ManualConfig.Create(DefaultConfig.Instance)
    .AddJob(Job.Default.WithArguments([new MsBuildArgument("/p:UseLocalBradixProject=true"), new MsBuildArgument("/p:UseLocalQuarkBuildersProject=true"), new MsBuildArgument("/p:UseLocalQuarkGenPresetsProject=true")]).AsMutator());
BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, config);
