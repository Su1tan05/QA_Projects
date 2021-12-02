using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;


namespace VKAPI.Test.Utils
{
    public static class ImageComparisonUtil
    {
        private static readonly int imageSize = 16;
        private static readonly int percentageOfSimilarity = 99;
        private static readonly int maxCountOfPixels = 256;
        private static double countOfPixels = PercentToPixels();

        public static bool CompareImages(string firstImgPath, string secondImgPath)
        {
            List<bool> iHash1 = GetHash(new Bitmap(firstImgPath));
            List<bool> iHash2 = GetHash(new Bitmap(secondImgPath));
            int equalElements = iHash1.Zip(iHash2, (i, j) => i == j).Count(eq => eq);
            if (equalElements >= countOfPixels)
                return true;
            else
                return false;
        }

        private static List<bool> GetHash(Bitmap bmpSource)
        {
            List<bool> lResult = new List<bool>();
            Bitmap bmpMin = new Bitmap(bmpSource, new Size(imageSize, imageSize));
            for (int j = 0; j < bmpMin.Height; j++)
            {
                for (int i = 0; i < bmpMin.Width; i++)
                {
                    lResult.Add(bmpMin.GetPixel(i, j).GetBrightness() < 0.5f);
                }
            }
            return lResult;
        }

        private static double PercentToPixels() => maxCountOfPixels * percentageOfSimilarity * 0.01;
    }
}
