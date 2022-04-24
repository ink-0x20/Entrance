using System;
using UnityEngine;
using UnityEngine.UI;

using Entrance.Common;
using Entrance.Utils;

using TMPro;

namespace Entrance.View
{
    public class ResultView : MonoBehaviour
    {
        // **************************************************
        // �C���X�y�N�^�I�u�W�F�N�g
        // **************************************************
        // �e�L�X�g�I�u�W�F�N�g
        [SerializeField]
        private Text musicName = default;
        [SerializeField]
        private TMP_Text score = default;
        [SerializeField]
        private TMP_Text highScore = default;
        [SerializeField]
        private TMP_Text perfect = default;
        [SerializeField]
        private TMP_Text great = default;
        [SerializeField]
        private TMP_Text good = default;
        [SerializeField]
        private TMP_Text bad = default;
        [SerializeField]
        private TMP_Text miss = default;
        [SerializeField]
        private TMP_Text maxCombo = default;
        // ���C���y�ȃA�C�R��
        [SerializeField]
        private Image mainMusicIcon = default;
        // �X�R�A�����N
        [SerializeField]
        private GameObject scoreRankD = default;
        [SerializeField]
        private GameObject scoreRankC = default;
        [SerializeField]
        private GameObject scoreRankB = default;
        [SerializeField]
        private GameObject scoreRankA = default;
        [SerializeField]
        private GameObject scoreRankS = default;
        [SerializeField]
        private GameObject scoreRankSS = default;
        [SerializeField]
        private GameObject scoreRankSSS = default;

        // **************************************************
        // �C�x���g���X�i�[��`
        // **************************************************
        // �I����ʑJ��
        public Action selectScene;

        // ********************************************************************************
        // ��������
        // ********************************************************************************
        public void Initialize(SessionCommon sessionCommon)
        {
            // ********************************************************************************
            // ���C���A�C�R��
            // ********************************************************************************
            // �A�C�R���ύX
            mainMusicIcon.sprite = sessionCommon.MusicIcon;

            // ********************************************************************************
            // ���ʕ\��
            // ********************************************************************************
            ScoreDetail scoreDetail = sessionCommon.GetScoreHistory();
            musicName.text = sessionCommon.MusicName;
            score.text = scoreDetail.Score.ToString();
            highScore.text = sessionCommon.GetBestScoreHistory().Score.ToString();
            perfect.text = scoreDetail.Perfect.ToString();
            great.text = scoreDetail.Great.ToString();
            good.text = scoreDetail.Good.ToString();
            bad.text = scoreDetail.Bad.ToString();
            miss.text = scoreDetail.Miss.ToString();
            maxCombo.text = scoreDetail.MaxCombo.ToString();
            switch (scoreDetail.ScoreRank)
            {
                case ScoreRank.D:
                    scoreRankD.SetActive(true);
                    break;
                case ScoreRank.C:
                    scoreRankC.SetActive(true);
                    break;
                case ScoreRank.B:
                    scoreRankB.SetActive(true);
                    break;
                case ScoreRank.A:
                    scoreRankA.SetActive(true);
                    break;
                case ScoreRank.S:
                    scoreRankS.SetActive(true);
                    break;
                case ScoreRank.SS:
                    scoreRankSS.SetActive(true);
                    break;
                case ScoreRank.SSS:
                    scoreRankSSS.SetActive(true);
                    break;
            }
        }

        // ********************************************************************************
        // �t���[��������
        // ********************************************************************************
        void Update()
        {
            // ********************************************************************************
            // �A�v���I������
            // ********************************************************************************
            KeyboardPressUtils.ExitApplication();

            // ********************************************************************************
            // ���͊Ď�
            // ********************************************************************************
            if (KeyboardPressUtils.Enter())
            {
                SystemUtils.SafeCall(selectScene);
            }
        }
    }
}
