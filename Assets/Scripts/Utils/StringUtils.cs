namespace Entrance.Utils
{
    public static class StringUtils
    {
        // ********************************************************************************
        // •¶š—ñ‚Ìs”‚ğæ“¾‚·‚é
        // ********************************************************************************
        public static int CountLine(string targetStr)
        {
            if (targetStr == null)
            {
                return 0;
            }
            // CRLF‚Ì•¶š”’²®
            string str = targetStr.Replace("\r\n", "\n");
            // s”
            return str.Length - str.Replace("\r", "").Replace("\n", "").Length + 1;
        }
    }
}
