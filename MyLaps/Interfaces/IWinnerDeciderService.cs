using System.Collections.Generic;

namespace MyLaps.Interfaces
{
    interface IWinnerDeciderService
    {
        void DecideWinner(IEnumerable<LapModel> laps);
    }
}
