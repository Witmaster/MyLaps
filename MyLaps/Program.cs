using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyLaps.Interfaces;
using MyLaps.Services;

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
                    .AddSingleton<IOutputService, OutputService>()
                    .AddSingleton<IRaceFileProcessorService, RaceFileProcessorService>();
            });
            using IHost host = builder.Build();

            var path = args[0]; //take in 1 argument - path to the .csv file with input data
            
            var processor = host.Services.GetRequiredService<IRaceFileProcessorService>();
            processor.Run(path);

            host.WaitForShutdown();
        }

        static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args);
    }
}
