using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;

namespace Nuba.Finance.Google.Readers
{
    public class HistoricalCandleReader
    {
        private readonly CultureInfo defaultCulture = new CultureInfo("en-US");

        public ICollection<Candle> Read(TextReader textReader)
        {
            using (var csvReader = new CsvReader(textReader, new CsvConfiguration
            {
                CultureInfo = this.defaultCulture,
                Delimiter = ",",
            }))
            {
                var result = new List<Candle>();
                while (csvReader.Read())
                {
                    var candle = GetCandle(csvReader);
                    if (candle != null)
                        result.Add(candle);
                }
                return result.OrderBy(c => c.Date).ToList();
            }
        }

        private Candle GetCandle(CsvReader csvReader)
        {
            var date = csvReader.GetField<DateTime>("Date");
            var volume = csvReader.GetField<decimal>("Volume");
            if (volume.Equals(0))
                return null;
            var high = csvReader.GetField<decimal>("High");
            var low = csvReader.GetField<decimal>("Low");
            var close = csvReader.GetField<decimal>("Close");
            var open = csvReader.GetField<decimal>("Open");

            return new Candle()
            {
                Date = date,
                Open = open,
                High = high,
                Low = low,
                Close = close,
                Volume = volume,
            };
        }
    }
}