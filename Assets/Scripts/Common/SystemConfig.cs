using System.Collections.Generic;
using System.Collections.ObjectModel;

using Entrance.Model;

namespace Entrance.Common
{
    public static class SystemConfig
    {
        // **************************************************
        // 設定の最大・最小値
        // **************************************************
        // ノーツスピード最小値
        public static readonly int MIN_NOTES_SPEED = 10;
        // ノーツスピード最大値
        public static readonly int MAX_NOTES_SPEED = 120;
        // シビアレベル最小値
        public static readonly int MIN_JUDGE_LEVEL = 1;
        // シビアレベル最大値
        public static readonly int MAX_JUDGE_LEVEL = 20;
        // タイミング最小値
        public static readonly int MIN_TIMING = -20;
        // タイミング最大値
        public static readonly int MAX_TIMING = 20;

        // **************************************************
        // ライブ設定
        // **************************************************
        // ライブ画面に遷移後、ライブを開始するまでの時間
        public static readonly float MUSIC_START_OFFSET = 3.0f;

        // **************************************************
        // ウィンドウが管理するスクリーン定義
        // **************************************************
        public static readonly ReadOnlyDictionary<WindowList, ScreenSetting> SCENE_MANAGE
            = new ReadOnlyDictionary<WindowList, ScreenSetting>(new Dictionary<WindowList, ScreenSetting>()
            {
                {
                    WindowList.TitleWindow, new ScreenSetting(true, new List<ScreenList>()
                        {
                            ScreenList.TitleScreen
                        }
                    )
                },
                {
                    WindowList.SelectWindow, new ScreenSetting(true, new List<ScreenList>()
                        {
                            ScreenList.SelectMusicScreen,
                            ScreenList.SelectDifficultScreen,
                            ScreenList.SelectPreparationScreen,
                            ScreenList.SettingNotesSpeed,
                            ScreenList.SettingSevereLevel,
                            ScreenList.SettingTiming
                        }
                    )
                },
                {
                    WindowList.MusicGameWindow, new ScreenSetting(true, new List<ScreenList>()
                        {
                        ScreenList.MusicGameScreen
                        }
                    )
                },
                {
                    WindowList.ResultWindow, new ScreenSetting(true, new List<ScreenList>()
                        {
                            ScreenList.ResultScreen
                        }
                    )
                }
            });
    }
}
