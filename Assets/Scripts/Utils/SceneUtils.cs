using System.Collections.Generic;

using Entrance.Common;
using Entrance.Model;

namespace Entrance.Utils
{
    public static class SceneUtils
    {
        // ********************************************************************************
        // 遷移先スクリーンから、管理ウィンドウ情報を取得する
        // ********************************************************************************
        public static WindowList GetWindowName(ScreenList nextScreen)
        {
            foreach (KeyValuePair<WindowList, ScreenSetting> scene in SystemConfig.SCENE_MANAGE)
            {
                if (scene.Value.IsExistScreen(nextScreen))
                {
                    return scene.Key;
                }
            }
            return WindowList.TitleWindow;
        }

        // ********************************************************************************
        // 遷移先スクリーンから、管理ウィンドウを破棄するか判定する
        // ********************************************************************************
        public static bool IsWindowDestroy(WindowList nextWindow)
        {
            return SystemConfig.SCENE_MANAGE[nextWindow].DestroyFlg;
        }
    }
}
