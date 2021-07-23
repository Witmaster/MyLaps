using MyLaps.Interfaces;
using System.IO;

namespace MyLaps.Services
{
    public class RaceFileProcessorService : IRaceFileProcessorService
    {
        private readonly ICsvParserService _csvParserService; 
        private readonly IWinnerDeciderService _winnerDeciderService;
        public RaceFileProcessorService(ICsvParserService csvParserService, IWinnerDeciderService winnerDeciderService)
        {
            _csvParserService = csvParserService;
            _winnerDeciderService = winnerDeciderService;
        }
        public void Run(string filePath)
        {
            var file = File.OpenRead(filePath);
            var laps = _csvParserService.ParseCsv(file);
            _winnerDeciderService.DecideWinner(laps);
        }
    }
}
