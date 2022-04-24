using System;
using UnityEngine;
using UnityEngine.UI;

using Entrance.Common;
using Entrance.Utils;

using TMPro;

namespace Entrance.View
{
    public class LivePreparationView : MonoBehaviour
    {
        // **************************************************
        // インスペクタオブジェクト
        // **************************************************
        // メイン楽曲アイコン
        [SerializeField]
        private Image mainMusicIcon = default;
        [SerializeField]
        private Text mainMusicName = default;
        // 設定表示
        [SerializeField]
        private TMP_Text notesSpeedText = default;
        [SerializeField]
        private TMP_Text severeLevelText = default;
        [SerializeField]
        private TMP_Text timingText = default;
        // 準備選択アイコン
        [SerializeField]
        private GameObject liveStart = default;
        [SerializeField]
        private GameObject notesSpeed = default;
        [SerializeField]
        private GameObject severeLevel = default;
        [SerializeField]
        private GameObject timing = default;
        [SerializeField]
        private GameObject otherSetting = default;

        // **************************************************
        // イベントリスナー定義
        // **************************************************
        // 準備選択イベント
        public Action selectPreparationListener1;
        public Action selectPreparationListener2;
        public Action selectPreparationListener3;
        public Action selectPreparationListener4;
        public Action selectPreparationListener5;
        // 準備決定イベント
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
            // メインアイコン
            // ********************************************************************************
            // アイコン変更
            mainMusicIcon.sprite = sessionCommon.MusicIcon;
            // 楽曲名変更
            mainMusicName.text = sessionCommon.MusicName;

            // ********************************************************************************
            // 設定表示
            // ********************************************************************************
            DisplaySetting(sessionCommon);
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
                SystemUtils.SafeCall(selectPreparationListener5);
            }
            if (KeyboardPressUtils.All4())
            {
                SystemUtils.SafeCall(selectPreparationListener3);
            }
            if (KeyboardPressUtils.All5())
            {
                SystemUtils.SafeCall(selectPreparationListener1);
            }
            if (KeyboardPressUtils.All6())
            {
                SystemUtils.SafeCall(selectPreparationListener2);
            }
            if (KeyboardPressUtils.All7())
            {
                SystemUtils.SafeCall(selectPreparationListener4);
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
        // 準備を選択
        // ********************************************************************************
        public void SelectPreparation(int preparationNumber)
        {
            switch (preparationNumber)
            {
                case 1:
                    liveStart.transform.localScale = selectScale;
                    notesSpeed.transform.localScale = nonSelectScale;
                    severeLevel.transform.localScale = nonSelectScale;
                    timing.transform.localScale = nonSelectScale;
                    otherSetting.transform.localScale = nonSelectScale;
                    break;
                case 2:
                    liveStart.transform.localScale = nonSelectScale;
                    notesSpeed.transform.localScale = selectScale;
                    severeLevel.transform.localScale = nonSelectScale;
                    timing.transform.localScale = nonSelectScale;
                    otherSetting.transform.localScale = nonSelectScale;
                    break;
                case 3:
                    liveStart.transform.localScale = nonSelectScale;
                    notesSpeed.transform.localScale = nonSelectScale;
                    severeLevel.transform.localScale = selectScale;
                    timing.transform.localScale = nonSelectScale;
                    otherSetting.transform.localScale = nonSelectScale;
                    break;
                case 4:
                    liveStart.transform.localScale = nonSelectScale;
                    notesSpeed.transform.localScale = nonSelectScale;
                    severeLevel.transform.localScale = nonSelectScale;
                    timing.transform.localScale = selectScale;
                    otherSetting.transform.localScale = nonSelectScale;
                    break;
            }
        }

        // ********************************************************************************
        // 設定を表示
        // ********************************************************************************
        public void DisplaySetting(SessionCommon sessionCommon)
        {
            string speed = sessionCommon.NotesSpeed.ToString();
            int length = speed.Length - 1;
            notesSpeedText.SetText(speed.Substring(0, length) + "." + speed.Substring(length));
            severeLevelText.SetText(sessionCommon.SevereLevel.ToString());
            timingText.SetText(sessionCommon.Timing.ToString());
        }
    }
}
