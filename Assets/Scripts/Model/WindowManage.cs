using UnityEngine;

using Entrance.Common;
using Entrance.Prezenter;

namespace Entrance.Model
{
    public struct WindowManage
    {
        // **************************************************
        // �E�B���h�E�Ǘ��p�ێ��f�[�^
        // **************************************************
        private WindowList windowName;
        private GameObject windowObject;
        private BaseWindowPrezenter baseWindowPrezenter;

        // **************************************************
        // �R���X�g���N�^
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
