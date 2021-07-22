using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MyLaps
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = args[0]; //take in 1 argument - path to the .csv file with input data
            var file = File.OpenRead(path);
            var laps = new CsvParser().ParseCsv(file);
            Decide(laps);
            Console.ReadLine();
        }

        static void Decide(List<LapModel> laps)
        {
            var racerLaps = laps.GroupBy(x => x.Kart);
            // assume max laps is total number as by definition total number is fixed, but never explicitly stated
            var totalLaps = racerLaps.Select(x => x.Count()).Max();
            //ignore values after first racer finishes last lap
            var lastValidLap = racerLaps.Where(x => x.Count() == totalLaps).Select(x => x.OrderByDescending(v => v.PassingTime).First()).OrderBy(v => v.PassingTime).First();
            var lastValidLapIndex = laps.IndexOf(lastValidLap);
            var validLaps = laps.Take(lastValidLapIndex + 1);

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
            Console.WriteLine($"Winner is {winner.Kart}! Best lap: #{lapNumber} at {winner.PassingTime:HH:mm:ss}, with lap time {bestLap}");
        }
    }
}
