using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Soenneker.Node.Util.Abstract;
using Soenneker.Node.Util.Registrars;

namespace Soenneker.Quark.Suite.Tests;

public sealed class JavaScriptInteropTests
{
    [Test]
    public async Task Browser_modules_pass_regression_tests()
    {
        DirectoryInfo? root = new(AppContext.BaseDirectory);
        while (root != null && !Directory.Exists(Path.Combine(root.FullName, "test", "js")))
            root = root.Parent;

        if (root == null)
            throw new DirectoryNotFoundException("Cannot locate the repository's test/js directory.");

        string[] files = Directory.GetFiles(Path.Combine(root.FullName, "test", "js"), "*.test.mjs");
        if (files.Length == 0)
            throw new InvalidOperationException("No JavaScript regression tests were found.");
        Array.Sort(files, StringComparer.Ordinal);

        var services = new ServiceCollection();
        services.AddLogging();
        services.AddSingleton<IConfiguration>(new ConfigurationBuilder().Build());
        services.AddNodeUtilAsSingleton();
        await using ServiceProvider provider = services.BuildServiceProvider();
        using var timeout = new CancellationTokenSource(TimeSpan.FromMinutes(20));
        string node = await provider.GetRequiredService<INodeUtil>().EnsureInstalled(cancellationToken: timeout.Token);

        var start = new ProcessStartInfo(node)
        {
            WorkingDirectory = root.FullName,
            UseShellExecute = false
        };
        start.ArgumentList.Add("--test");
        foreach (string file in files)
            start.ArgumentList.Add(file);

        using Process process = Process.Start(start) ?? throw new InvalidOperationException("Could not start Node.");
        try
        {
            await process.WaitForExitAsync(timeout.Token);
        }
        finally
        {
            if (!process.HasExited)
            {
                process.Kill(entireProcessTree: true);
                await process.WaitForExitAsync();
            }
        }

        if (process.ExitCode != 0)
            throw new InvalidOperationException($"JavaScript regression tests failed with exit code {process.ExitCode}.");
    }
}

