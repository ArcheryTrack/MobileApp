﻿using System;
namespace ATMobile.Helpers
{
    public static class DateHelper
    {
        public static string ToDisplayDate (this DateTime _date)
        {
            return _date.ToString ("d");
        }

        public static string ToDisplayDateTime (this DateTime _date)
        {
            return _date.ToString ("g");
        }
    }
}

