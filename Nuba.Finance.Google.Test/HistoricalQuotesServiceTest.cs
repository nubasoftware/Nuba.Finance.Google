using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nuba.Finance.Google.Test.Util;

namespace Nuba.Finance.Google.Test
{
    [TestClass]
    public class HistoricalQuotesServiceTest
    {
        [TestMethod]
        public void CallHistoricalServiceWithoutDatesTest()
        {
            HistoricalQuotesService hqs = new HistoricalQuotesService();
            var candles = hqs.GetValues(Market.NASDAQ, "AAPL");

            Assert.IsNotNull(candles);
            Assert.IsTrue(candles.Any());
        }

        [TestMethod]
        public void CallHistoricalServiceWithDatesTest()
        {
            HistoricalQuotesService hqs = new HistoricalQuotesService();
            var candles = hqs.GetValues(Market.NASDAQ, "FB", new DateTime(2014, 1,2), DateTime.Today);

            Assert.IsNotNull(candles);
            Assert.IsTrue(candles.Any());
            Assert.IsTrue(candles.First().Date == new DateTime(2014,1,2));
        }

        [TestMethod]
        public void ShouldFailBecauseInstrumentDoesNotExist()
        {
            HistoricalQuotesService hqs = new HistoricalQuotesService();
            var expectedException =
                ExceptionAssert.Throws<QuotesServiceException>(() =>
                    hqs.GetValues(Market.NYSE, "FAIL", new DateTime(2014, 1, 2), DateTime.Today)
                );
            Assert.AreEqual(expectedException.Market, Market.NYSE);
        }

    }
}
