using UnityEngine;
using System.Collections.Generic;

namespace Entrance.Common
{
    [System.Serializable]
    public class SessionCommon
    {
        // **************************************************
        // サーバー情報
        // **************************************************
        // アクセスキーの存在によるサーバー通信有無フラグ
        private bool defaultLoadFlg = false;
        public bool DefaultLoadFlg { get => defaultLoadFlg; set => defaultLoadFlg = value; }
        // オブジェクトID
        private string objectId = null;
        public string ObjectId { get => objectId; set => objectId = value; }

        // **************************************************
        // ライブシステム設定
        // **************************************************
        // ノーツ移動時間（ノーツ速度によってシステム側で演算）
        private float notesMovingTime = 0.0f;
        public float NotesMovingTime { get => notesMovingTime; }
        // 各種判定の判定時間（シビアレベルによってシステム側で演算）
        private double perfectJudgeTime = 0.0;
        private double greatJudgeTime = 0.0;
        private double goodJudgeTime = 0.0;
        private double badJudgeTime = 0.0;
        private double missJudgeTime = 0.0;
        public double PerfectJudgeTime { get => perfectJudgeTime; }
        public double GreatJudgeTime { get => greatJudgeTime; }
        public double GoodJudgeTime { get => goodJudgeTime; }
        public double BadJudgeTime { get => badJudgeTime; }
        public double MissJudgeTime { get => missJudgeTime; }
        // タイミング調整時間（タイミングによってシステム側で演算）
        private double timingTime = 0.0;
        public double TimingTime { get => timingTime; }

        // **************************************************
        // ライブユーザ設定
        // **************************************************
        // ノーツ速度（10～120※実際値は1～12）
        [SerializeField]
        private int notesSpeed = 10;
        public int NotesSpeed 
        {
            get => notesSpeed;
            set
            {
                // ノーツ速度
                notesSpeed = value;
                SetNotesMovingTime();
            }
        }
        // シビアレベル（1～20）
        [SerializeField]
        private int severeLevel = 1;
        public int SevereLevel
        {
            get => severeLevel;
            set
            {
                // シビアレベル
                severeLevel = value;
                SetNotesJudgeTime();
            }
        }
        // タイミング（+-20）
        [SerializeField]
        private int timing = 0;
        public int Timing
        {
            get => timing;
            set
            {
                // タイミング
                timing = value;
                SetTimingTime();
            }
        }

        // 背景の明るさ（50%～100%）
        [SerializeField]
        private int backgroundBrightness = 100;
        public int BackgroundBrightness { get => backgroundBrightness; set => backgroundBrightness = value <= 50 ? 50 : 100 <= value ? 100 : value; }
        // レーンの不透明度（0%～100%）
        [SerializeField]
        private int laneOpacity = 100;
        public int LaneOpacity { get => laneOpacity; set => laneOpacity = value <= 0 ? 0 : 100 <= value ? 100 : value; }

        // ライブBGM音量（0～1）
        [SerializeField]
        private float liveVolumeBGM = 1.0f;
        public float LiveVolumeBGM { get => liveVolumeBGM; set => liveVolumeBGM = value <= 0.0f ? 0.0f : 1.0f <= value ? 1.0f : value; }
        // ライブSE音量（0～1）
        [SerializeField]
        private float liveVolumeSE = 1.0f;
        public float LiveVolumeSE { get => liveVolumeSE; set => liveVolumeSE = value <= 0.0f ? 0.0f : 1.0f <= value ? 1.0f : value; }
        // ライブボイス音量（0～1）
        [SerializeField]
        private float liveVolumeVoice = 1.0f;
        public float LiveVolumeVoice { get => liveVolumeVoice; set => liveVolumeVoice = value <= 0.0f ? 0.0f : 1.0f <= value ? 1.0f : value; }
        // システムBGM音量（0～1）
        [SerializeField]
        private float systemVolumeBGM = 1.0f;
        public float SystemVolumeBGM { get => systemVolumeBGM; set => systemVolumeBGM = value <= 0.0f ? 0.0f : 1.0f <= value ? 1.0f : value; }
        // システムSE音量（0～1）
        [SerializeField]
        private float systemVolumeSE = 1.0f;
        public float SystemVolumeSE { get => systemVolumeSE; set => systemVolumeSE = value <= 0.0f ? 0.0f : 1.0f <= value ? 1.0f : value; }
        // システムボイス音量（0～1）
        [SerializeField]
        private float systemVolumeVoice = 1.0f;
        public float SystemVolumeVoice { get => systemVolumeVoice; set => systemVolumeVoice = value <= 0.0f ? 0.0f : 1.0f <= value ? 1.0f : value; }

        // **************************************************
        // ライブオプション
        // **************************************************
        // オートモード
        [SerializeField]
        private bool autoMode = false;
        public bool AutoMode { get => autoMode; set => autoMode = value; }

        // ミラーモード
        [SerializeField]
        private bool mirrorMode = false;
        public bool MirrorMode { get => mirrorMode; set => mirrorMode = value; }

        // カットイン演出
        // スキル・活躍表示
        // 同時押しライン
        // タップ時の振動
        // MVモード　軽量・高品質等
        // 画面固定
        // ライブ前設定確認

        // **************************************************
        // 楽曲データ
        // **************************************************
        // 楽曲管理番号
        [SerializeField]
        private int musicNumber = 1;
        public int MusicNumber { get => musicNumber; set => musicNumber = value; }
        // 楽曲名
        [SerializeField]
        private string musicName = null;
        public string MusicName { get => musicName; set => musicName = value; }
        // カテゴリ
        [SerializeField]
        private CategoryType categoryType = CategoryType.ENTRANCE;
        public CategoryType CategoryType { get => categoryType; set => categoryType = value; }
        // 難易度
        [SerializeField]
        private DifficultyType difficultyType = DifficultyType.EASY;
        public DifficultyType DifficultyType { get => difficultyType; set => difficultyType = value; }
        // 難易度一覧
        private Dictionary<string, int> musicLevelList;
        public Dictionary<string, int> MusicLevelList { get => musicLevelList; set => musicLevelList = value; }
        // メインアイコン
        private Sprite musicIcon;
        public Sprite MusicIcon { get => musicIcon; set => musicIcon = value; }

        // ユニット情報

        // **************************************************
        // ライブ結果
        // **************************************************
        // スコア
        private float score = 0.0f;
        public float Score { get => score; set => score = value; }
        // 各判定
        private int perfect = 0;
        public int Perfect { get => perfect; set => perfect = value; }
        private int great = 0;
        public int Great { get => great; set => great = value; }
        private int good = 0;
        public int Good { get => good; set => good = value; }
        private int bad = 0;
        public int Bad { get => bad; set => bad = value; }
        private int miss = 0;
        public int Miss { get => miss; set => miss = value; }
        // コンボ
        private int combo = 0;
        public int Combo { get => combo; set => combo = value; }
        // 最大コンボ
        private int maxCombo = 0;
        public int MaxCombo { get => maxCombo; set => maxCombo = value; }

        // スコア履歴
        [SerializeField]
        private DictionaryJson<string, ScoreDetail> scoreHistory = new DictionaryJson<string, ScoreDetail>();
        public DictionaryJson<string, ScoreDetail> ScoreHistory { get => scoreHistory; }

        // ********************************************************************************
        // 現在プレイしたスコア履歴を取得する
        // ********************************************************************************
        public ScoreDetail GetScoreHistory()
        {
            return GetScoreHistory(musicName, categoryType, difficultyType);
        }

        // ********************************************************************************
        // 現在の最高スコア履歴を取得する
        // ********************************************************************************
        public ScoreDetail GetBestScoreHistory()
        {
            return GetBestScoreHistory(musicName, categoryType, difficultyType);
        }

        // ********************************************************************************
        // 特定のスコア履歴を取得する
        // ********************************************************************************
        public ScoreDetail GetScoreHistory(string musicName, CategoryType categoryType, DifficultyType difficultyType)
        {
            return scoreHistory[musicName + "_" + categoryType.ToString() + "_" + difficultyType.ToString()];
        }

        // ********************************************************************************
        // 特定の最高スコア履歴を取得する
        // ********************************************************************************
        public ScoreDetail GetBestScoreHistory(string musicName, CategoryType categoryType, DifficultyType difficultyType)
        {
            string key = musicName + "_" + categoryType.ToString() + "_" + difficultyType.ToString() + "_Best";
            if (scoreHistory.ContainsKey(key))
            {
                return scoreHistory[key];
            }
            return new ScoreDetail(0, ScoreRank.NON, 0, 0, 0, 0, 0, 0, false, false);
        }

        // ********************************************************************************
        // スコア履歴を追加する
        // ********************************************************************************
        public void AddScoreHistory(ScoreDetail scoreDetail)
        {
            string key = musicName + "_" + categoryType.ToString() + "_" + difficultyType.ToString();
            if (scoreHistory.ContainsKey(key))
            {
                // 更新（上書き）
                scoreHistory[key] = scoreDetail;
                if (scoreHistory[key + "_Best"].Score < scoreDetail.Score)
                {
                    scoreHistory[key + "_Best"] = scoreDetail;
                }
            }
            else
            {
                // 新規追加
                scoreHistory.Add(key, scoreDetail);
                scoreHistory.Add(key + "_Best", scoreDetail);
            }
        }


        // ********************************************************************************
        // セッション読み込み後の処理を行う
        // ********************************************************************************
        public void OnLoaded()
        {
            SetNotesMovingTime();
            SetNotesJudgeTime();
            SetTimingTime();
        }

        // ********************************************************************************
        // ノーツ移動時間を演算※(ベース値 - ノーツスピード) / 調整値
        // ********************************************************************************
        private void SetNotesMovingTime()
        {
            notesMovingTime = (129 - notesSpeed) / 35.0f;
        }

        // ********************************************************************************
        // ノーツの各判定時間を演算※(基準時間 - ((レベル - 1) * 調整倍率)) * 割合
        // ********************************************************************************
        private void SetNotesJudgeTime()
        {
            double baseTime = 0.8 - ((severeLevel - 1) * 0.039);
            perfectJudgeTime = baseTime * 0.35;
            greatJudgeTime = baseTime * 0.7;
            goodJudgeTime = baseTime * 0.8;
            badJudgeTime = baseTime * 0.9;
            missJudgeTime = baseTime * 1;
        }

        // ********************************************************************************
        // タイミング調整時間を演算
        // ********************************************************************************
        private void SetTimingTime()
        {
            timingTime = timing * 0.015;
        }
    }
}
