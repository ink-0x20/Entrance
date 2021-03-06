using System;

namespace Entrance.Utils
{
    public static class SystemUtils
    {
        // ********************************************************************************
        // nullでない場合のみ処理を実行する
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
