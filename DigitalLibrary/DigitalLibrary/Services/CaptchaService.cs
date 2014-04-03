using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalLibrary.Services
{
    public static class CaptchaService
    {
        private static char GenerateChar(Random rnd)
        {
            if (rnd == null)
                rnd = new Random();

            return Convert.ToChar(rnd.Next(97, 122));
        }

        public static string GenerateCaptchaValue(int size = DigitalLibrary.Internal.Constants.CaptchaSize)
        {
            string value = "";
            Random rnd = new Random();
            for (int i = 0; i < size; i++)
                value += GenerateChar(rnd);

            return value;
        }
    }
}