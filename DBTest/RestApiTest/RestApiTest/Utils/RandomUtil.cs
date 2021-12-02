using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApiTest.Utils
{
    public static class RandomUtil
    {
        private static Random random = new Random();
        private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghgklmnopqrstuvwxyz";
        private const int textLength = 20;

        public static string GetRandomText()
        {
            return new string(Enumerable.Repeat(chars, textLength)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static int GetRandomNumber(int maxValue)
        {
            return random.Next(maxValue);
        }
    }
}
