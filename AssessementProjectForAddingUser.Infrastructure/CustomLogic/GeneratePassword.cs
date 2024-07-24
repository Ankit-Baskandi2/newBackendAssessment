using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AssessementProjectForAddingUser.Infrastructure.CustomLogic
{
    public static class GeneratePassword
    {
        private const string CharPool = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz@#$%";

        public static string GenerateUniquePassword(int length=9)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            StringBuilder pass = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                byte[] randomNumber = new byte[1];
                rng.GetBytes(randomNumber);

                int index = randomNumber[0] % CharPool.Length;
                pass.Append(CharPool[index]);
            }
            return pass.ToString();
        }
    }
}
