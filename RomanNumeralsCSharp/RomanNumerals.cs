using System;
using System.IO;
using System.Linq;
using System.Text;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace RomanNumeralsCSharp
{
    public static class RomanNumerals
    {
        public static int ToInteger(this string roman)
        {
            Console.WriteLine(roman);
            return roman
                .Replace("IV", "IIII")
                .Replace("IX", "VIIII")
                .Replace("XL", "XXXX")
                .Replace("XC", "LXXXX")
                .Replace("CD", "CCCC")
                .Replace("CM", "DCCCC")
                .Sum(c => c.GetArabicValue());
        }

        private static int GetArabicValue(this char c)
        {
            switch (c)
            {
                case 'I': return 1;
                case 'V': return 5;
                case 'X': return 10;
                case 'L': return 50;
                case 'C': return 100;
                case 'D': return 500;
                case 'M': return 1000;
            }
            throw new ArgumentOutOfRangeException();
        }

        public static string ToRoman(this int number)
        {
            var result = "";
            var value = number;
            result += GetLetters(value, 'M', 1000);
            value = value % 1000;
            result += GetLetters(value, 'D', 500);
            value = value % 500;
            result += GetLetters(value, 'C', 100);
            value = value % 100;
            result += GetLetters(value, 'L', 50);
            value = value % 50;
            result += GetLetters(value, 'X', 10);
            value = value % 10;
            result += GetLetters(value, 'V', 5);
            value = value % 5;
            result += GetLetters(value, 'I', 1);

            return result
                .Replace("DCCCC", "CM")
                .Replace("CCCC", "CD")
                .Replace("LXXXX", "XC")
                .Replace("XXXX", "XL")
                .Replace("VIIII", "IX")
                .Replace("IIII", "IV");
        }

        private static string GetLetters(int value, char letter, int amountPerLetter)
        {
            var remainder = value % amountPerLetter;
            var numLetters = (value - remainder)/amountPerLetter;
            return new String(letter, numLetters);
        }


    }

    public class Tests
    {
        //private readonly ITestOutputHelper Console;

        public Tests(ITestOutputHelper output)
        {
        }

        [Fact]
        public void ToRoman_Test()
        {
            for (var i = 1; i < 2000; i++)
            {
                var roman = i.ToRoman();
                roman.ToInteger().Should().Be(i);
            }
        }
    }
}
