using System;
using System.Linq;

namespace HD.Site.Common
{
    public class RandomPassword
    {
        private static readonly string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly string _upperChars = _chars.ToUpper();
        private static readonly string _lowerChars = _chars.ToLower();
        private static readonly string _numberChars = "0123456789";
        private static readonly Random _random = new Random();

        public static string GetARandomPassword()
        {
            string partUpper = new string(Enumerable.Repeat(_upperChars, 4).Select(s => s[_random.Next(s.Length)]).ToArray());
            string partLower = new string(Enumerable.Repeat(_lowerChars, 4).Select(s => s[_random.Next(s.Length)]).ToArray());
            string partNumber = new string(Enumerable.Repeat(_numberChars, 4).Select(s => s[_random.Next(s.Length)]).ToArray());
            return partUpper + partLower + partNumber;
        }
    }
}