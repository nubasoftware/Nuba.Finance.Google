using System.CodeDom;

namespace Nuba.Finance.Google
{
    public class Frequency
    {
        public static int EverySecond => 1;
        public static int EveryMinute => 60 * EverySecond;
        public static int EveryHour => 60 * EveryMinute;
        public static int EveryDay => 24 * EveryHour;

        public static int EveryNSeconds(int number)
        {
            return number * EverySecond;
        }

        public static int EveryNMinutes(int number)
        {
            return number * EveryMinute;
        }

        public static int EveryNHours(int number)
        {
            return number * EveryHour;
        }

        public static int EveryNDays(int number)
        {
            return number * EveryDay;
        }
    }
}