using System;

namespace Entrance.Utils
{
    public static class SystemUtils
    {
        // ********************************************************************************
        // null�łȂ��ꍇ�̂ݏ��������s����
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
