using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace MyLaps
{
    public class CsvParser
    {
        public List<LapModel> ParseCsv(Stream csv)
        {
            var res = new List<LapModel>();
            using (TextFieldParser parser = new TextFieldParser(csv))
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
