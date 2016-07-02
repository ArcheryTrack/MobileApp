using System;
using System.Collections.Generic;

namespace ATMobile.Constants
{
    public class LocationConstants
    {
        //TODO - Move to database and should sync down from server

        public static List<string> USA_States = new List<string> () {
            "AL", "AK", "AZ", "AR", "CA", "CO", "CT", "DE", "FL", "GA",
            "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA", "ME", "MD",
            "MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ",
            "NM", "NY", "NC", "ND", "OH", "OK", "OR", "PA", "RI", "SC",
            "SD", "TN", "TX", "UT", "VT", "VA", "WA", "WV", "WI", "WY", "DC"
        };

        public static List<string> CA_Provinces = new List<string> () {
            "AB", "BC", "MB", "NB", "NL", "NS", "NT", "NU", "ON", "PE",
            "QC", "SK", "YT"
        };

        public static List<string> Countries = new List<string> () { "USA", "CAN" };
    }
}

