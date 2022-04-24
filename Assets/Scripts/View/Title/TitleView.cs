using System;
using UnityEngine;

using Entrance.Utils;

namespace Entrance.View
{
    public class TitleView : MonoBehaviour
    {
        // **************************************************
        // インスペクタオブジェクト
        // **************************************************
        // 開始テキスト
        [SerializeField]
        private GameObject startText = default;

        // **************************************************
        // イベントリスナー定義
        // **************************************************
        // 遷移イベント
        public Action sceneListener;

        void Update()
        {
            // ********************************************************************************
            // アプリ終了判定
            // ********************************************************************************
            KeyboardPressUtils.ExitApplication();

            // ********************************************************************************
            // 入力監視
            // ********************************************************************************
            if (KeyboardPressUtils.Enter())
            {
                SystemUtils.SafeCall(sceneListener);
                startText.SetActive(false);
            }
        }
    }
}
