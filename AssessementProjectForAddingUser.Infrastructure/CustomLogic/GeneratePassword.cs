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
        private const string ValidCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()";

        public static string GenerateUniquePassword(int length = 12)
        {
            if (length <= 0)
            {
                throw new ArgumentException("Password length must be greater than zero.", nameof(length));
            }

            using (var rng = new RNGCryptoServiceProvider())
            {
                var byteArray = new byte[length];
                rng.GetBytes(byteArray);

                return new string(byteArray.Select(b => ValidCharacters[b % ValidCharacters.Length]).ToArray());
            }
        }
    }
}
