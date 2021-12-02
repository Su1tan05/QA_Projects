using System;
using System.Linq;

namespace TDTask.Utils
{
    public static class RandomUtils
    {
        private static Random _random = new Random();
        private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghgklmnopqrstuvwxyz123456789";

        public static string GenerateText(int length)
        {
            return new string(Enumerable.Repeat(Chars, length)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }
    }
}
