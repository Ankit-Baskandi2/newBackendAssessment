using System.Text;

namespace AssessementProjectForAddingUser.Infrastructure.CustomLogic
{
    public static class EncriptionAndDecription
    {
        public static string EncryptData(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return null;
            }
            else
            {
                byte[] storeData = ASCIIEncoding.ASCII.GetBytes(data);
                string encryptData = Convert.ToBase64String(storeData);
                return encryptData;
            }
        }

        public static string DecryptData(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return null;
            }
            else
            {
                byte[] encryptedData = Convert.FromBase64String(data);
                string decryptData = ASCIIEncoding.ASCII.GetString(encryptedData);
                return decryptData;
            }
        }
    }
}
