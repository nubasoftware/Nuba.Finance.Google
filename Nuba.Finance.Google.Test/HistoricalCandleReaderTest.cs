using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nuba.Finance.Google.Readers;

namespace Nuba.Finance.Google.Test
{
    [TestClass]
    public class HistoricalCandleReaderTest
    {
        [TestMethod]
        public void TestFromLocalFile()
        {
            var reader = new HistoricalCandleReader();
            var str = new StringReader(File.ReadAllText("Files\\historical.fb.csv"));
            var candles = reader.Read(str);
            
            Assert.IsNotNull(candles);
            Assert.AreEqual(candles.Count, 874);

            var firstCandle = candles.First();
            Assert.AreEqual(firstCandle.Date, new DateTime(2014, 1, 2));
        }
    }
}
