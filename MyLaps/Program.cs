using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyLaps.Interfaces;
using MyLaps.Services;
using System.IO;

namespace MyLaps
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = CreateHostBuilder(args);
            builder.ConfigureServices(sp =>
            {
                sp.AddSingleton<ICsvParserService, CsvParserService>()
                    .AddSingleton<IWinnerDeciderService, WinnerDeciderService>()
                    .AddSingleton<IOutputService, OutputService>();
            });
            using IHost host = builder.Build();

            var path = args[0]; //take in 1 argument - path to the .csv file with input data
            var file = File.OpenRead(path);
            var parser = host.Services.GetRequiredService<ICsvParserService>();
            var laps = parser.ParseCsv(file);
            var decider = host.Services.GetRequiredService<IWinnerDeciderService>();
            decider.DecideWinner(laps);

            host.WaitForShutdown();
        }

        static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args);
    }
}
