using Entrance.Common;
using Entrance.DTO;

namespace Entrance.Utils
{
    public static class ScoreRankUtils
    {
        public static ScoreRank GetScoreRank(float score, float maxScore)
        {
            float percentage = score / maxScore;
            if (percentage < 0.38f)
            {
                return ScoreRank.D;
            }
            else if (percentage < 0.55f)
            {
                return ScoreRank.C;
            }
            else if (percentage < 0.70f)
            {
                return ScoreRank.B;
            }
            else if (percentage < 0.78f)
            {
                return ScoreRank.A;
            }
            else if (percentage < 0.90f)
            {
                return ScoreRank.S;
            }
            else if (percentage < 0.98f)
            {
                return ScoreRank.SS;
            }
            else
            {
                return ScoreRank.SSS;
            }
        }
    }
}
