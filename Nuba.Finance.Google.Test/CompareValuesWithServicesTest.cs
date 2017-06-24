using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Nuba.Finance.Google.Test
{
    [TestClass]
    public class CompareValuesWithServicesTest
    {
        [TestMethod, Ignore]
        public void ComparePricesOfFbOnThisYearTest()
        {
            var historical = new HistoricalQuotesService();
            var latest = new LatestQuotesService();

            var fromDate = new DateTime(2017,1,1);
            var toDate = DateTime.Today;
            var symbol = "FB";
            var market = "NASDAQ";

            var histCandles = historical.GetValues(market, symbol, fromDate, toDate);
            var latCandles = latest.GetValues(market, symbol, Frequency.EveryDay, fromDate, toDate);
            Assert.AreEqual(histCandles.Count(), latCandles.Count());
            this.CompareCandles(histCandles.First(), latCandles.First());

            var randomIndex = new Random().Next(histCandles.Count() - 1);
            this.CompareCandles(histCandles.ElementAt(randomIndex), latCandles.ElementAt(randomIndex));
        }

        private void CompareCandles(Candle first, Candle second)
        {
            Assert.AreEqual(first.Date, second.Date);
            Assert.AreEqual(first.Open, second.Open);
            Assert.AreEqual(first.High, second.High);
            Assert.AreEqual(first.Low, second.Low);
            Assert.AreEqual(first.Close, second.Close);
            Assert.AreEqual(first.Volume, second.Volume);
        }

    }
}
