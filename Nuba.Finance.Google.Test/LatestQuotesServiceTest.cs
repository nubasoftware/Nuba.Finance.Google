using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Nuba.Finance.Google.Test
{
    [TestClass]
    public class LatestQuotesServiceTest
    {
        [TestMethod]
        public void CallServiceByDayWithoutDatesTest()
        {
            LatestQuotesService hqs = new LatestQuotesService();
            var candles = hqs.GetValues("NASDAQ", "FB", Frequency.EveryDay);

            Assert.IsNotNull(candles);
            Assert.IsTrue(candles.Count() > 200);
            Assert.AreEqual(candles.First().Date, new DateTime(2012,5,17));
        }

        [TestMethod]
        public void CallServiceByDayWithDatesTest()
        {
            var dateFrom = new DateTime(2017, 1, 1);
            var dateTo = new DateTime(2017, 5, 1);
            LatestQuotesService hqs = new LatestQuotesService();
            var candles = hqs.GetValues("NASDAQ", "FB", Frequency.EveryDay, dateFrom, dateTo);

            Assert.IsNotNull(candles);
            Assert.AreEqual(candles.Count(), 82);
            Assert.AreEqual(candles.First().Date, dateFrom.AddDays(2));
            Assert.AreEqual(candles.Last().Date, dateTo);
        }

        [TestMethod]
        public void CallServiceEvery15minutes()
        {
            var dateFrom = new DateTime(2017, 6, 28);
            var dateTo = new DateTime(2017, 7, 5);
            LatestQuotesService hqs = new LatestQuotesService();
            var candles = hqs.GetValues("NASDAQ", "FB", Frequency.EveryNMinutes(15), dateFrom, dateTo);

            Assert.IsNotNull(candles);
            Assert.AreEqual(candles.First().Date.Date, dateFrom);
            Assert.AreEqual(candles.Last().Date.Date, dateTo);
        }

        [TestMethod]
        public void CallServiceEvery30minutes()
        {
            var dateFrom = new DateTime(2017, 6, 28);
            var dateTo = new DateTime(2017, 7, 5);
            LatestQuotesService hqs = new LatestQuotesService();
            var candles = hqs.GetValues("NASDAQ", "FB", Frequency.EveryNMinutes(30), dateFrom, dateTo);

            Assert.IsNotNull(candles);
            Assert.AreEqual(candles.First().Date.Date, dateFrom);
            Assert.AreEqual(candles.Last().Date.Date, dateTo);
        }

        [TestMethod]
        public void CallServiceEveryHour()
        {
            var dateFrom = new DateTime(2017, 6, 28);
            var dateTo = new DateTime(2017, 7, 5);
            LatestQuotesService hqs = new LatestQuotesService();
            var candles = hqs.GetValues("NASDAQ", "FB", Frequency.EveryHour, dateFrom, dateTo);

            Assert.IsNotNull(candles);
            Assert.AreEqual(candles.First().Date.Date, dateFrom);
            Assert.AreEqual(candles.Last().Date.Date, dateTo);
        }

        [TestMethod]
        public void CallServicesWithDifferentFrequencyAndCompareValues()
        {
            var dateFrom = new DateTime(2017, 6, 28);
            var dateTo = new DateTime(2017, 7, 5);
            LatestQuotesService hqs = new LatestQuotesService();
            var hourlyCandles = hqs.GetValues("NASDAQ", "FB", Frequency.EveryHour, dateFrom, dateTo);
            var halfHourlycandles = hqs.GetValues("NASDAQ", "FB", Frequency.EveryNMinutes(30), dateFrom, dateTo);

            var specificDateTime = dateFrom.Add(new TimeSpan(12, 30, 0));
            var candleFor1230to1330 = hourlyCandles.FirstOrDefault(c => c.Date.Equals(specificDateTime));
            var candleFor1230to1300 = halfHourlycandles.FirstOrDefault(c => c.Date.Equals(specificDateTime));
            var candleFor1300to1330 = halfHourlycandles.FirstOrDefault(c => c.Date.Equals(specificDateTime.AddMinutes(30)));

            Assert.IsNotNull(candleFor1230to1330);
            Assert.IsNotNull(candleFor1230to1300);
            Assert.IsNotNull(candleFor1300to1330);
            Assert.IsTrue(candleFor1230to1330.Open.Equals(candleFor1230to1300.Open));
            Assert.IsTrue(candleFor1230to1330.Close.Equals(candleFor1300to1330.Close));
        }
    }
}
