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
    }
}
