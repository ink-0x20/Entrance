using UnityEngine;

using Entrance.Common;
using Entrance.Prezenter;

namespace Entrance.Model
{
    public struct WindowManage
    {
        // **************************************************
        // ウィンドウ管理用保持データ
        // **************************************************
        private WindowList windowName;
        private GameObject windowObject;
        private BaseWindowPrezenter baseWindowPrezenter;

        // **************************************************
        // コンストラクタ
        // **************************************************
        public WindowManage(WindowList windowName, GameObject windowObject, BaseWindowPrezenter baseWindowPrezenter)
        {
            this.windowName = windowName;
            this.windowObject = windowObject;
            this.baseWindowPrezenter = baseWindowPrezenter;
        }

        public WindowList WindowName { get => windowName; }
        public GameObject WindowObject { get => windowObject; }
        public BaseWindowPrezenter BaseWindowPrezenter { get => baseWindowPrezenter; }
    }
}
