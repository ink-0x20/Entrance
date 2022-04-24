using UnityEngine;

namespace Entrance.Utils
{
    public static class AudioUtils
    {
        private static AudioSource backGroungMusic = null;

        // ********************************************************************************
        // �V�X�e��BGM���Đ�����
        // ********************************************************************************
        public static void PlaySystemBGM(AudioClip audioClip, float volume)
        {
            SettingMusic(audioClip, volume);
            backGroungMusic.loop = true;
            backGroungMusic.Play();
        }

        // ********************************************************************************
        // �I�ȗp���C�u�y�Ȃ��Đ�����
        // ********************************************************************************
        public static void PlayLiveMusic(AudioClip audioClip, float volume)
        {
            SettingMusic(audioClip, volume);
            backGroungMusic.loop = true;
            backGroungMusic.Play();
        }

        // ********************************************************************************
        // ���C�u�y�Ȃ��Đ��\�񂷂�
        // ********************************************************************************
        public static void PlayScheduledLiveMusic(AudioClip audioClip, float volume, double time)
        {
            SettingMusic(audioClip, volume);
            backGroungMusic.loop = false;
            backGroungMusic.Stop();
            backGroungMusic.PlayScheduled(time);
        }

        // ********************************************************************************
        // ���y�ݒ�
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
