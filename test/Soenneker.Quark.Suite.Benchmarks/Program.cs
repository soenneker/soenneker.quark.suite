using BenchmarkDotNet.Running;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;

// Forward the local dependency setting into BenchmarkDotNet's generated builds.
var config = ManualConfig.Create(DefaultConfig.Instance)
    .AddJob(Job.Default.WithArguments([new MsBuildArgument("/p:UseLocalBradixProject=true")]).AsMutator());
BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, config);
