using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Nuba.Finance.Google.Test
{
    [TestClass]
    public class FrequencyTest
    {
        [TestMethod]
        public void TestEvery()
        {
            Assert.AreEqual(Frequency.EverySecond, 1);
            Assert.AreEqual(Frequency.EveryMinute, 60);
            Assert.AreEqual(Frequency.EveryHour, 60*60);
            Assert.AreEqual(Frequency.EveryDay, 24*60*60);
        }

        [TestMethod]
        public void TestEveryN()
        {
            var aNumber = new Random().Next(60);
            Assert.AreEqual(Frequency.EveryNSeconds(aNumber), aNumber * Frequency.EverySecond);
            Assert.AreEqual(Frequency.EveryNMinutes(aNumber), aNumber * Frequency.EveryMinute);
            Assert.AreEqual(Frequency.EveryNHours(aNumber), aNumber * Frequency.EveryHour);
            Assert.AreEqual(Frequency.EveryNDays(aNumber), aNumber * Frequency.EveryDay);
        }
    }
}
