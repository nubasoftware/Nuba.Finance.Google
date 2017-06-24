using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nuba.Finance.Google.Readers;

namespace Nuba.Finance.Google.Test
{
    [TestClass]
    public  class LatestCandleReaderTest
    {
        [TestMethod]
        public void TestFromLocalFile()
        {
            var reader = new LatestCandleReader(Frequency.EveryDay);
            var str = new StringReader(File.ReadAllText("Files\\latest.fb.txt"));
            var candles = reader.Read(str);

            Assert.IsNotNull(candles);
            var firstCandle = candles.First();
            Assert.AreEqual(firstCandle.Date, new DateTime(2012, 5, 17));

        }
    }
}
