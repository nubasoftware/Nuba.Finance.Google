using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nuba.Finance.Google
{
    public class QuotesServiceException : Exception

    {
        public string Market { get; set; }
        public string Symbol { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public QuotesServiceException(string market, string symbol, DateTime? startDate, DateTime? endDate, string message = "")
            : base(BuildMessage(market, symbol, startDate, endDate, message))
        {
            this.Market = market;
            this.Symbol = symbol;
            this.StartDate = startDate;
            this.EndDate = endDate;
        }


        public QuotesServiceException(string market, string symbol, DateTime? startDate, DateTime? endDate,
            Exception innerException) : this(market, symbol, startDate, endDate, BuildMessage(market,symbol,startDate,endDate), innerException)
        {
        }

        public QuotesServiceException(string market, string symbol, DateTime? startDate, DateTime? endDate,
            string message, Exception innerException) 
            : base(BuildMessage(market, symbol, startDate, endDate, message), innerException)
        {
            this.Market = market;
            this.Symbol = symbol;
            this.StartDate = startDate;
            this.EndDate = endDate;
        }

        private static string BuildMessage(string market, string symbol, DateTime? startDate, DateTime? endDate, string message = "")
        {
            message = string.Format("nuba.Finance.Google failed. {0} - Error while getting information for {1}:{2}", message, market, symbol) ;
            if (startDate.HasValue)
                message += string.Format(" with start date = {0}", startDate.Value);
            if (endDate.HasValue)
                message += string.Format(" with end date = {0}", endDate.Value);
            return message;
        }
    }
}
