using Entrance.Common;

namespace Entrance.Utils
{
    public static class ResourcesPathUtils
    {
        public static string Music(string musicName)
        {
            return "Music/" + musicName;
        }
        public static string Music(SessionCommon sessionCommon)
        {
            return "Music/" + sessionCommon.MusicName;
        }
        public static string MusicData(SessionCommon sessionCommon)
        {
            return "MusicData/" + sessionCommon.MusicName + "_" + sessionCommon.CategoryType.ToString() + "_" + sessionCommon.DifficultyType.ToString();
        }
        public static string MusicIcon(string musicName)
        {
            return "MusicIcon/" + musicName;
        }
    }
}
