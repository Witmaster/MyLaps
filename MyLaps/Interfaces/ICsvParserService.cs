using MyLaps.Models;
using System.Collections.Generic;
using System.IO;

namespace MyLaps.Interfaces
{
    public interface ICsvParserService
    {
        List<LapModel> ParseCsv(Stream csv);
    }
}
