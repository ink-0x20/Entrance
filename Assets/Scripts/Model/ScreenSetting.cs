using System.Collections.Generic;

using Entrance.Common;

namespace Entrance.Model
{
    public struct ScreenSetting
    {
        // **************************************************
        // ウィンドウごとの情報定義
        // **************************************************
        // 破棄フラグ（遷移時に破棄するか）
        private bool destroyFlg;
        // 自身のウィンドウが管理するスクリーン
        private List<ScreenList> screenList;

        // **************************************************
        // コンストラクタ
        // **************************************************
        public ScreenSetting(bool destroyFlg, List<ScreenList> screenList)
        {
            this.destroyFlg = destroyFlg;
            this.screenList = screenList;
        }

        public bool DestroyFlg { get => destroyFlg; }

        // ********************************************************************************
        // 自身のウィンドウが管理しているスクリーンかを判定する
        // ********************************************************************************
        public bool IsExistScreen(ScreenList nextScreen)
        {
            return screenList.Contains(nextScreen);
        }
    }
}
