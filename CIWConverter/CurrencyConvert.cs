using CurrencyInWordsApp.CIWConverter.Currency;
using System;
using System.Diagnostics;
using System.Text;

namespace CurrencyInWordsApp.CIWConverter
{
    public class CurrencyConvert
    {
        readonly ICurrency Currency;

        public CurrencyConvert(CurrencyTicker.Ticker currencyTicker = CurrencyTicker.Ticker.USD)
        {
            Currency = CurrencyTicker.CurrencyByTicker[currencyTicker];
        }

        /// <summary>
        /// Converts a money amount into words. The input has to be given as string. It must not contain any 
        /// chars but digits, whitespaces and optionally a single comma as separator for fractions like cents.
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public string ToWords(string amount)
        {
            if (string.IsNullOrEmpty(amount) || string.IsNullOrWhiteSpace(amount))
            {
                throw new ArgumentException("String of currency amount was null, empty or white spaces only");
            }

            return ToWords_(amount);
        }
        private string ToWords_(string amount)
        {
            amount = amount.Replace(" ", "");                   // remove all whitespaces
            var sb = new StringBuilder();

            string integer = amount;
            string fraction = "";

            if (amount.Contains(','))                           // check for fraction
            {
                var integerAndFraction = amount.Split(',');
                integer = integerAndFraction[0];
                fraction = integerAndFraction[1];
            }

            HandleInteger(integer, sb);                         // handle dollars
            HandleFraction(fraction, sb);                       // handle cents if any

            return sb.ToString();
        }

        private void HandleInteger(string integer, StringBuilder sb)
        {
            if (string.IsNullOrEmpty(integer)) return;

            int startIndex = 0;

            if (integer.Length > 6)                                                                // handle million block
            {
                string millions = integer.Substring(startIndex, integer.Length - 6);
                HandleMillions(millions, sb);
            }
            if (integer.Length > 3)                                                                // handle thousand block
            {
                startIndex = Math.Max(0, integer.Length - 6);                                      // startIndex by len 9=>3 8=>2 7=>1 6=>0 5=>0 4=>0 ..
                string thousands = integer.Substring(startIndex, Math.Min(3, integer.Length - 3));              
                HandleThousands(thousands, sb);
            }
            {                                                                                      // handle hundred block
                startIndex = Math.Max(0, integer.Length - 3);                                      // startIndex by len 9=>6 8=>5 7=>4 6=>3 5=>2 4=>1 ..
                string hundreds = integer.Substring(startIndex, Math.Min(3, integer.Length));
                HandleHundreds(hundreds, sb);
            }
            string integerUnit = integer.Length != 1 || integer[0] != '1' ? Currency.IntegerPlural : Currency.IntegerSingular; // only singular if exactly 'one dollar'
            
            if (sb[^1] != ' ')
            {
                sb.Append(' ');
            }
            sb.Append($"{integerUnit}");
        }

        private void HandleFraction(string fraction, StringBuilder sb)
        {
            if (string.IsNullOrEmpty(fraction)) return;

            sb.Append(" and ");

            HandleUpTo99(fraction, sb, false);

            string fractionUnit = fraction != "01" ? Currency.FractionPlural : Currency.FractionSingular;
            sb.Append($" {fractionUnit}");
        }

        private void HandleUpTo99(string tens, StringBuilder sb, bool isInteger = true)
        {
            if (string.IsNullOrEmpty(tens)) return;

            if (tens.Length > 2)                                                                   // only care about last 2 digits
            {
                tens = tens[^2..];
            }

            if (tens.Length == 1)                                                                  // single digit (single for dollars, tens for cents)                                              
            {
                sb.Append(isInteger ? Mappings.Digits[tens] : Mappings.Tens[tens]);
            }
            else                                                                                   // double digit with..
            {
                if (tens[0] == '1')                                                                // ..teens
                {
                    sb.Append(Mappings.Teens[tens]);
                } else                                                                             // ..tens with hyphen
                {
                    if (tens[0] != '0')                    sb.Append(Mappings.Tens[$"{tens[0]}"]);
                    if (tens[0] != '0' && tens[1] != '0')  sb.Append('-');
                    if (tens[1] != '0')                    sb.Append(Mappings.Digits[$"{tens[1]}"]);
                }
            }
        }

        private void HandleHundreds(string integer, StringBuilder sb)
        {
            if (integer.Length > 2 && integer[0] != '0')
            {
                sb.Append($"{Mappings.Digits[$"{integer[0]}"]} {Mappings.Hundred} ");
            }
            HandleUpTo99(integer, sb);
        }

        private void HandleThousands(string integer, StringBuilder sb)
        {
            HandleHundreds(integer, sb);
            sb.Append($" {Mappings.Thousand} ");
        }

        private void HandleMillions(string integer, StringBuilder sb)
        {
            HandleHundreds(integer, sb);
            sb.Append($" {Mappings.Million} ");
        }
    }
}
