using UnityEngine;

namespace Entrance.Utils
{
    public static class AudioUtils
    {
        private static AudioSource backGroungMusic = null;

        // ********************************************************************************
        // システムBGMを再生する
        // ********************************************************************************
        public static void PlaySystemBGM(AudioClip audioClip, float volume)
        {
            SettingMusic(audioClip, volume);
            backGroungMusic.loop = true;
            backGroungMusic.Play();
        }

        // ********************************************************************************
        // 選曲用ライブ楽曲を再生する
        // ********************************************************************************
        public static void PlayLiveMusic(AudioClip audioClip, float volume)
        {
            SettingMusic(audioClip, volume);
            backGroungMusic.loop = true;
            backGroungMusic.Play();
        }

        // ********************************************************************************
        // ライブ楽曲を再生予約する
        // ********************************************************************************
        public static void PlayScheduledLiveMusic(AudioClip audioClip, float volume, double time)
        {
            SettingMusic(audioClip, volume);
            backGroungMusic.loop = false;
            backGroungMusic.Stop();
            backGroungMusic.PlayScheduled(time);
        }

        // ********************************************************************************
        // 音楽設定
        // ********************************************************************************
        private static void SettingMusic(AudioClip audioClip, float volume)
        {
            if (backGroungMusic == null)
            {
                backGroungMusic = GameObject.Find("BackGroungMusic").GetComponent<AudioSource>();
            }
            backGroungMusic.clip = audioClip;
            backGroungMusic.volume = volume;
        }
    }
}
