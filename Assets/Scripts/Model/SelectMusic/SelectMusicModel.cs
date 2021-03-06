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
        // [UniRxÄCxg]IðyÈîñ
        // **************************************************
        private readonly ReactiveProperty<SelectMusicInfo> musicInfo = new ReactiveProperty<SelectMusicInfo>();
        public IReadOnlyReactiveProperty<SelectMusicInfo> MusicInfo => musicInfo;

        // **************************************************
        // [UniRxÄCxg]yÈÇÔ
        // **************************************************
        private readonly ReactiveProperty<int> musicNumber = new ReactiveProperty<int>(0);
        public IReadOnlyReactiveProperty<int> MusicNumber => musicNumber;
        public void SelectMusic(int musicNumber)
        {
            if (musicNumber <= musicNameListLength && this.musicNumber.Value != musicNumber)
            {
                // yÈÇÔðÝè
                this.musicNumber.Value = musicNumber;
                // yÈÇÔ©çyÈ¼ðÁè
                musicName = musicNameList[musicNumber - 1];
                // BGMðØèÖ¦
                AudioUtils.PlayLiveMusic(musicClipList[musicName], liveVolume);
                // XRAðæ¾
                ScoreDetail scoreDetailEasy = GetBestScoreHistory(musicName, CategoryType.ENTRANCE, DifficultyType.EASY);
                ScoreDetail scoreDetailNormal = GetBestScoreHistory(musicName, CategoryType.ENTRANCE, DifficultyType.NORMAL);
                ScoreDetail scoreDetailHard = GetBestScoreHistory(musicName, CategoryType.ENTRANCE, DifficultyType.HARD);
                ScoreDetail scoreDetailExtreme = GetBestScoreHistory(musicName, CategoryType.ENTRANCE, DifficultyType.EXTREME);
                ScoreDetail scoreDetailChallenge = GetBestScoreHistory(musicName, CategoryType.ENTRANCE, DifficultyType.CHALLENGE);
                // XRAN
                Dictionary<DifficultyType, ScoreRank> musicScoreRankList = new Dictionary<DifficultyType, ScoreRank>();
                musicScoreRankList.Add(DifficultyType.EASY, scoreDetailEasy.ScoreRank);
                musicScoreRankList.Add(DifficultyType.NORMAL, scoreDetailNormal.ScoreRank);
                musicScoreRankList.Add(DifficultyType.HARD, scoreDetailHard.ScoreRank);
                musicScoreRankList.Add(DifficultyType.EXTREME, scoreDetailExtreme.ScoreRank);
                musicScoreRankList.Add(DifficultyType.CHALLENGE, scoreDetailChallenge.ScoreRank);
                // tR{
                Dictionary<DifficultyType, bool> musicFullComboList = new Dictionary<DifficultyType, bool>();
                musicFullComboList.Add(DifficultyType.EASY, scoreDetailEasy.IsFullCombo);
                musicFullComboList.Add(DifficultyType.NORMAL, scoreDetailNormal.IsFullCombo);
                musicFullComboList.Add(DifficultyType.HARD, scoreDetailHard.IsFullCombo);
                musicFullComboList.Add(DifficultyType.EXTREME, scoreDetailExtreme.IsFullCombo);
                musicFullComboList.Add(DifficultyType.CHALLENGE, scoreDetailChallenge.IsFullCombo);
                // IðyÈîñðXV
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
        // [UniRxÄCxg]yÈXg
        // **************************************************
        private readonly ReactiveProperty<MusicListModel> musicList = new ReactiveProperty<MusicListModel>();
        public IReadOnlyReactiveProperty<MusicListModel> MusicList => musicList;

        // **************************************************
        // y[WÔ
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
        // yÈîñÛ
        // **************************************************
        // yÈ¼
        private string musicName = "";
        public string MusicName { get => musicName; }

        // CuyÈ¹Ê
        private float liveVolume = 0.0f;
        // yÈ
        private int musicNameListLength = 0;
        // yÈê
        private List<string> musicNameList = new List<string>();
        // yÈACR
        private Dictionary<string, Sprite> musicIconList = new Dictionary<string, Sprite>();
        // yÈNbvê
        private Dictionary<string, AudioClip> musicClipList = new Dictionary<string, AudioClip>();
        // yÈïÕxê
        private Dictionary<string, int> musicLevelList = new Dictionary<string, int>();
        // XRAð
        private DictionaryJson<string, ScoreDetail> scoreHistory = new DictionaryJson<string, ScoreDetail>();

        // **************************************************
        // Êîñ
        // **************************************************

        // ********************************************************************************
        // ú
        // ********************************************************************************
        public void Initialize(SessionCommon sessionCommon)
        {
            // ********************************************************************************
            // ÊÇ
            // ********************************************************************************
            TextAsset[] musicList = Resources.LoadAll<TextAsset>("MusicData");
            int length = musicList.Length;
            // yÈf[^iNbvjð·×ÄÇÝÝ
            for (int i = 0; i < length; i++)
            {
                // LbV
                TextAsset music = musicList[i];
                // t@C¼©çyÈîñðæ¾iÈ¼j
                string musicNameDifficult = music.name.Substring(0,
                    music.name.LastIndexOf("_"));
                string musicName = musicNameDifficult.Substring(0,
                    musicNameDifficult.LastIndexOf("_"));
                // yÈACRÇÝÝ
                if (!musicIconList.ContainsKey(musicName))
                {
                    musicIconList.Add(musicName, Resources.Load<Sprite>(ResourcesPathUtils.MusicIcon(musicName)));
                }
                // yÈf[^iNbvjÇÝÝ
                if (!musicClipList.ContainsKey(musicName))
                {
                    musicClipList.Add(musicName, Resources.Load<AudioClip>(ResourcesPathUtils.Music(musicName)));
                }
                // yÈXgÇÁ
                if (!musicNameList.Contains(musicName))
                {
                    musicNameList.Add(musicName);
                }
                // ïÕx
                MusicDTO.EditData musicData = JsonUtility.FromJson<MusicDTO.EditData>(music.ToString());
                musicLevelList.Add(music.name, musicData.level);
            }
            // \¦pyÈ
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
            // yÈðÛ¶
            musicNameListLength = musicNameList.Count;
            // ÅÌÈðÛ
            musicName = musicNameList[0];
            // y[WÅåðÛ
            maxPage = 0;
            // XRAððÛ
            scoreHistory = sessionCommon.ScoreHistory;
            // CuyÈ¹Ê
            liveVolume = sessionCommon.LiveVolumeBGM;
        }

        public void SetSelectedMusic(SessionCommon sessionCommon)
        {
            // ********************************************************************************
            // ßIðyÈðZbg
            // ********************************************************************************
            // yÈÇÔðÝè
            this.musicNumber.Value = sessionCommon.MusicNumber;
            // yÈÇÔ©çyÈ¼ðÁè
            musicName = musicNameList[sessionCommon.MusicNumber - 1];
            // BGMðØèÖ¦
            AudioUtils.PlayLiveMusic(musicClipList[musicName], liveVolume);
            // XRAðæ¾
            ScoreDetail scoreDetailEasy = GetBestScoreHistory(musicName, CategoryType.ENTRANCE, DifficultyType.EASY);
            ScoreDetail scoreDetailNormal = GetBestScoreHistory(musicName, CategoryType.ENTRANCE, DifficultyType.NORMAL);
            ScoreDetail scoreDetailHard = GetBestScoreHistory(musicName, CategoryType.ENTRANCE, DifficultyType.HARD);
            ScoreDetail scoreDetailExtreme = GetBestScoreHistory(musicName, CategoryType.ENTRANCE, DifficultyType.EXTREME);
            ScoreDetail scoreDetailChallenge = GetBestScoreHistory(musicName, CategoryType.ENTRANCE, DifficultyType.CHALLENGE);
            // XRAN
            Dictionary<DifficultyType, ScoreRank> musicScoreRankList = new Dictionary<DifficultyType, ScoreRank>();
            musicScoreRankList.Add(DifficultyType.EASY, scoreDetailEasy.ScoreRank);
            musicScoreRankList.Add(DifficultyType.NORMAL, scoreDetailNormal.ScoreRank);
            musicScoreRankList.Add(DifficultyType.HARD, scoreDetailHard.ScoreRank);
            musicScoreRankList.Add(DifficultyType.EXTREME, scoreDetailExtreme.ScoreRank);
            musicScoreRankList.Add(DifficultyType.CHALLENGE, scoreDetailChallenge.ScoreRank);
            // tR{
            Dictionary<DifficultyType, bool> musicFullComboList = new Dictionary<DifficultyType, bool>();
            musicFullComboList.Add(DifficultyType.EASY, scoreDetailEasy.IsFullCombo);
            musicFullComboList.Add(DifficultyType.NORMAL, scoreDetailNormal.IsFullCombo);
            musicFullComboList.Add(DifficultyType.HARD, scoreDetailHard.IsFullCombo);
            musicFullComboList.Add(DifficultyType.EXTREME, scoreDetailExtreme.IsFullCombo);
            musicFullComboList.Add(DifficultyType.CHALLENGE, scoreDetailChallenge.IsFullCombo);
            // IðyÈîñðXV
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
            // ïÕxê
            sessionCommon.MusicLevelList = musicLevelList;
            // ACR
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
