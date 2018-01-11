using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Nuba.Finance.Google.Readers;
using Nuba.Finance.Google.Util;

namespace Nuba.Finance.Google
{
    public class HistoricalQuotesService
    {
        private readonly HistoricalCandleReader reader;
        private readonly RestCaller restCaller;

        /// <summary>
        /// Base Google finance URL
        /// </summary>
        private const string BaseUrl = "http://finance.google.com/finance/historical?q={0}{1}&output=csv";

        public HistoricalQuotesService()
        {
            this.restCaller = new RestCaller();
            this.reader = new HistoricalCandleReader();
       }

        public IEnumerable<Candle> GetValues(string market, string symbol, DateTime? from = null, DateTime? to = null)
        {
            try
            {
                var url = this.GetUrl(market, symbol, from, to);
                var response = this.restCaller.Get(url);
                if (response == null)
                {
                    var message = "Response was empty. Probably the combination symbol:market does not exist";
                    throw new QuotesServiceException(market, symbol, from, to, message);
                }
                if (response.Length == 0)
                    return new Candle[0];

                response = response.Replace("\n", Environment.NewLine);
                return reader.Read(new StringReader(response));
            }
            catch (QuotesServiceException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new QuotesServiceException(market, symbol, from, to, e);
            }

        }

        private string GetUrl(string market, string symbol, DateTime? startDate, DateTime? endDate)
        {
            var datesStr = "";
            var culture = new CultureInfo("en-US");
            if (startDate.HasValue)
                datesStr += "&startdate=" + startDate.Value.ToString("MMM+dd+yyyy", culture);
            if (endDate.HasValue)
                datesStr += "&enddate=" + endDate.Value.ToString("MMM+dd+yyyy", culture);
            return string.Format(BaseUrl, market + "%3A" + symbol, datesStr);
        }

    }
}
