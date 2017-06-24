using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Nuba.Finance.Google.Readers;
using Nuba.Finance.Google.Util;

namespace Nuba.Finance.Google
{
    public class LatestQuotesService
    {
        private readonly RestCaller restCaller;
        
        /// <summary>
        /// Base Google finance URL
        /// </summary>
        /// <example>http://www.google.com/finance/getprices?q=QQQ&x=NASDAQ&i=86400&p=47Y&f=d,c,h,l,o,v Gets all history,
        /// I understand p = 47Y means bring information for the last 47 years but p = 1 means 1 day </example>
        private const string BaseUrl = "http://www.google.com/finance/getprices?q={0}&x={1}&i={2}&p={3}&f=d,c,h,l,o,v";

        private const int FirstYear = 1990;

        public LatestQuotesService()
        {
            this.restCaller = new RestCaller();
        }

        public IEnumerable<Candle> GetValues(string market, string symbol, int numberOfSeconds, 
            DateTime? from = null, DateTime? to = null)
        {
            try
            {
                var url = this.GetUrl(market, symbol, numberOfSeconds, from, to);
                var response = this.restCaller.Get(url);
                if (response == null)
                    throw new QuotesServiceException(market, symbol, from, to);
                if (response.Length == 0)
                    return new Candle[0];

                var reader = new LatestCandleReader(numberOfSeconds);
                return reader.Read(new StringReader(response))
                    .Where(c => (!from.HasValue || c.Date >= from.Value) &&
                                (!to.HasValue || c.Date <= to.Value));
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

        private string GetUrl(string market, string symbol, int numberOfSeconds, 
            DateTime? from = null, DateTime? to = null)
        {
            var pValue = this.GetPeriodValueFrom(market, symbol, from, to);
            return string.Format(BaseUrl, symbol, market, numberOfSeconds, pValue);
        }

        private string GetPeriodValueFrom(string market, string symbol,
            DateTime? from = null, DateTime? to = null)
        {
            if ((to.HasValue && !from.HasValue) || 
                (!to.HasValue && from.HasValue) ||
                (to.HasValue && from.HasValue && to.Value.Date < from.Value.Date))
                throw new QuotesServiceException(market, symbol, from, to, "Date Validation ocurr. From and To Dates should be together or none of  them. From date should be smaller than To Date" );

            if (!to.HasValue && !from.HasValue)
                return (DateTime.Today.Year - FirstYear) + "Y";

            // We use DateTime.Today instead of To.Value because Google only returns data from today and not
            // between dates.
            var numberOfDays = (DateTime.Today - from.Value).Days;
            if (numberOfDays < 50)
                return  numberOfDays + "d";
            if (numberOfDays < 1500)
                return ((numberOfDays / 25) + 1) + "M";

            return ((numberOfDays / 320) + 1) + "Y";
        }
    }
}
