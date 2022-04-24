using UnityEngine;

using Entrance.Common;

namespace Entrance.Model
{
    public class NotesSE
    {
        // **************************************************
        // ノーツSE情報定義
        // **************************************************
        private AudioSource normalNotesSESource;
        private AudioClip normalNotesSEClip;

        // ********************************************************************************
        // 初期処理
        // ********************************************************************************
        public void Initialize(SessionCommon sessionCommon)
        {
            // ノーマルノーツSE
            normalNotesSESource = GameObject.Find("NormalNotesSE").GetComponent<AudioSource>();
            normalNotesSEClip = normalNotesSESource.clip;
            normalNotesSESource.volume = sessionCommon.LiveVolumeSE;
        }

        // ********************************************************************************
        // ノーマルノーツのSEを再生する
        // ********************************************************************************
        public void PlayNormalNotesSE()
        {
            normalNotesSESource.PlayOneShot(normalNotesSEClip);
        }
    }
}
