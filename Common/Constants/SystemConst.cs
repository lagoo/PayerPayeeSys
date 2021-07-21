using System;
using System.Globalization;

namespace Common.Constants
{
    public static class SystemConst
    {
        public const string SYSTEM_DEFAULT_DATE = "2021-01-01 00:00:00";
        public const string SYSTEM_USER_NAME = "System";
        public const int SYSTEM_USER_ID = 1;        


        public const int DECIMAL_ROUND = 6;
        public const int COUNT_TO_ELLIPSIS = 50;
        public static readonly CultureInfo DEFAULT_CULTURE = new CultureInfo("pt-br");

        public static DateTime GetDateDefault()
        {
            return DateTime.Parse(SYSTEM_DEFAULT_DATE);
        }        
    }
}
