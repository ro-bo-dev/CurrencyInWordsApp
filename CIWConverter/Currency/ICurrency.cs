using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyInWordsApp.CIWConverter.Currency
{
    public interface ICurrency
    {
        string IntegerSingular { get; }
        string IntegerPlural { get; }

        string FractionSingular { get; }
        string FractionPlural { get; }
    }
}
