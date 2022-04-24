using System;
using UnityEngine;

using Entrance.Common;
using Entrance.Utils;

using TMPro;

namespace Entrance.View
{
    public class SettingSevereLevelView : MonoBehaviour
    {
        // **************************************************
        // インスペクタオブジェクト
        // **************************************************
        // 設定表示
        [SerializeField]
        private TMP_Text settingData = default;
        // 設定選択アイコン
        [SerializeField]
        private GameObject setting1 = default;
        [SerializeField]
        private GameObject setting2 = default;
        [SerializeField]
        private GameObject setting3 = default;

        // **************************************************
        // イベントリスナー定義
        // **************************************************
        // 設定選択イベント
        public Action selectSettingListener1;
        public Action selectSettingListener2;
        public Action selectSettingListener3;
        // 設定決定イベント
        public Action decideListener;
        public Action cancelListener;

        // **************************************************
        // 選択・非選択スケール
        // **************************************************
        private Vector3 selectScale = new Vector3(2.3f, 2.3f, 1.0f);
        private Vector3 nonSelectScale = new Vector3(1.81f, 1.8f, 1.0f);

        // ********************************************************************************
        // 初期処理
        // ********************************************************************************
        public void Initialize(SessionCommon sessionCommon)
        {
            // ********************************************************************************
            // 現在の設定を適用
            // ********************************************************************************
            SetSevereLevel(sessionCommon.SevereLevel);
        }

        void Update()
        {
            // ********************************************************************************
            // アプリ終了判定
            // ********************************************************************************
            KeyboardPressUtils.ExitApplication();

            // ********************************************************************************
            // 入力監視
            // ********************************************************************************
            if (KeyboardPressUtils.All4())
            {
                SystemUtils.SafeCall(selectSettingListener3);
            }
            if (KeyboardPressUtils.All5())
            {
                SystemUtils.SafeCall(selectSettingListener1);
            }
            if (KeyboardPressUtils.All6())
            {
                SystemUtils.SafeCall(selectSettingListener2);
            }
            if (KeyboardPressUtils.Enter())
            {
                SystemUtils.SafeCall(decideListener);
            }
            if (KeyboardPressUtils.All0())
            {
                SystemUtils.SafeCall(cancelListener);
            }
        }

        // ********************************************************************************
        // 設定を選択
        // ********************************************************************************
        public void SelectSetting(int settingNumber)
        {
            switch (settingNumber)
            {
                case 1:
                    setting1.transform.localScale = selectScale;
                    setting2.transform.localScale = nonSelectScale;
                    setting3.transform.localScale = nonSelectScale;
                    break;
                case 2:
                    setting1.transform.localScale = nonSelectScale;
                    setting2.transform.localScale = selectScale;
                    setting3.transform.localScale = nonSelectScale;
                    break;
                case 3:
                    setting1.transform.localScale = nonSelectScale;
                    setting2.transform.localScale = nonSelectScale;
                    setting3.transform.localScale = selectScale;
                    break;
            }
        }

        // ********************************************************************************
        // シビアレベルを表示
        // ********************************************************************************
        public void SetSevereLevel(int severeLevel)
        {
            settingData.SetText(severeLevel.ToString());
        }
    }
}
