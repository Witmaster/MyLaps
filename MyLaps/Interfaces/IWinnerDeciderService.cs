using MyLaps.Models;
using System.Collections.Generic;

namespace MyLaps.Interfaces
{
    public interface IWinnerDeciderService
    {
        void DecideWinner(IEnumerable<LapModel> laps);
    }
}
