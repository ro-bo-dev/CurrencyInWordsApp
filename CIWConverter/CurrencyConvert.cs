using System;
using System.Text;
using System.Diagnostics;
using CurrencyInWordsApp.CIWConverter.Currency;
using System.Text.RegularExpressions;

namespace CurrencyInWordsApp.CIWConverter
{
    public class CurrencyConvert
    {
        #region Properties and Constructor
        readonly ICurrency Currency;

        static readonly Regex Validator = new Regex(@"^[ \d,]+$");

        public CurrencyConvert(CurrencyTicker.Ticker currencyTicker = CurrencyTicker.Ticker.USD)
        {
            Currency = CurrencyTicker.CurrencyByTicker[currencyTicker];
        }
        #endregion

        /// <summary>
        /// Converts a money amount into words. The input has to be given as string. It must not contain any 
        /// chars but digits, whitespaces and optionally a single comma as separator for fractions like cents.
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public string ToWords(string amount)
        {
            Validate(ref amount);

            return ToWords_(amount);
        }

        #region Validator and Conversion Method
        private void Validate(ref string amount)
        {
            if (string.IsNullOrEmpty(amount))
            {
                throw new ArgumentNullException("String of currency amount was null or empty");
            }
            if (string.IsNullOrWhiteSpace(amount))
            {
                throw new ArgumentException("String of currency amount consisted of white spaces only");
            }
            if (!Validator.IsMatch(amount))
            {
                throw new ArgumentException("String of currency amount must only consist of digits, white spaces and optionally a single comma");
            }
            if (amount.IndexOf(',') != amount.LastIndexOf(','))
            {
                throw new ArgumentException("String of currency amount must not contain more than one comma");
            }

            amount = amount.Replace(" ", "");                   // remove all whitespaces

            if (!amount.Contains(",") && amount.Length > 9)
            {
                throw new ArgumentException("String of currency amount without fraction must not have more than 9 digits");
            }
            if (amount.Contains(","))
            {
                if (amount.Length < 3 || amount.Length > 12)
                {
                    throw new ArgumentException("String of currency amount with fraction must not have more than 11 digits");
                }
                if (amount.IndexOf(',') != amount.Length - 2 && amount.IndexOf(',') != amount.Length - 3)
                {
                    throw new ArgumentException("String of currency amount with fraction must have either 1 or 2 digits after the comma");
                }
            }
        }

        private string ToWords_(string amount)
        {
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
        #endregion

        #region Block Handler
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
        #endregion
    }
}
