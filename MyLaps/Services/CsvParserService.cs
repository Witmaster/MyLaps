using Microsoft.VisualBasic.FileIO;
using MyLaps.Interfaces;
using MyLaps.Models;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace MyLaps.Services
{
    public class CsvParserService : ICsvParserService
    {
        public List<LapModel> ParseCsv(string filePath)
        {
            var res = new List<LapModel>();
            using (TextFieldParser parser = new TextFieldParser(filePath))
            {
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields(); //reads next row
                    if (!DateTime.TryParseExact(fields[1], "hh:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out var time))
                    {
                        continue; //skip header and possible corrupted data. It is also possible to map data based on headers, but it wasn't required
                    }
                    var item = new LapModel
                    {
                        Kart = fields[0],
                        PassingTime = time
                    };
                    res.Add(item);
                }
            }
            return res;
        }
    }
}
