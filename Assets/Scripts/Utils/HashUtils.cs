using System.Text;
using System.Security.Cryptography;

namespace Entrance.Utils
{
    public static class HashUtils
    {
        // ********************************************************************************
        // �n�b�V����(SHA512)���s��
        // ********************************************************************************
        public static string Sha512(string value)
        {
            // �n�b�V���A���S���Y������
            SHA512CryptoServiceProvider algorithm = new SHA512CryptoServiceProvider();
            // �n�b�V����
            byte[] hash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(value));
            // ���\�[�X�J��
            algorithm.Clear();
            // 16�i����
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
