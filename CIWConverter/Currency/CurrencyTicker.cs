﻿using System.Collections.Generic;

namespace CurrencyInWordsApp.CIWConverter.Currency
{
    public class CurrencyTicker
    {
        public enum Ticker { USD }

        public static IDictionary<Ticker, ICurrency> CurrencyByTicker = new Dictionary<Ticker, ICurrency>()
        {
            {Ticker.USD, new Dollar()}
        };
    }
}
