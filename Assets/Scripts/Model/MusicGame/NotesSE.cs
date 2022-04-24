using UnityEngine;

using Entrance.Common;

namespace Entrance.Model
{
    public class NotesSE
    {
        // **************************************************
        // �m�[�cSE����`
        // **************************************************
        private AudioSource normalNotesSESource;
        private AudioClip normalNotesSEClip;

        // ********************************************************************************
        // ��������
        // ********************************************************************************
        public void Initialize(SessionCommon sessionCommon)
        {
            // �m�[�}���m�[�cSE
            normalNotesSESource = GameObject.Find("NormalNotesSE").GetComponent<AudioSource>();
            normalNotesSEClip = normalNotesSESource.clip;
            normalNotesSESource.volume = sessionCommon.LiveVolumeSE;
        }

        // ********************************************************************************
        // �m�[�}���m�[�c��SE���Đ�����
        // ********************************************************************************
        public void PlayNormalNotesSE()
        {
            normalNotesSESource.PlayOneShot(normalNotesSEClip);
        }
    }
}
