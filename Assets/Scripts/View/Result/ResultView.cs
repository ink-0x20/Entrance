using System;
using UnityEngine;
using UnityEngine.UI;

using Entrance.Common;
using Entrance.Utils;

using TMPro;

namespace Entrance.View
{
    public class ResultView : MonoBehaviour
    {
        // **************************************************
        // インスペクタオブジェクト
        // **************************************************
        // テキストオブジェクト
        [SerializeField]
        private Text musicName = default;
        [SerializeField]
        private TMP_Text score = default;
        [SerializeField]
        private TMP_Text highScore = default;
        [SerializeField]
        private TMP_Text perfect = default;
        [SerializeField]
        private TMP_Text great = default;
        [SerializeField]
        private TMP_Text good = default;
        [SerializeField]
        private TMP_Text bad = default;
        [SerializeField]
        private TMP_Text miss = default;
        [SerializeField]
        private TMP_Text maxCombo = default;
        // メイン楽曲アイコン
        [SerializeField]
        private Image mainMusicIcon = default;
        // スコアランク
        [SerializeField]
        private GameObject scoreRankD = default;
        [SerializeField]
        private GameObject scoreRankC = default;
        [SerializeField]
        private GameObject scoreRankB = default;
        [SerializeField]
        private GameObject scoreRankA = default;
        [SerializeField]
        private GameObject scoreRankS = default;
        [SerializeField]
        private GameObject scoreRankSS = default;
        [SerializeField]
        private GameObject scoreRankSSS = default;

        // **************************************************
        // イベントリスナー定義
        // **************************************************
        // 選択画面遷移
        public Action selectScene;

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

            // ********************************************************************************
            // 結果表示
            // ********************************************************************************
            ScoreDetail scoreDetail = sessionCommon.GetScoreHistory();
            musicName.text = sessionCommon.MusicName;
            score.text = scoreDetail.Score.ToString();
            highScore.text = sessionCommon.GetBestScoreHistory().Score.ToString();
            perfect.text = scoreDetail.Perfect.ToString();
            great.text = scoreDetail.Great.ToString();
            good.text = scoreDetail.Good.ToString();
            bad.text = scoreDetail.Bad.ToString();
            miss.text = scoreDetail.Miss.ToString();
            maxCombo.text = scoreDetail.MaxCombo.ToString();
            switch (scoreDetail.ScoreRank)
            {
                case ScoreRank.D:
                    scoreRankD.SetActive(true);
                    break;
                case ScoreRank.C:
                    scoreRankC.SetActive(true);
                    break;
                case ScoreRank.B:
                    scoreRankB.SetActive(true);
                    break;
                case ScoreRank.A:
                    scoreRankA.SetActive(true);
                    break;
                case ScoreRank.S:
                    scoreRankS.SetActive(true);
                    break;
                case ScoreRank.SS:
                    scoreRankSS.SetActive(true);
                    break;
                case ScoreRank.SSS:
                    scoreRankSSS.SetActive(true);
                    break;
            }
        }

        // ********************************************************************************
        // フレーム毎処理
        // ********************************************************************************
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
                SystemUtils.SafeCall(selectScene);
            }
        }
    }
}
