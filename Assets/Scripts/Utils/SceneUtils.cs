using System.Collections.Generic;

using Entrance.Common;
using Entrance.Model;

namespace Entrance.Utils
{
    public static class SceneUtils
    {
        // ********************************************************************************
        // �J�ڐ�X�N���[������A�Ǘ��E�B���h�E�����擾����
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
        // �J�ڐ�X�N���[������A�Ǘ��E�B���h�E��j�����邩���肷��
        // ********************************************************************************
        public static bool IsWindowDestroy(WindowList nextWindow)
        {
            return SystemConfig.SCENE_MANAGE[nextWindow].DestroyFlg;
        }
    }
}
