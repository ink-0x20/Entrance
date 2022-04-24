using System;
using UnityEngine;

using Entrance.Common;
using Entrance.Utils;

using TMPro;

namespace Entrance.View
{
    public class SettingNotesSpeedView : MonoBehaviour
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
        [SerializeField]
        private GameObject setting4 = default;
        [SerializeField]
        private GameObject setting5 = default;

        // **************************************************
        // イベントリスナー定義
        // **************************************************
        // 設定選択イベント
        public Action selectSettingListener1;
        public Action selectSettingListener2;
        public Action selectSettingListener3;
        public Action selectSettingListener4;
        public Action selectSettingListener5;
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
            SetNotesSpeed(sessionCommon.NotesSpeed);
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
            if (KeyboardPressUtils.All3())
            {
                SystemUtils.SafeCall(selectSettingListener5);
            }
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
            if (KeyboardPressUtils.All7())
            {
                SystemUtils.SafeCall(selectSettingListener4);
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
                    setting4.transform.localScale = nonSelectScale;
                    setting5.transform.localScale = nonSelectScale;
                    break;
                case 2:
                    setting1.transform.localScale = nonSelectScale;
                    setting2.transform.localScale = selectScale;
                    setting3.transform.localScale = nonSelectScale;
                    setting4.transform.localScale = nonSelectScale;
                    setting5.transform.localScale = nonSelectScale;
                    break;
                case 3:
                    setting1.transform.localScale = nonSelectScale;
                    setting2.transform.localScale = nonSelectScale;
                    setting3.transform.localScale = selectScale;
                    setting4.transform.localScale = nonSelectScale;
                    setting5.transform.localScale = nonSelectScale;
                    break;
                case 4:
                    setting1.transform.localScale = nonSelectScale;
                    setting2.transform.localScale = nonSelectScale;
                    setting3.transform.localScale = nonSelectScale;
                    setting4.transform.localScale = selectScale;
                    setting5.transform.localScale = nonSelectScale;
                    break;
                case 5:
                    setting1.transform.localScale = nonSelectScale;
                    setting2.transform.localScale = nonSelectScale;
                    setting3.transform.localScale = nonSelectScale;
                    setting4.transform.localScale = nonSelectScale;
                    setting5.transform.localScale = selectScale;
                    break;
            }
        }

        // ********************************************************************************
        // ノーツスピードを表示
        // ********************************************************************************
        public void SetNotesSpeed(int notesSpeed)
        {
            string speed = notesSpeed.ToString();
            int length = speed.Length - 1;
            settingData.SetText(speed.Substring(0, length) + "." + speed.Substring(length));

        }
    }
}
