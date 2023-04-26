using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyInWordsApp.CIWConverter
{
    internal static class Mappings
    {
        public const string Hundred = "hundred";
        public const string Thousand = "thousand";
        public const string Million = "million";

        public static IDictionary<string, string> Digits = new Dictionary<string, string> ()
        {
            {"0", "zero"},
            {"1", "one"},
            {"2", "two"},
            {"3", "three"},
            {"4", "four"},
            {"5", "five"},
            {"6", "six"},
            {"7", "seven"},
            {"8", "eight"},
            {"9", "nine"},
        };

        public static IDictionary<string, string> Teens = new Dictionary<string, string>()
        {
            {"10", "ten"},
            {"11", "eleven"},
            {"12", "twelve"},
            {"13", "thirteen"},
            {"14", "fourteen"},
            {"15", "fifteen"},
            {"16", "sixteen"},
            {"17", "seventeen"},
            {"18", "eighteen"},
            {"19", "nineteen"},
        };

        public static IDictionary<string, string> Tens = new Dictionary<string, string>()
        {
            {"1", "ten"},
            {"2", "twenty"},
            {"3", "thirty"},
            {"4", "forty"},
            {"5", "fifty"},
            {"6", "sixty"},
            {"7", "seventy"},
            {"8", "eighty"},
            {"9", "ninety"},
        };
    }
}
