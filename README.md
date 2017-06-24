# Nuba.Finance.Google

Nuba.Finance.Google is an open source library built to provide a simple and easy to use API to get latest and historical prices from different Markets all over the world.

This library is based on Google Finance services.

I will try to put some lines of code to make coding as easy as possible.

## Namespace: 
Nuba.Finance.Google

All examples will understand that you have add a line:

            using Nuba.Finance.Google;

To make coding shorter.

## Historical Prices
To get candlebars for a specific instrument you should create a new instance of HistoricalQuotesService and then call GetValues method with the market name and the symbol name.

            HistoricalQuotesService hqs = new HistoricalQuotesService();
            var candles = hqs.GetValues(Market.NASDAQ, "AAPL");

You can also use the GetValues with more parameters to get prices between two specific dates. To do that you just need to add parameters to the same call GetValues.

            HistoricalQuotesService hqs = new HistoricalQuotesService();
            var candles = hqs.GetValues(Market.NASDAQ, "FB", new DateTime(2014, 1,2), DateTime.Today);


## Latest Prices
To get candlebars for a specific instrument you should create a new instance of LatestQuotesService and then call GetValues method with the market name and the symbo name as well as the frecuency of the candle bars you want.

            LatestQuotesService hqs = new LatestQuotesService();
            var candles = hqs.GetValues("NASDAQ", "FB", Frequency.EveryDay);

You can also use the GetValues with more parameters to get prices between two specific dates. To do that you just need to add parameters to the same call GetValues.

            LatestQuotesService hqs = new LatestQuotesService();
            var candles = hqs.GetValues(Market.NASDAQ, "FB", Frequency.EveryDay,
            				new DateTime(2016, 1, 1), new DateTime(2016, 12, 31));


## Helpers

### Market
Market is a simple class to avoid writing magic strings. Market just contains static fields with the different names of the markets. I will be adding more markets as I have time.
Usage is as simple as:

			Market.NYSE

### Frequency
Frequency is a simple class that provides a lot of methods that simply calculates the number of seconds of a specific period. This way, instead of using 86.400 as a not so understandable int value (24 hours * 60 minutes * 60 seconds) you just use Frecuency.Everyday.

If you want different candle bars, as a five minute bar you can do:

			Frequency.EveryNMinutes(5)

and for 30 seconds bar you can do:

			Frequency.EveryNSeconds(30)

Please, feel free to contact us for any improvements to the library as we are just sharing something that it's in use in our company. 

You can download this library through Nuget with the name Nuba.Finance.Google.

## Credits
I have to give credit to a [CodeProject article](https://www.codeproject.com/Articles/221952/Simple-Csharp-DLL-to-download-data-from-Google-Fin) written by a Spanish guy, [Juan1R](https://www.codeproject.com/Members/Juan1R), that make me detect some strange cases with the Google Latest Quotes Services. 

Visit us at www.nuba.com.ar.


