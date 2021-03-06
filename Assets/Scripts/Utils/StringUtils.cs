namespace Entrance.Utils
{
    public static class StringUtils
    {
        // ********************************************************************************
        // 文字列の行数を取得する
        // ********************************************************************************
        public static int CountLine(string targetStr)
        {
            if (targetStr == null)
            {
                return 0;
            }
            // CRLFの文字数調整
            string str = targetStr.Replace("\r\n", "\n");
            // 行数
            return str.Length - str.Replace("\r", "").Replace("\n", "").Length + 1;
        }
    }
}
