using System;
using System.Collections;
using System.Globalization;

namespace Archangel.Tests.WebEditor.Infrastructure.Services
{
    public class WeekCompareService : IWeekCompareService 
    {
        private readonly CultureInfo _cultureInfo;
        private readonly CalendarWeekRule myCWR;
        private readonly DayOfWeek myFirstDOW;

        public WeekCompareService()
        {
            _cultureInfo = new CultureInfo("en-GB");
            myCWR = _cultureInfo.DateTimeFormat.CalendarWeekRule;
            myFirstDOW = _cultureInfo.DateTimeFormat.FirstDayOfWeek;
        }

        public int Compare(DateTime x, DateTime y)
        {
            Calendar myCal = _cultureInfo.Calendar;

            if (myCal.GetWeekOfYear(x, myCWR, myFirstDOW) == myCal.GetWeekOfYear(y, myCWR, myFirstDOW))
                return 0;
            else if (myCal.GetWeekOfYear(x, myCWR, myFirstDOW) < myCal.GetWeekOfYear(y, myCWR, myFirstDOW))
                return -1;
            else
                return 1;
        }
    }
}