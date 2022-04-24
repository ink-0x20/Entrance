using System;

namespace Entrance.Utils
{
    public static class SystemUtils
    {
        // ********************************************************************************
        // null‚Å‚È‚¢ê‡‚Ì‚İˆ—‚ğÀs‚·‚é
        // ********************************************************************************
        public static void SafeCall(Action action)
        {
            if (action != null)
            {
                action();
            }
        }
    }
}
