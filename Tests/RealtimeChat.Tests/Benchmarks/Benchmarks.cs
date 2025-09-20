using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using Xunit.Abstractions;

namespace RealtimeChat.Tests.Benchmarks;

public class Benchmarks(ITestOutputHelper output)
{
    [Fact]
    public void Run_Benchmarks()
    {
        var logger = new AccumulationLogger();

        var config = ManualConfig.Create(DefaultConfig.Instance)
            .AddLogger(logger)
            .WithOptions(ConfigOptions.DisableOptimizationsValidator);

        BenchmarkRunner.Run<MessageSearchBenchmark>(config);
        
        output.WriteLine(logger.GetLog());
    }
}