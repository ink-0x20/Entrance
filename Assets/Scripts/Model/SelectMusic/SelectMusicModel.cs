using UnityEngine;
using System.Collections.Generic;

using Entrance.Common;
using Entrance.DTO;
using Entrance.Utils;

using UniRx;

namespace Entrance.Model
{
    public class SelectMusicModel
    {
        // **************************************************
        // [UniRx監視イベント]選択楽曲情報
        // **************************************************
        private readonly ReactiveProperty<SelectMusicInfo> musicInfo = new ReactiveProperty<SelectMusicInfo>();
        public IReadOnlyReactiveProperty<SelectMusicInfo> MusicInfo => musicInfo;

        // **************************************************
        // [UniRx監視イベント]楽曲管理番号
        // **************************************************
        private readonly ReactiveProperty<int> musicNumber = new ReactiveProperty<int>(0);
        public IReadOnlyReactiveProperty<int> MusicNumber => musicNumber;
        public void SelectMusic(int musicNumber)
        {
            if (musicNumber <= musicNameListLength && this.musicNumber.Value != musicNumber)
            {
                // 楽曲管理番号を設定
                this.musicNumber.Value = musicNumber;
                // 楽曲管理番号から楽曲名を特定
                musicName = musicNameList[musicNumber - 1];
                // BGMを切り替え
                AudioUtils.PlayLiveMusic(musicClipList[musicName], liveVolume);
                // スコア履歴取得
                ScoreDetail scoreDetailEasy = GetBestScoreHistory(musicName, CategoryType.ENTRANCE, DifficultyType.EASY);
                ScoreDetail scoreDetailNormal = GetBestScoreHistory(musicName, CategoryType.ENTRANCE, DifficultyType.NORMAL);
                ScoreDetail scoreDetailHard = GetBestScoreHistory(musicName, CategoryType.ENTRANCE, DifficultyType.HARD);
                ScoreDetail scoreDetailExtreme = GetBestScoreHistory(musicName, CategoryType.ENTRANCE, DifficultyType.EXTREME);
                ScoreDetail scoreDetailChallenge = GetBestScoreHistory(musicName, CategoryType.ENTRANCE, DifficultyType.CHALLENGE);
                // スコアランク
                Dictionary<DifficultyType, ScoreRank> musicScoreRankList = new Dictionary<DifficultyType, ScoreRank>();
                musicScoreRankList.Add(DifficultyType.EASY, scoreDetailEasy.ScoreRank);
                musicScoreRankList.Add(DifficultyType.NORMAL, scoreDetailNormal.ScoreRank);
                musicScoreRankList.Add(DifficultyType.HARD, scoreDetailHard.ScoreRank);
                musicScoreRankList.Add(DifficultyType.EXTREME, scoreDetailExtreme.ScoreRank);
                musicScoreRankList.Add(DifficultyType.CHALLENGE, scoreDetailChallenge.ScoreRank);
                // フルコンボ
                Dictionary<DifficultyType, bool> musicFullComboList = new Dictionary<DifficultyType, bool>();
                musicFullComboList.Add(DifficultyType.EASY, scoreDetailEasy.IsFullCombo);
                musicFullComboList.Add(DifficultyType.NORMAL, scoreDetailNormal.IsFullCombo);
                musicFullComboList.Add(DifficultyType.HARD, scoreDetailHard.IsFullCombo);
                musicFullComboList.Add(DifficultyType.EXTREME, scoreDetailExtreme.IsFullCombo);
                musicFullComboList.Add(DifficultyType.CHALLENGE, scoreDetailChallenge.IsFullCombo);
                // 選択楽曲情報を更新
                this.musicInfo.Value = new SelectMusicInfo(
                    musicNumber,
                    musicName,
                    musicIconList[musicName],
                    musicLevelList,
                    musicFullComboList,
                    musicScoreRankList);
            }
        }

        // **************************************************
        // [UniRx監視イベント]楽曲リスト
        // **************************************************
        private readonly ReactiveProperty<MusicListModel> musicList = new ReactiveProperty<MusicListModel>();
        public IReadOnlyReactiveProperty<MusicListModel> MusicList => musicList;

        // **************************************************
        // ページ番号
        // **************************************************
        private int maxPage = 0;
        private int page = 0;
        public void LeftPage()
        {
            if (0 < page)
            {
                page--;
            }
        }
        public void RightPage()
        {
            if (page < maxPage)
            {
                page++;
            }
        }

        // **************************************************
        // 楽曲情報保持
        // **************************************************
        // 楽曲名
        private string musicName = "";
        public string MusicName { get => musicName; }

        // ライブ楽曲音量
        private float liveVolume = 0.0f;
        // 楽曲数
        private int musicNameListLength = 0;
        // 楽曲一覧
        private List<string> musicNameList = new List<string>();
        // 楽曲アイコン
        private Dictionary<string, Sprite> musicIconList = new Dictionary<string, Sprite>();
        // 楽曲クリップ一覧
        private Dictionary<string, AudioClip> musicClipList = new Dictionary<string, AudioClip>();
        // 楽曲難易度一覧
        private Dictionary<string, int> musicLevelList = new Dictionary<string, int>();
        // スコア履歴
        private DictionaryJson<string, ScoreDetail> scoreHistory = new DictionaryJson<string, ScoreDetail>();

        // **************************************************
        // 譜面情報
        // **************************************************

        // ********************************************************************************
        // 初期処理
        // ********************************************************************************
        public void Initialize(SessionCommon sessionCommon)
        {
            // ********************************************************************************
            // 譜面管理
            // ********************************************************************************
            TextAsset[] musicList = Resources.LoadAll<TextAsset>("MusicData");
            int length = musicList.Length;
            // 楽曲データ（クリップ）をすべて読み込み
            for (int i = 0; i < length; i++)
            {
                // キャッシュ
                TextAsset music = musicList[i];
                // ファイル名から楽曲情報を取得（曲名）
                string musicNameDifficult = music.name.Substring(0,
                    music.name.LastIndexOf("_"));
                string musicName = musicNameDifficult.Substring(0,
                    musicNameDifficult.LastIndexOf("_"));
                // 楽曲アイコン読み込み
                if (!musicIconList.ContainsKey(musicName))
                {
                    musicIconList.Add(musicName, Resources.Load<Sprite>(ResourcesPathUtils.MusicIcon(musicName)));
                }
                // 楽曲データ（クリップ）読み込み
                if (!musicClipList.ContainsKey(musicName))
                {
                    musicClipList.Add(musicName, Resources.Load<AudioClip>(ResourcesPathUtils.Music(musicName)));
                }
                // 楽曲リスト追加
                if (!musicNameList.Contains(musicName))
                {
                    musicNameList.Add(musicName);
                }
                // 難易度
                MusicDTO.EditData musicData = JsonUtility.FromJson<MusicDTO.EditData>(music.ToString());
                musicLevelList.Add(music.name, musicData.level);
            }
            // 表示用楽曲
            if (musicNameList.Count == 1)
            {
                this.musicList.Value = new MusicListModel(
                    musicIconList[musicNameList[0]],
                    null,
                    null,
                    null,
                    null
                    );
            }
            else if (musicClipList.Count == 2)
            {
                this.musicList.Value = new MusicListModel(
                    musicIconList[musicNameList[0]],
                    musicIconList[musicNameList[1]],
                    null,
                    null,
                    null
                    );
            }
            else if (musicClipList.Count == 3)
            {
                this.musicList.Value = new MusicListModel(
                    musicIconList[musicNameList[0]],
                    musicIconList[musicNameList[1]],
                    musicIconList[musicNameList[2]],
                    null,
                    null
                    );
            }
            else if (musicClipList.Count == 4)
            {
                this.musicList.Value = new MusicListModel(
                    musicIconList[musicNameList[0]],
                    musicIconList[musicNameList[1]],
                    musicIconList[musicNameList[2]],
                    musicIconList[musicNameList[3]],
                    null
                    );
            }
            else
            {
                this.musicList.Value = new MusicListModel(
                    musicIconList[musicNameList[0]],
                    musicIconList[musicNameList[1]],
                    musicIconList[musicNameList[2]],
                    musicIconList[musicNameList[3]],
                    musicIconList[musicNameList[4]]
                    );
            }
            // 楽曲数を保存
            musicNameListLength = musicNameList.Count;
            // 最初の曲を保持
            musicName = musicNameList[0];
            // ページ最大数を保持
            maxPage = 0;
            // スコア履歴を保持
            scoreHistory = sessionCommon.ScoreHistory;
            // ライブ楽曲音量
            liveVolume = sessionCommon.LiveVolumeBGM;
        }

        public void SetSelectedMusic(SessionCommon sessionCommon)
        {
            // ********************************************************************************
            // 過去選択楽曲をセット
            // ********************************************************************************
            // 楽曲管理番号を設定
            this.musicNumber.Value = sessionCommon.MusicNumber;
            // 楽曲管理番号から楽曲名を特定
            musicName = musicNameList[sessionCommon.MusicNumber - 1];
            // BGMを切り替え
            AudioUtils.PlayLiveMusic(musicClipList[musicName], liveVolume);
            // スコア履歴取得
            ScoreDetail scoreDetailEasy = GetBestScoreHistory(musicName, CategoryType.ENTRANCE, DifficultyType.EASY);
            ScoreDetail scoreDetailNormal = GetBestScoreHistory(musicName, CategoryType.ENTRANCE, DifficultyType.NORMAL);
            ScoreDetail scoreDetailHard = GetBestScoreHistory(musicName, CategoryType.ENTRANCE, DifficultyType.HARD);
            ScoreDetail scoreDetailExtreme = GetBestScoreHistory(musicName, CategoryType.ENTRANCE, DifficultyType.EXTREME);
            ScoreDetail scoreDetailChallenge = GetBestScoreHistory(musicName, CategoryType.ENTRANCE, DifficultyType.CHALLENGE);
            // スコアランク
            Dictionary<DifficultyType, ScoreRank> musicScoreRankList = new Dictionary<DifficultyType, ScoreRank>();
            musicScoreRankList.Add(DifficultyType.EASY, scoreDetailEasy.ScoreRank);
            musicScoreRankList.Add(DifficultyType.NORMAL, scoreDetailNormal.ScoreRank);
            musicScoreRankList.Add(DifficultyType.HARD, scoreDetailHard.ScoreRank);
            musicScoreRankList.Add(DifficultyType.EXTREME, scoreDetailExtreme.ScoreRank);
            musicScoreRankList.Add(DifficultyType.CHALLENGE, scoreDetailChallenge.ScoreRank);
            // フルコンボ
            Dictionary<DifficultyType, bool> musicFullComboList = new Dictionary<DifficultyType, bool>();
            musicFullComboList.Add(DifficultyType.EASY, scoreDetailEasy.IsFullCombo);
            musicFullComboList.Add(DifficultyType.NORMAL, scoreDetailNormal.IsFullCombo);
            musicFullComboList.Add(DifficultyType.HARD, scoreDetailHard.IsFullCombo);
            musicFullComboList.Add(DifficultyType.EXTREME, scoreDetailExtreme.IsFullCombo);
            musicFullComboList.Add(DifficultyType.CHALLENGE, scoreDetailChallenge.IsFullCombo);
            // 選択楽曲情報を更新
            this.musicInfo.Value = new SelectMusicInfo(
                sessionCommon.MusicNumber,
                musicName,
                musicIconList[musicName],
                musicLevelList,
                musicFullComboList,
                musicScoreRankList);
        }

        public void LinkageSession(SessionCommon sessionCommon)
        {
            // 難易度一覧
            sessionCommon.MusicLevelList = musicLevelList;
            // アイコン
            sessionCommon.MusicIcon = musicIconList[musicName];
        }

        private ScoreDetail GetBestScoreHistory(string musicName, CategoryType categoryType, DifficultyType difficultyType)
        {
            string key = musicName + "_" + categoryType.ToString() + "_" + difficultyType.ToString() + "_Best";
            if (scoreHistory.ContainsKey(key))
            {
                return scoreHistory[key];
            }
            return new ScoreDetail(0, ScoreRank.NON, 0, 0, 0, 0, 0, 0, false, false);
        }
    }
}
