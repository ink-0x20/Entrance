namespace Entrance.Utils
{
    public static class StringUtils
    {
        // ********************************************************************************
        // ������̍s�����擾����
        // ********************************************************************************
        public static int CountLine(string targetStr)
        {
            if (targetStr == null)
            {
                return 0;
            }
            // CRLF�̕���������
            string str = targetStr.Replace("\r\n", "\n");
            // �s��
            return str.Length - str.Replace("\r", "").Replace("\n", "").Length + 1;
        }
    }
}
