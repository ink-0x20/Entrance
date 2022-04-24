using System.Collections.Generic;
using UnityEngine;

using Entrance.Common;

namespace Entrance.Model
{
    public struct SelectMusicInfo
    {
        private int musicNumber;
        private string musicName;
        private Sprite musicIcon;
        private Dictionary<string, int> musicLevelList;
        private Dictionary<DifficultyType, bool> isFullComboList;
        private Dictionary<DifficultyType, ScoreRank> musicScoreRankList;

        public SelectMusicInfo(
            int musicNumber,
            string musicName,
            Sprite musicIcon,
            Dictionary<string, int> musicLevelList,
            Dictionary<DifficultyType, bool> isFullComboList,
            Dictionary<DifficultyType, ScoreRank> musicScoreRankList)
        {
            this.musicNumber = musicNumber;
            this.musicName = musicName;
            this.musicIcon = musicIcon;
            this.musicLevelList = musicLevelList;
            this.isFullComboList = isFullComboList;
            this.musicScoreRankList = musicScoreRankList;
        }

        public int MusicNumber { get => musicNumber; }
        public string MusicName { get => musicName; }
        public Sprite MusicIcon { get => musicIcon; }
        public Dictionary<string, int> MusicLevelList { get => musicLevelList; }
        public Dictionary<DifficultyType, bool> IsFullComboList { get => isFullComboList; }
        public Dictionary<DifficultyType, ScoreRank> MusicScoreRankList { get => musicScoreRankList; }
    }
}
