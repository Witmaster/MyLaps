using MyLaps.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyLaps.Services
{
    public class WinnerDeciderService : IWinnerDeciderService
    {
        private readonly IOutputService _outputService;
        public WinnerDeciderService(IOutputService outputService)
        {
            _outputService = outputService;
        }
        public void DecideWinner(IEnumerable<LapModel> laps)
        {
            var racerLaps = laps.GroupBy(x => x.Kart);
            // assume max laps is total number as by definition total number is fixed, but never explicitly stated
            var totalLaps = racerLaps.Select(x => x.Count()).Max();
            //ignore values after first racer finishes last lap
            var lastValidLap = racerLaps.Where(x => x.Count() == totalLaps).Select(x => x.OrderByDescending(v => v.PassingTime).First()).OrderBy(v => v.PassingTime).First();

            var validLaps = laps.Where(x => x.PassingTime <= lastValidLap.PassingTime);

            racerLaps = validLaps.OrderBy(x => x.PassingTime).GroupBy(x => x.Kart);

            LapModel winner = null;
            TimeSpan bestLap = TimeSpan.MaxValue;
            int lapNumber = 0;
            foreach (var group in racerLaps)
            {
                //start time is not given in requirements, nor can be inferred from example data, so assuming first lap can't be winning
                LapModel reference = group.First();
                for (int i = 1; i < group.Count(); i++)
                {
                    var lap = group.ElementAt(i);
                    if (reference != null)
                    {
                        var lapTime = lap.PassingTime - reference.PassingTime;
                        if (bestLap > lapTime)
                        {
                            bestLap = lapTime;
                            winner = lap;
                            lapNumber = i + 1;
                        }
                    }
                    reference = lap;
                }
            }
            _outputService.WriteLine($"Winner is {winner.Kart}! Best lap: #{lapNumber} at {winner.PassingTime:HH:mm:ss}, with lap time {bestLap}");
        }
    }
}
