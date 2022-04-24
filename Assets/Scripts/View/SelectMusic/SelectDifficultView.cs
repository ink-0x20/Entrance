using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

using Entrance.Common;
using Entrance.DTO;
using Entrance.Model;
using Entrance.Utils;

namespace Entrance.View
{
    public class SelectDifficultView : MonoBehaviour
    {
        // **************************************************
        // �C���X�y�N�^�I�u�W�F�N�g
        // **************************************************
        // ���C���y�ȃA�C�R��
        [SerializeField]
        private Image mainMusicIcon = default;
        [SerializeField]
        private Text mainMusicName = default;
        // ��Փx
        [SerializeField]
        private GameObject musicEasy = default;
        [SerializeField]
        private GameObject musicNormal = default;
        [SerializeField]
        private GameObject musicHard = default;
        [SerializeField]
        private GameObject musicExtreme = default;
        [SerializeField]
        private GameObject musicChallenge = default;
        // ��Փx�I���A�C�R��
        [SerializeField]
        private GameObject selectEasy = default;
        [SerializeField]
        private GameObject nonSelectEasy = default;
        [SerializeField]
        private GameObject selectNormal = default;
        [SerializeField]
        private GameObject nonSelectNormal = default;
        [SerializeField]
        private GameObject selectHard = default;
        [SerializeField]
        private GameObject nonSelectHard = default;
        [SerializeField]
        private GameObject selectExtreme = default;
        [SerializeField]
        private GameObject nonSelectExtreme = default;
        [SerializeField]
        private GameObject selectChallenge = default;
        [SerializeField]
        private GameObject nonSelectChallenge = default;

        // **************************************************
        // �C�x���g���X�i�[��`
        // **************************************************
        // ��Փx�I���C�x���g
        public Action selectDifficultListener1;
        public Action selectDifficultListener2;
        public Action selectDifficultListener3;
        public Action selectDifficultListener4;
        public Action selectDifficultListener5;
        // ��Փx����C�x���g
        public Action decideListener;
        public Action cancelListener;

        // **************************************************
        // �I���E��I���X�P�[��
        // **************************************************
        private Vector3 selectScale = new Vector3(2.3f, 2.3f, 1.0f);
        private Vector3 nonSelectScale = new Vector3(1.81f, 1.8f, 1.0f);

        // **************************************************
        // ��Փx�p�I�u�W�F�N�g
        // **************************************************
        private Dictionary<string, GameObject> difficultObject = new Dictionary<string, GameObject>();

        // ********************************************************************************
        // ��������
        // ********************************************************************************
        public void Initialize(SessionCommon sessionCommon)
        {
            // ********************************************************************************
            // ��Փx�I�u�W�F�N�g�擾
            // ********************************************************************************
            for (int i = 1; i <= 10; i++)
            {
                string nonStarKey = "NonStar" + i.ToString();
                string starKey = "Star" + i.ToString();
                string redStarKey = "RedStar" + i.ToString();
                // Easy
                difficultObject.Add(DifficultyType.EASY.ToString() + "_" + nonStarKey,
                    musicEasy.transform.Find(nonStarKey).gameObject);
                difficultObject.Add(DifficultyType.EASY.ToString() + "_" + starKey,
                    musicEasy.transform.Find(starKey).gameObject);
                difficultObject.Add(DifficultyType.EASY.ToString() + "_" + redStarKey,
                    musicEasy.transform.Find(redStarKey).gameObject);
                // Normal
                difficultObject.Add(DifficultyType.NORMAL.ToString() + "_" + nonStarKey,
                    musicNormal.transform.Find(nonStarKey).gameObject);
                difficultObject.Add(DifficultyType.NORMAL.ToString() + "_" + starKey,
                    musicNormal.transform.Find(starKey).gameObject);
                difficultObject.Add(DifficultyType.NORMAL.ToString() + "_" + redStarKey,
                    musicNormal.transform.Find(redStarKey).gameObject);
                // Hard
                difficultObject.Add(DifficultyType.HARD.ToString() + "_" + nonStarKey,
                    musicHard.transform.Find(nonStarKey).gameObject);
                difficultObject.Add(DifficultyType.HARD.ToString() + "_" + starKey,
                    musicHard.transform.Find(starKey).gameObject);
                difficultObject.Add(DifficultyType.HARD.ToString() + "_" + redStarKey,
                    musicHard.transform.Find(redStarKey).gameObject);
                // Extreme
                difficultObject.Add(DifficultyType.EXTREME.ToString() + "_" + nonStarKey,
                    musicExtreme.transform.Find(nonStarKey).gameObject);
                difficultObject.Add(DifficultyType.EXTREME.ToString() + "_" + starKey,
                    musicExtreme.transform.Find(starKey).gameObject);
                difficultObject.Add(DifficultyType.EXTREME.ToString() + "_" + redStarKey,
                    musicExtreme.transform.Find(redStarKey).gameObject);
                // Challenge
                difficultObject.Add(DifficultyType.CHALLENGE.ToString() + "_" + nonStarKey,
                    musicChallenge.transform.Find(nonStarKey).gameObject);
                difficultObject.Add(DifficultyType.CHALLENGE.ToString() + "_" + starKey,
                    musicChallenge.transform.Find(starKey).gameObject);
                difficultObject.Add(DifficultyType.CHALLENGE.ToString() + "_" + redStarKey,
                    musicChallenge.transform.Find(redStarKey).gameObject);
            }
            // �t���R���{
            difficultObject.Add(DifficultyType.EASY.ToString() + "_FullCombo", musicEasy.transform.Find("FullCombo").gameObject);
            difficultObject.Add(DifficultyType.NORMAL.ToString() + "_FullCombo", musicNormal.transform.Find("FullCombo").gameObject);
            difficultObject.Add(DifficultyType.HARD.ToString() + "_FullCombo", musicHard.transform.Find("FullCombo").gameObject);
            difficultObject.Add(DifficultyType.EXTREME.ToString() + "_FullCombo", musicExtreme.transform.Find("FullCombo").gameObject);
            difficultObject.Add(DifficultyType.CHALLENGE.ToString() + "_FullCombo", musicChallenge.transform.Find("FullCombo").gameObject);
            // �X�R�A�����N
            difficultObject.Add(DifficultyType.EASY.ToString() + "_D", musicEasy.transform.Find("D").gameObject);
            difficultObject.Add(DifficultyType.EASY.ToString() + "_C", musicEasy.transform.Find("C").gameObject);
            difficultObject.Add(DifficultyType.EASY.ToString() + "_B", musicEasy.transform.Find("B").gameObject);
            difficultObject.Add(DifficultyType.EASY.ToString() + "_A", musicEasy.transform.Find("A").gameObject);
            difficultObject.Add(DifficultyType.EASY.ToString() + "_S", musicEasy.transform.Find("S").gameObject);
            difficultObject.Add(DifficultyType.EASY.ToString() + "_SS", musicEasy.transform.Find("SS").gameObject);
            difficultObject.Add(DifficultyType.EASY.ToString() + "_SSS", musicEasy.transform.Find("SSS").gameObject);
            difficultObject.Add(DifficultyType.NORMAL.ToString() + "_D", musicNormal.transform.Find("D").gameObject);
            difficultObject.Add(DifficultyType.NORMAL.ToString() + "_C", musicNormal.transform.Find("C").gameObject);
            difficultObject.Add(DifficultyType.NORMAL.ToString() + "_B", musicNormal.transform.Find("B").gameObject);
            difficultObject.Add(DifficultyType.NORMAL.ToString() + "_A", musicNormal.transform.Find("A").gameObject);
            difficultObject.Add(DifficultyType.NORMAL.ToString() + "_S", musicNormal.transform.Find("S").gameObject);
            difficultObject.Add(DifficultyType.NORMAL.ToString() + "_SS", musicNormal.transform.Find("SS").gameObject);
            difficultObject.Add(DifficultyType.NORMAL.ToString() + "_SSS", musicNormal.transform.Find("SSS").gameObject);
            difficultObject.Add(DifficultyType.HARD.ToString() + "_D", musicHard.transform.Find("D").gameObject);
            difficultObject.Add(DifficultyType.HARD.ToString() + "_C", musicHard.transform.Find("C").gameObject);
            difficultObject.Add(DifficultyType.HARD.ToString() + "_B", musicHard.transform.Find("B").gameObject);
            difficultObject.Add(DifficultyType.HARD.ToString() + "_A", musicHard.transform.Find("A").gameObject);
            difficultObject.Add(DifficultyType.HARD.ToString() + "_S", musicHard.transform.Find("S").gameObject);
            difficultObject.Add(DifficultyType.HARD.ToString() + "_SS", musicHard.transform.Find("SS").gameObject);
            difficultObject.Add(DifficultyType.HARD.ToString() + "_SSS", musicHard.transform.Find("SSS").gameObject);
            difficultObject.Add(DifficultyType.EXTREME.ToString() + "_D", musicExtreme.transform.Find("D").gameObject);
            difficultObject.Add(DifficultyType.EXTREME.ToString() + "_C", musicExtreme.transform.Find("C").gameObject);
            difficultObject.Add(DifficultyType.EXTREME.ToString() + "_B", musicExtreme.transform.Find("B").gameObject);
            difficultObject.Add(DifficultyType.EXTREME.ToString() + "_A", musicExtreme.transform.Find("A").gameObject);
            difficultObject.Add(DifficultyType.EXTREME.ToString() + "_S", musicExtreme.transform.Find("S").gameObject);
            difficultObject.Add(DifficultyType.EXTREME.ToString() + "_SS", musicExtreme.transform.Find("SS").gameObject);
            difficultObject.Add(DifficultyType.EXTREME.ToString() + "_SSS", musicExtreme.transform.Find("SSS").gameObject);
            difficultObject.Add(DifficultyType.CHALLENGE.ToString() + "_D", musicChallenge.transform.Find("D").gameObject);
            difficultObject.Add(DifficultyType.CHALLENGE.ToString() + "_C", musicChallenge.transform.Find("C").gameObject);
            difficultObject.Add(DifficultyType.CHALLENGE.ToString() + "_B", musicChallenge.transform.Find("B").gameObject);
            difficultObject.Add(DifficultyType.CHALLENGE.ToString() + "_A", musicChallenge.transform.Find("A").gameObject);
            difficultObject.Add(DifficultyType.CHALLENGE.ToString() + "_S", musicChallenge.transform.Find("S").gameObject);
            difficultObject.Add(DifficultyType.CHALLENGE.ToString() + "_SS", musicChallenge.transform.Find("SS").gameObject);
            difficultObject.Add(DifficultyType.CHALLENGE.ToString() + "_SSS", musicChallenge.transform.Find("SSS").gameObject);

            // ********************************************************************************
            // ���C���A�C�R��
            // ********************************************************************************
            // �A�C�R���ύX
            mainMusicIcon.sprite = sessionCommon.MusicIcon;
            // �y�Ȗ��ύX
            mainMusicName.text = sessionCommon.MusicName;

            // ********************************************************************************
            // ��Փx�f�[�^�Z�b�g
            // ********************************************************************************
            // ��Փx�擾�L�[
            string keyEasy = sessionCommon.MusicName + "_" + CategoryType.ENTRANCE + "_" + DifficultyType.EASY;
            string keyNormal = sessionCommon.MusicName + "_" + CategoryType.ENTRANCE + "_" + DifficultyType.NORMAL;
            string keyHard = sessionCommon.MusicName + "_" + CategoryType.ENTRANCE + "_" + DifficultyType.HARD;
            string keyExtreme = sessionCommon.MusicName + "_" + CategoryType.ENTRANCE + "_" + DifficultyType.EXTREME;
            string keyChallenge = sessionCommon.MusicName + "_" + CategoryType.ENTRANCE + "_" + DifficultyType.CHALLENGE;
            // ��Փx�擾
            MusicDTO.EditData musicData = JsonUtility.FromJson<MusicDTO.EditData>(Resources.Load<TextAsset>(ResourcesPathUtils.MusicData(sessionCommon)).ToString());
            sessionCommon.MusicLevelList.TryGetValue(keyEasy, out int levelEasy);
            sessionCommon.MusicLevelList.TryGetValue(keyNormal, out int levelNormal);
            sessionCommon.MusicLevelList.TryGetValue(keyHard, out int levelHard);
            sessionCommon.MusicLevelList.TryGetValue(keyExtreme, out int levelExtreme);
            sessionCommon.MusicLevelList.TryGetValue(keyChallenge, out int levelChallenge);
            // ��Փx�\��
            for (int i = 1; i <= 10; i++)
            {
                string nonStarKey = "NonStar" + i.ToString();
                string starKey = "Star" + i.ToString();
                string redStarKey = "RedStar" + i.ToString();
                // Easy
                if (levelEasy <= 0)
                {
                    difficultObject[DifficultyType.EASY.ToString() + "_" + nonStarKey].SetActive(true);
                    difficultObject[DifficultyType.EASY.ToString() + "_" + starKey].SetActive(false);
                    difficultObject[DifficultyType.EASY.ToString() + "_" + redStarKey].SetActive(false);
                }
                else if (10 < levelEasy && i + 10 <= levelEasy)
                {
                    // �ԃX�^�[
                    difficultObject[DifficultyType.EASY.ToString() + "_" + nonStarKey].SetActive(false);
                    difficultObject[DifficultyType.EASY.ToString() + "_" + starKey].SetActive(false);
                    difficultObject[DifficultyType.EASY.ToString() + "_" + redStarKey].SetActive(true);
                }
                else if (i <= levelEasy)
                {
                    // �ʏ�X�^�[
                    difficultObject[DifficultyType.EASY.ToString() + "_" + nonStarKey].SetActive(false);
                    difficultObject[DifficultyType.EASY.ToString() + "_" + starKey].SetActive(true);
                    difficultObject[DifficultyType.EASY.ToString() + "_" + redStarKey].SetActive(false);
                }
                else
                {
                    difficultObject[DifficultyType.EASY.ToString() + "_" + nonStarKey].SetActive(true);
                    difficultObject[DifficultyType.EASY.ToString() + "_" + starKey].SetActive(false);
                    difficultObject[DifficultyType.EASY.ToString() + "_" + redStarKey].SetActive(false);
                }
                // Normal
                if (levelNormal <= 0)
                {
                    difficultObject[DifficultyType.NORMAL.ToString() + "_" + nonStarKey].SetActive(true);
                    difficultObject[DifficultyType.NORMAL.ToString() + "_" + starKey].SetActive(false);
                    difficultObject[DifficultyType.NORMAL.ToString() + "_" + redStarKey].SetActive(false);
                }
                else if (10 < levelNormal && i + 10 <= levelNormal)
                {
                    // �ԃX�^�[
                    difficultObject[DifficultyType.NORMAL.ToString() + "_" + nonStarKey].SetActive(false);
                    difficultObject[DifficultyType.NORMAL.ToString() + "_" + starKey].SetActive(false);
                    difficultObject[DifficultyType.NORMAL.ToString() + "_" + redStarKey].SetActive(true);
                }
                else if (i <= levelNormal)
                {
                    // �ʏ�X�^�[
                    difficultObject[DifficultyType.NORMAL.ToString() + "_" + nonStarKey].SetActive(false);
                    difficultObject[DifficultyType.NORMAL.ToString() + "_" + starKey].SetActive(true);
                    difficultObject[DifficultyType.NORMAL.ToString() + "_" + redStarKey].SetActive(false);
                }
                else
                {
                    difficultObject[DifficultyType.NORMAL.ToString() + "_" + nonStarKey].SetActive(true);
                    difficultObject[DifficultyType.NORMAL.ToString() + "_" + starKey].SetActive(false);
                    difficultObject[DifficultyType.NORMAL.ToString() + "_" + redStarKey].SetActive(false);
                }
                // Hard
                if (levelHard <= 0)
                {
                    difficultObject[DifficultyType.HARD.ToString() + "_" + nonStarKey].SetActive(true);
                    difficultObject[DifficultyType.HARD.ToString() + "_" + starKey].SetActive(false);
                    difficultObject[DifficultyType.HARD.ToString() + "_" + redStarKey].SetActive(false);
                }
                else if (10 < levelHard && i + 10 <= levelHard)
                {
                    // �ԃX�^�[
                    difficultObject[DifficultyType.HARD.ToString() + "_" + nonStarKey].SetActive(false);
                    difficultObject[DifficultyType.HARD.ToString() + "_" + starKey].SetActive(false);
                    difficultObject[DifficultyType.HARD.ToString() + "_" + redStarKey].SetActive(true);
                }
                else if (i <= levelHard)
                {
                    // �ʏ�X�^�[
                    difficultObject[DifficultyType.HARD.ToString() + "_" + nonStarKey].SetActive(false);
                    difficultObject[DifficultyType.HARD.ToString() + "_" + starKey].SetActive(true);
                    difficultObject[DifficultyType.HARD.ToString() + "_" + redStarKey].SetActive(false);
                }
                else
                {
                    difficultObject[DifficultyType.HARD.ToString() + "_" + nonStarKey].SetActive(true);
                    difficultObject[DifficultyType.HARD.ToString() + "_" + starKey].SetActive(false);
                    difficultObject[DifficultyType.HARD.ToString() + "_" + redStarKey].SetActive(false);
                }
                // Extreme
                if (levelExtreme <= 0)
                {
                    difficultObject[DifficultyType.EXTREME.ToString() + "_" + nonStarKey].SetActive(true);
                    difficultObject[DifficultyType.EXTREME.ToString() + "_" + starKey].SetActive(false);
                    difficultObject[DifficultyType.EXTREME.ToString() + "_" + redStarKey].SetActive(false);
                }
                else if (10 < levelExtreme && i + 10 <= levelExtreme)
                {
                    // �ԃX�^�[
                    difficultObject[DifficultyType.EXTREME.ToString() + "_" + nonStarKey].SetActive(false);
                    difficultObject[DifficultyType.EXTREME.ToString() + "_" + starKey].SetActive(false);
                    difficultObject[DifficultyType.EXTREME.ToString() + "_" + redStarKey].SetActive(true);
                }
                else if (i <= levelExtreme)
                {
                    // �ʏ�X�^�[
                    difficultObject[DifficultyType.EXTREME.ToString() + "_" + nonStarKey].SetActive(false);
                    difficultObject[DifficultyType.EXTREME.ToString() + "_" + starKey].SetActive(true);
                    difficultObject[DifficultyType.EXTREME.ToString() + "_" + redStarKey].SetActive(false);
                }
                else
                {
                    difficultObject[DifficultyType.EXTREME.ToString() + "_" + nonStarKey].SetActive(true);
                    difficultObject[DifficultyType.EXTREME.ToString() + "_" + starKey].SetActive(false);
                    difficultObject[DifficultyType.EXTREME.ToString() + "_" + redStarKey].SetActive(false);
                }
                // Challenge
                if (levelChallenge <= 0)
                {
                    difficultObject[DifficultyType.CHALLENGE.ToString() + "_" + nonStarKey].SetActive(true);
                    difficultObject[DifficultyType.CHALLENGE.ToString() + "_" + starKey].SetActive(false);
                    difficultObject[DifficultyType.CHALLENGE.ToString() + "_" + redStarKey].SetActive(false);
                }
                else if (10 < levelChallenge && i + 10 <= levelChallenge)
                {
                    // �ԃX�^�[
                    difficultObject[DifficultyType.CHALLENGE.ToString() + "_" + nonStarKey].SetActive(false);
                    difficultObject[DifficultyType.CHALLENGE.ToString() + "_" + starKey].SetActive(false);
                    difficultObject[DifficultyType.CHALLENGE.ToString() + "_" + redStarKey].SetActive(true);
                }
                else if (i <= levelChallenge)
                {
                    // �ʏ�X�^�[
                    difficultObject[DifficultyType.CHALLENGE.ToString() + "_" + nonStarKey].SetActive(false);
                    difficultObject[DifficultyType.CHALLENGE.ToString() + "_" + starKey].SetActive(true);
                    difficultObject[DifficultyType.CHALLENGE.ToString() + "_" + redStarKey].SetActive(false);
                }
                else
                {
                    difficultObject[DifficultyType.CHALLENGE.ToString() + "_" + nonStarKey].SetActive(true);
                    difficultObject[DifficultyType.CHALLENGE.ToString() + "_" + starKey].SetActive(false);
                    difficultObject[DifficultyType.CHALLENGE.ToString() + "_" + redStarKey].SetActive(false);
                }
                // �X�R�A�����擾
                ScoreDetail scoreDetailEasy = sessionCommon.GetBestScoreHistory(sessionCommon.MusicName, CategoryType.ENTRANCE, DifficultyType.EASY);
                ScoreDetail scoreDetailNormal = sessionCommon.GetBestScoreHistory(sessionCommon.MusicName, CategoryType.ENTRANCE, DifficultyType.NORMAL);
                ScoreDetail scoreDetailHard = sessionCommon.GetBestScoreHistory(sessionCommon.MusicName, CategoryType.ENTRANCE, DifficultyType.HARD);
                ScoreDetail scoreDetailExtreme = sessionCommon.GetBestScoreHistory(sessionCommon.MusicName, CategoryType.ENTRANCE, DifficultyType.EXTREME);
                ScoreDetail scoreDetailChallenge = sessionCommon.GetBestScoreHistory(sessionCommon.MusicName, CategoryType.ENTRANCE, DifficultyType.CHALLENGE);
                // �t���R���{�\��
                difficultObject[DifficultyType.EASY.ToString() + "_FullCombo"].SetActive(scoreDetailEasy.IsFullCombo);
                difficultObject[DifficultyType.NORMAL.ToString() + "_FullCombo"].SetActive(scoreDetailNormal.IsFullCombo);
                difficultObject[DifficultyType.HARD.ToString() + "_FullCombo"].SetActive(scoreDetailHard.IsFullCombo);
                difficultObject[DifficultyType.EXTREME.ToString() + "_FullCombo"].SetActive(scoreDetailExtreme.IsFullCombo);
                difficultObject[DifficultyType.CHALLENGE.ToString() + "_FullCombo"].SetActive(scoreDetailChallenge.IsFullCombo);
                // �X�R�A�����N�\��(�������񃊃Z�b�g)
                difficultObject[DifficultyType.EASY.ToString() + "_D"].SetActive(false);
                difficultObject[DifficultyType.EASY.ToString() + "_C"].SetActive(false);
                difficultObject[DifficultyType.EASY.ToString() + "_B"].SetActive(false);
                difficultObject[DifficultyType.EASY.ToString() + "_A"].SetActive(false);
                difficultObject[DifficultyType.EASY.ToString() + "_S"].SetActive(false);
                difficultObject[DifficultyType.EASY.ToString() + "_SS"].SetActive(false);
                difficultObject[DifficultyType.EASY.ToString() + "_SSS"].SetActive(false);
                difficultObject[DifficultyType.NORMAL.ToString() + "_D"].SetActive(false);
                difficultObject[DifficultyType.NORMAL.ToString() + "_C"].SetActive(false);
                difficultObject[DifficultyType.NORMAL.ToString() + "_B"].SetActive(false);
                difficultObject[DifficultyType.NORMAL.ToString() + "_A"].SetActive(false);
                difficultObject[DifficultyType.NORMAL.ToString() + "_S"].SetActive(false);
                difficultObject[DifficultyType.NORMAL.ToString() + "_SS"].SetActive(false);
                difficultObject[DifficultyType.NORMAL.ToString() + "_SSS"].SetActive(false);
                difficultObject[DifficultyType.HARD.ToString() + "_D"].SetActive(false);
                difficultObject[DifficultyType.HARD.ToString() + "_C"].SetActive(false);
                difficultObject[DifficultyType.HARD.ToString() + "_B"].SetActive(false);
                difficultObject[DifficultyType.HARD.ToString() + "_A"].SetActive(false);
                difficultObject[DifficultyType.HARD.ToString() + "_S"].SetActive(false);
                difficultObject[DifficultyType.HARD.ToString() + "_SS"].SetActive(false);
                difficultObject[DifficultyType.HARD.ToString() + "_SSS"].SetActive(false);
                difficultObject[DifficultyType.EXTREME.ToString() + "_D"].SetActive(false);
                difficultObject[DifficultyType.EXTREME.ToString() + "_C"].SetActive(false);
                difficultObject[DifficultyType.EXTREME.ToString() + "_B"].SetActive(false);
                difficultObject[DifficultyType.EXTREME.ToString() + "_A"].SetActive(false);
                difficultObject[DifficultyType.EXTREME.ToString() + "_S"].SetActive(false);
                difficultObject[DifficultyType.EXTREME.ToString() + "_SS"].SetActive(false);
                difficultObject[DifficultyType.EXTREME.ToString() + "_SSS"].SetActive(false);
                difficultObject[DifficultyType.CHALLENGE.ToString() + "_D"].SetActive(false);
                difficultObject[DifficultyType.CHALLENGE.ToString() + "_C"].SetActive(false);
                difficultObject[DifficultyType.CHALLENGE.ToString() + "_B"].SetActive(false);
                difficultObject[DifficultyType.CHALLENGE.ToString() + "_A"].SetActive(false);
                difficultObject[DifficultyType.CHALLENGE.ToString() + "_S"].SetActive(false);
                difficultObject[DifficultyType.CHALLENGE.ToString() + "_SS"].SetActive(false);
                difficultObject[DifficultyType.CHALLENGE.ToString() + "_SSS"].SetActive(false);
                // �X�R�A�����N�\��(�Ώۂ̂ݕ\��)
                if (scoreDetailEasy.ScoreRank != ScoreRank.NON)
                {
                    difficultObject[DifficultyType.EASY.ToString() + "_" + scoreDetailEasy.ScoreRank.ToString()].SetActive(true);
                }
                if (scoreDetailNormal.ScoreRank != ScoreRank.NON)
                {
                    difficultObject[DifficultyType.NORMAL.ToString() + "_" + scoreDetailNormal.ScoreRank.ToString()].SetActive(true);
                }
                if (scoreDetailHard.ScoreRank != ScoreRank.NON)
                {
                    difficultObject[DifficultyType.HARD.ToString() + "_" + scoreDetailHard.ScoreRank.ToString()].SetActive(true);
                }
                if (scoreDetailExtreme.ScoreRank != ScoreRank.NON)
                {
                    difficultObject[DifficultyType.EXTREME.ToString() + "_" + scoreDetailExtreme.ScoreRank.ToString()].SetActive(true);
                }
                if (scoreDetailChallenge.ScoreRank != ScoreRank.NON)
                {
                    difficultObject[DifficultyType.CHALLENGE.ToString() + "_" + scoreDetailChallenge.ScoreRank.ToString()].SetActive(true);
                }
            }
        }

        void Update()
        {
            // ********************************************************************************
            // �A�v���I������
            // ********************************************************************************
            KeyboardPressUtils.ExitApplication();

            // ********************************************************************************
            // ���͊Ď�
            // ********************************************************************************
            if (KeyboardPressUtils.All3())
            {
                SystemUtils.SafeCall(selectDifficultListener5);
            }
            if (KeyboardPressUtils.All4())
            {
                SystemUtils.SafeCall(selectDifficultListener3);
            }
            if (KeyboardPressUtils.All5())
            {
                SystemUtils.SafeCall(selectDifficultListener1);
            }
            if (KeyboardPressUtils.All6())
            {
                SystemUtils.SafeCall(selectDifficultListener2);
            }
            if (KeyboardPressUtils.All7())
            {
                SystemUtils.SafeCall(selectDifficultListener4);
            }
            if (KeyboardPressUtils.Enter())
            {
                SystemUtils.SafeCall(decideListener);
            }
            if (KeyboardPressUtils.All0())
            {
                SystemUtils.SafeCall(cancelListener);
            }
        }

        // ********************************************************************************
        // ��Փx�C���[�W��K�p
        // ********************************************************************************
        public void SetDifficulyList(DifficultListModel difficultListModel)
        {
            if (difficultListModel.DifficultFlg1)
            {
                selectNormal.SetActive(true);
                nonSelectNormal.SetActive(false);
            }
            if (difficultListModel.DifficultFlg2)
            {
                selectHard.SetActive(true);
                nonSelectHard.SetActive(false);
            }
            if (difficultListModel.DifficultFlg3)
            {
                selectEasy.SetActive(true);
                nonSelectEasy.SetActive(false);
            }
            if (difficultListModel.DifficultFlg4)
            {
                selectExtreme.SetActive(true);
                nonSelectExtreme.SetActive(false);
            }
            if (difficultListModel.DifficultFlg5)
            {
                selectChallenge.SetActive(true);
                nonSelectChallenge.SetActive(false);
            }
        }

        // ********************************************************************************
        // ��Փx��I��
        // ********************************************************************************
        public void SelectDifficult(int difficultNumber)
        {
            switch (difficultNumber)
            {
                case 1:
                    selectEasy.transform.localScale = nonSelectScale;
                    nonSelectEasy.transform.localScale = nonSelectScale;
                    selectNormal.transform.localScale = selectScale;
                    nonSelectNormal.transform.localScale = selectScale;
                    selectHard.transform.localScale = nonSelectScale;
                    nonSelectHard.transform.localScale = nonSelectScale;
                    selectExtreme.transform.localScale = nonSelectScale;
                    nonSelectExtreme.transform.localScale = nonSelectScale;
                    selectChallenge.transform.localScale = nonSelectScale;
                    nonSelectChallenge.transform.localScale = nonSelectScale;
                    break;
                case 2:
                    selectEasy.transform.localScale = nonSelectScale;
                    nonSelectEasy.transform.localScale = nonSelectScale;
                    selectNormal.transform.localScale = nonSelectScale;
                    nonSelectNormal.transform.localScale = nonSelectScale;
                    selectHard.transform.localScale = selectScale;
                    nonSelectHard.transform.localScale = selectScale;
                    selectExtreme.transform.localScale = nonSelectScale;
                    nonSelectExtreme.transform.localScale = nonSelectScale;
                    selectChallenge.transform.localScale = nonSelectScale;
                    nonSelectChallenge.transform.localScale = nonSelectScale;
                    break;
                case 3:
                    selectEasy.transform.localScale = selectScale;
                    nonSelectEasy.transform.localScale = selectScale;
                    selectNormal.transform.localScale = nonSelectScale;
                    nonSelectNormal.transform.localScale = nonSelectScale;
                    selectHard.transform.localScale = nonSelectScale;
                    nonSelectHard.transform.localScale = nonSelectScale;
                    selectExtreme.transform.localScale = nonSelectScale;
                    nonSelectExtreme.transform.localScale = nonSelectScale;
                    selectChallenge.transform.localScale = nonSelectScale;
                    nonSelectChallenge.transform.localScale = nonSelectScale;
                    break;
                case 4:
                    selectEasy.transform.localScale = nonSelectScale;
                    nonSelectEasy.transform.localScale = nonSelectScale;
                    selectNormal.transform.localScale = nonSelectScale;
                    nonSelectNormal.transform.localScale = nonSelectScale;
                    selectHard.transform.localScale = nonSelectScale;
                    nonSelectHard.transform.localScale = nonSelectScale;
                    selectExtreme.transform.localScale = selectScale;
                    nonSelectExtreme.transform.localScale = selectScale;
                    selectChallenge.transform.localScale = nonSelectScale;
                    nonSelectChallenge.transform.localScale = nonSelectScale;
                    break;
                case 5:
                    selectEasy.transform.localScale = nonSelectScale;
                    nonSelectEasy.transform.localScale = nonSelectScale;
                    selectNormal.transform.localScale = nonSelectScale;
                    nonSelectNormal.transform.localScale = nonSelectScale;
                    selectHard.transform.localScale = nonSelectScale;
                    nonSelectHard.transform.localScale = nonSelectScale;
                    selectExtreme.transform.localScale = nonSelectScale;
                    nonSelectExtreme.transform.localScale = nonSelectScale;
                    selectChallenge.transform.localScale = selectScale;
                    nonSelectChallenge.transform.localScale = selectScale;
                    break;
            }
        }
    }
}
