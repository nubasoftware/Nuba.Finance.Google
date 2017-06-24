using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Nuba.Finance.Google.Readers
{
    public class LatestCandleReader
    {
        private static string DataLine = "DATA=";
        private static string TimeZoneOffsetLine = "TIMEZONE_OFFSET=";
        private static string DateSpecialLine = "A";
        private readonly CultureInfo defaultCulture = new CultureInfo("en-US");

        private readonly int numberOfSeconds;
        private int offset;
        private DateTime lastDateRead;

        public LatestCandleReader(int numberOfSeconds)
        {
            this.numberOfSeconds = numberOfSeconds;
        }

        public ICollection<Candle> Read(TextReader textReader)
        {
            string line;
            bool isHeader = true;
            var result = new List<Candle>();

            while ((line = textReader.ReadLine()) != null)
            {
                if (isHeader)
                {
                    isHeader = !line.Trim().ToUpper().Equals(DataLine);
                }
                else if (line.StartsWith(TimeZoneOffsetLine))
                {
                    offset = int.Parse(line.Substring(TimeZoneOffsetLine.Length), defaultCulture);
                }
                else
                {
                    // Column with Data
                    var values = line.Split(',');

                    // Read all Fields
                    var candle = this.GetCandle(values);
                    if (candle != null)
                        result.Add(candle);
                }
            }
            return result.OrderBy(c => c.Date).ToList();
        }

        private DateTime SetNewDates(string dateStr)
        {
            if (dateStr.ToUpper().StartsWith(DateSpecialLine))
            {
                this.lastDateRead = this.ConvertToDate(dateStr.Substring(1)).Date;
                return lastDateRead;
            }

            var valueRead = double.Parse(dateStr, defaultCulture);
            return this.lastDateRead.AddSeconds(valueRead * this.numberOfSeconds);
        }

        private DateTime ConvertToDate(string dateStr)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local)
                .AddSeconds(double.Parse(dateStr, defaultCulture));
        }

        private Candle GetCandle(string[] values)
        {
            DateTime candleDate = this.SetNewDates(values[0]);
            return new Candle()
            {
                Date = candleDate,
                Close = decimal.Parse(values[1], defaultCulture),
                High = decimal.Parse(values[2], defaultCulture),
                Low = decimal.Parse(values[3], defaultCulture),
                Open = decimal.Parse(values[4], defaultCulture),
                Volume = decimal.Parse(values[5], defaultCulture)
            };

        }
    }
}
