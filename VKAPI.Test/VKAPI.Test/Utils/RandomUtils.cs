using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKAPI.Test.Utils
{
    public static class RandomUtils
    {
        private static Random random = new Random();
        private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghgklmnopqrstuvwxyz";

        public static string GetRandomText(int textLength)
        {
            return new string(Enumerable.Repeat(chars, textLength)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
