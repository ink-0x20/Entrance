using System.Text;
using System.Security.Cryptography;

namespace Entrance.Utils
{
    public static class HashUtils
    {
        // ********************************************************************************
        // ハッシュ化(SHA512)を行う
        // ********************************************************************************
        public static string Sha512(string value)
        {
            // ハッシュアルゴリズム生成
            SHA512CryptoServiceProvider algorithm = new SHA512CryptoServiceProvider();
            // ハッシュ化
            byte[] hash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(value));
            // リソース開放
            algorithm.Clear();
            // 16進数化
            StringBuilder result = new StringBuilder();
            int length = hash.Length;
            for (int i = 0; i < length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }
    }
}
