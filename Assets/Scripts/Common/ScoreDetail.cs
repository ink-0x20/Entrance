using System;
using UnityEngine;

namespace Entrance.Common
{
    [Serializable]
    public struct ScoreDetail
    {
        [SerializeField]
        private float score;
        [SerializeField]
        private ScoreRank scoreRank;
        [SerializeField]
        private int perfect;
        [SerializeField]
        private int great;
        [SerializeField]
        private int good;
        [SerializeField]
        private int bad;
        [SerializeField]
        private int miss;
        [SerializeField]
        private int maxCombo;
        [SerializeField]
        private bool isFullCombo;
        [SerializeField]
        private bool isAllPerfect;

        public ScoreDetail(
            float score,
            ScoreRank scoreRank,
            int perfect,
            int great,
            int good,
            int bad,
            int miss,
            int maxCombo,
            bool isFullCombo,
            bool isAllPerfect)
        {
            this.score = score;
            this.scoreRank = scoreRank;
            this.perfect = perfect;
            this.great = great;
            this.good = good;
            this.bad = bad;
            this.miss = miss;
            this.maxCombo = maxCombo;
            this.isFullCombo = isFullCombo;
            this.isAllPerfect = isAllPerfect;
        }

        public float Score { get => score; }
        public ScoreRank ScoreRank { get => scoreRank; }
        public int Perfect { get => perfect; }
        public int Great { get => great; }
        public int Good { get => good; }
        public int Bad { get => bad; }
        public int Miss { get => miss; }
        public int MaxCombo { get => maxCombo; }
        public bool IsFullCombo { get => isFullCombo; }
        public bool IsAllPerfect { get => isAllPerfect; }
    }
}
