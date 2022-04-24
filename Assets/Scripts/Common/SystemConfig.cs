using System.Collections.Generic;
using System.Collections.ObjectModel;

using Entrance.Model;

namespace Entrance.Common
{
    public static class SystemConfig
    {
        // **************************************************
        // �ݒ�̍ő�E�ŏ��l
        // **************************************************
        // �m�[�c�X�s�[�h�ŏ��l
        public static readonly int MIN_NOTES_SPEED = 10;
        // �m�[�c�X�s�[�h�ő�l
        public static readonly int MAX_NOTES_SPEED = 120;
        // �V�r�A���x���ŏ��l
        public static readonly int MIN_JUDGE_LEVEL = 1;
        // �V�r�A���x���ő�l
        public static readonly int MAX_JUDGE_LEVEL = 20;
        // �^�C�~���O�ŏ��l
        public static readonly int MIN_TIMING = -20;
        // �^�C�~���O�ő�l
        public static readonly int MAX_TIMING = 20;

        // **************************************************
        // ���C�u�ݒ�
        // **************************************************
        // ���C�u��ʂɑJ�ڌ�A���C�u���J�n����܂ł̎���
        public static readonly float MUSIC_START_OFFSET = 3.0f;

        // **************************************************
        // �E�B���h�E���Ǘ�����X�N���[����`
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
