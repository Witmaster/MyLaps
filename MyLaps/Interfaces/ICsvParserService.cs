using MyLaps.Models;
using System.Collections.Generic;

namespace MyLaps.Interfaces
{
    public interface ICsvParserService
    {
        List<LapModel> ParseCsv(string filePath);
    }
}
