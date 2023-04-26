using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyInWordsApp.CIWConverter.Currency
{
    public class Dollar : ICurrency
    {
        public string IntegerSingular { get => "dollar"; }

        public string IntegerPlural { get => "dollars"; }

        public string FractionSingular { get => "cent"; }

        public string FractionPlural { get => "cents"; }
    }
}
