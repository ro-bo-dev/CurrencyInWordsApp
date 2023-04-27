using CurrencyInWordsApp.CIWConverter;
using CurrencyInWordsApp.CIWConverter.Currency;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CurrencyInWordsApp.CIWConverterTest.Test
{
    [TestClass]
    public class ConversionTest
    {
        readonly CurrencyConvert Convert = new CurrencyConvert(CurrencyTicker.Ticker.USD);

        #region Test Conversion
        [TestMethod]
        public void TestAgainstSpecification()
        {
            Assert.AreEqual("zero dollars", Convert.ToWords("0"));
            Assert.AreEqual("one dollar", Convert.ToWords("1"));
            Assert.AreEqual("twenty-five dollars and ten cents", Convert.ToWords("25,1"));
            Assert.AreEqual("zero dollars and one cent", Convert.ToWords("0,01"));
            Assert.AreEqual("forty-five thousand one hundred dollars", Convert.ToWords("45 100"));
            Assert.AreEqual("nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine dollars and ninety-nine cents", Convert.ToWords("999 999 999,99"));
        }

        [TestMethod]
        public void TestCents()
        {
            Assert.AreEqual("zero dollars and two cents", Convert.ToWords("0,02"));
            Assert.AreEqual("zero dollars and twenty cents", Convert.ToWords("0,2"));
            Assert.AreEqual("one dollar and forty cents", Convert.ToWords("1,4"));
            Assert.AreEqual("one dollar and ten cents", Convert.ToWords("1,1"));
            Assert.AreEqual("one dollar and one cent", Convert.ToWords("1,01"));
            Assert.AreEqual("six hundred sixty-six million seven hundred seventy-seven thousand eight hundred eighty-eight dollars and ninety-nine cents", Convert.ToWords("666777888,99"));
            Assert.AreEqual("ten dollars and twelve cents", Convert.ToWords("10,12"));
        }

        [TestMethod]
        public void TestBlocks()
        {
            Assert.AreEqual("ten dollars", Convert.ToWords("10"));
            Assert.AreEqual("one thousand ten dollars", Convert.ToWords("1 010"));
            Assert.AreEqual("one hundred one thousand ten dollars", Convert.ToWords("101 010"));
            Assert.AreEqual("ten million one hundred one thousand ten dollars", Convert.ToWords("10 101 010"));
            Assert.AreEqual("one hundred one million one hundred one thousand ten dollars", Convert.ToWords("101 101 010"));
            Assert.AreEqual("ten million one hundred ten thousand one hundred one dollars", Convert.ToWords("10 110 101"));
            Assert.AreEqual("one million eleven thousand ten dollars", Convert.ToWords("1 011 010"));
            Assert.AreEqual("one hundred one thousand one hundred one dollars", Convert.ToWords("101 101"));
        }

        [TestMethod]
        public void TestAscending()
        {
            Assert.AreEqual("zero dollars and twelve cents", Convert.ToWords("0,12"));
            Assert.AreEqual("one dollar and twenty-three cents", Convert.ToWords("1,23"));
            Assert.AreEqual("twelve dollars and thirty-four cents", Convert.ToWords("12,34"));
            Assert.AreEqual("one hundred twenty-three dollars and forty-five cents", Convert.ToWords("123,45"));
            Assert.AreEqual("one thousand two hundred thirty-four dollars and fifty-six cents", Convert.ToWords("1234,56"));
            Assert.AreEqual("twelve thousand three hundred forty-five dollars and sixty-seven cents", Convert.ToWords("12345,67"));
            Assert.AreEqual("one hundred twenty-three thousand four hundred fifty-six dollars and seventy-eight cents", Convert.ToWords("123456,78"));
            Assert.AreEqual("one million two hundred thirty-four thousand five hundred sixty-seven dollars and eighty-nine cents", Convert.ToWords("1234567,89"));
            Assert.AreEqual("twelve million three hundred forty-five thousand six hundred seventy-eight dollars and ninety cents", Convert.ToWords("12345678,90"));
            Assert.AreEqual("one hundred twenty-three million four hundred fifty-six thousand seven hundred eighty-nine dollars and one cent", Convert.ToWords("123 456 789,01"));
        }

        [TestMethod]
        public void TestRepdigits()
        {
            Assert.AreEqual("one hundred eleven million one hundred eleven thousand one hundred eleven dollars and one cent", Convert.ToWords("111 111 111,01"));
            Assert.AreEqual("one hundred eleven million one hundred eleven thousand one hundred eleven dollars and eleven cents", Convert.ToWords("111 111 111,11"));
            Assert.AreEqual("two hundred twenty-two million two hundred twenty-two thousand two hundred twenty-two dollars and twenty-two cents", Convert.ToWords("222 222 222,22"));
            Assert.AreEqual("three hundred thirty-three million three hundred thirty-three thousand three hundred thirty-three dollars and thirty-three cents", Convert.ToWords("333 333 333,33"));
            Assert.AreEqual("four hundred forty-four million four hundred forty-four thousand four hundred forty-four dollars and forty-four cents", Convert.ToWords("444 444 444,44"));
            Assert.AreEqual("five hundred fifty-five million five hundred fifty-five thousand five hundred fifty-five dollars and fifty-five cents", Convert.ToWords("555 555 555,55"));
            Assert.AreEqual("six hundred sixty-six million six hundred sixty-six thousand six hundred sixty-six dollars and sixty-six cents", Convert.ToWords("666 666 666,66"));
            Assert.AreEqual("seven hundred seventy-seven million seven hundred seventy-seven thousand seven hundred seventy-seven dollars and seventy-seven cents", Convert.ToWords("777 777 777,77"));
            Assert.AreEqual("eight hundred eighty-eight million eight hundred eighty-eight thousand eight hundred eighty-eight dollars and eighty-eight cents", Convert.ToWords("888 888 888,88"));
        }
        #endregion

        #region Test Validation
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestArgNullException()
        {
            Convert.ToWords(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestArgEmptyException()
        {
            Convert.ToWords("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestArgWhiteSpaceOnlyException()
        {
            Convert.ToWords("   ");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestArgForbiddenCharsException()
        {
            Convert.ToWords("354,2B");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestArgTwoCommasException()
        {
            Convert.ToWords("354,22,1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestArgCommaPositionException()
        {
            Convert.ToWords("354,221");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestArgOverlongWithCommaException()
        {
            Convert.ToWords("1234567890,12");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestArgOverlongWithoutCommaException()
        {
            Convert.ToWords("1234567890");
        }
        #endregion
    }
}
