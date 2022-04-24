using Entrance.Common;

namespace Entrance.Model
{
    public struct ScreenDataLinkage
    {
        // **************************************************
        // 次のスクリーン
        // **************************************************
        private ScreenList nextScreen;

        // **************************************************
        // 共通セッション
        // **************************************************
        private SessionCommon sessionCommon;

        // **************************************************
        // アクティブ状態保持
        // **************************************************
        private bool activeFlg;

        // ********************************************************************************
        // 非アクティブ時のコンストラクタ
        // ********************************************************************************
        public ScreenDataLinkage(bool activeFlg)
        {
            this.nextScreen = ScreenList.TitleScreen;
            this.sessionCommon = null;
            this.activeFlg = activeFlg;
        }

        // ********************************************************************************
        // 初回・リセット時の遷移先のみのコンストラクタ
        // ********************************************************************************
        public ScreenDataLinkage(ScreenList nextScreen)
        {
            this.nextScreen = nextScreen;
            this.sessionCommon = null;
            this.activeFlg = true;
        }

        // ********************************************************************************
        // 通常遷移時のコンストラクタ
        // ********************************************************************************
        public ScreenDataLinkage(ScreenList nextScreen, SessionCommon sessionCommon)
        {
            this.nextScreen = nextScreen;
            this.sessionCommon = sessionCommon;
            this.activeFlg = true;
        }

        public ScreenList NextScreen { get => nextScreen; }
        public SessionCommon SessionCommon { get => sessionCommon; }
        public bool ActiveFlg { get => activeFlg; }
    }
}
