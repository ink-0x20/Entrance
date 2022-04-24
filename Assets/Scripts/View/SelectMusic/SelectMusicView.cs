using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

using Entrance.Common;
using Entrance.Model;
using Entrance.Utils;

namespace Entrance.View
{
    public class SelectMusicView : MonoBehaviour
    {
        // **************************************************
        // �C���X�y�N�^�I�u�W�F�N�g
        // **************************************************
        // ���C���y�ȃA�C�R��
        [SerializeField]
        private Image mainMusicImage = default;
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
        // �y�ȑI���I�u�W�F�N�g
        [SerializeField]
        private GameObject musicIconObject1 = default;
        [SerializeField]
        private GameObject musicIconObject2 = default;
        [SerializeField]
        private GameObject musicIconObject3 = default;
        [SerializeField]
        private GameObject musicIconObject4 = default;
        [SerializeField]
        private GameObject musicIconObject5 = default;
        // �y�ȑI���A�C�R��
        [SerializeField]
        private Image musicIcon1 = default;
        [SerializeField]
        private Image musicIcon2 = default;
        [SerializeField]
        private Image musicIcon3 = default;
        [SerializeField]
        private Image musicIcon4 = default;
        [SerializeField]
        private Image musicIcon5 = default;

        // **************************************************
        // �C�x���g���X�i�[��`
        // **************************************************
        // �y�ȑI���C�x���g
        public Action selectMusicListener1;
        public Action selectMusicListener2;
        public Action selectMusicListener3;
        public Action selectMusicListener4;
        public Action selectMusicListener5;
        // �y�ȃ��X�g�؂�ւ��C�x���g
        public Action leftPageListener;
        public Action rightPageListener;
        public Action musicTypeListener;
        public Action sortMusicListener;
        // �y�Ȍ���C�x���g
        public Action decideListener;

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
        public void Initialize()
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
            if (KeyboardPressUtils.All1())
            {
                SystemUtils.SafeCall(leftPageListener);
            }
            if (KeyboardPressUtils.All2())
            {
                SystemUtils.SafeCall(musicTypeListener);
            }
            if (KeyboardPressUtils.All3())
            {
                SystemUtils.SafeCall(selectMusicListener5);
            }
            if (KeyboardPressUtils.All4())
            {
                SystemUtils.SafeCall(selectMusicListener3);
            }
            if (KeyboardPressUtils.All5())
            {
                SystemUtils.SafeCall(selectMusicListener1);
            }
            if (KeyboardPressUtils.All6())
            {
                SystemUtils.SafeCall(selectMusicListener2);
            }
            if (KeyboardPressUtils.All7())
            {
                SystemUtils.SafeCall(selectMusicListener4);
            }
            if (KeyboardPressUtils.All8())
            {
                SystemUtils.SafeCall(sortMusicListener);
            }
            if (KeyboardPressUtils.All9())
            {
                SystemUtils.SafeCall(rightPageListener);
            }
            if (KeyboardPressUtils.Enter())
            {
                SystemUtils.SafeCall(decideListener);
            }
        }

        // ********************************************************************************
        // �y�ȏ����Z�b�g����
        // ********************************************************************************
        public void SelectMusic(SelectMusicInfo musicInfo)
        {
            // ********************************************************************************
            // ���C���A�C�R��
            // ********************************************************************************
            // �A�C�R���ύX
            mainMusicImage.sprite = musicInfo.MusicIcon;
            // �y�Ȗ��ύX
            mainMusicName.text = musicInfo.MusicName;

            // ********************************************************************************
            // ��Փx�\��
            // ********************************************************************************
            // ��Փx�擾�L�[
            string keyEasy = musicInfo.MusicName + "_" + CategoryType.ENTRANCE + "_" + DifficultyType.EASY;
            string keyNormal = musicInfo.MusicName + "_" + CategoryType.ENTRANCE + "_" + DifficultyType.NORMAL;
            string keyHard = musicInfo.MusicName + "_" + CategoryType.ENTRANCE + "_" + DifficultyType.HARD;
            string keyExtreme = musicInfo.MusicName + "_" + CategoryType.ENTRANCE + "_" + DifficultyType.EXTREME;
            string keyChallenge = musicInfo.MusicName + "_" + CategoryType.ENTRANCE + "_" + DifficultyType.CHALLENGE;
            // ��Փx�擾
            musicInfo.MusicLevelList.TryGetValue(keyEasy, out int levelEasy);
            musicInfo.MusicLevelList.TryGetValue(keyNormal, out int levelNormal);
            musicInfo.MusicLevelList.TryGetValue(keyHard, out int levelHard);
            musicInfo.MusicLevelList.TryGetValue(keyExtreme, out int levelExtreme);
            musicInfo.MusicLevelList.TryGetValue(keyChallenge, out int levelChallenge);
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
                // �t���R���{�\��
                difficultObject[DifficultyType.EASY.ToString() + "_FullCombo"].SetActive(
                    musicInfo.IsFullComboList[DifficultyType.EASY]);
                difficultObject[DifficultyType.NORMAL.ToString() + "_FullCombo"].SetActive(
                    musicInfo.IsFullComboList[DifficultyType.NORMAL]);
                difficultObject[DifficultyType.HARD.ToString() + "_FullCombo"].SetActive(
                    musicInfo.IsFullComboList[DifficultyType.HARD]);
                difficultObject[DifficultyType.EXTREME.ToString() + "_FullCombo"].SetActive(
                    musicInfo.IsFullComboList[DifficultyType.EXTREME]);
                difficultObject[DifficultyType.CHALLENGE.ToString() + "_FullCombo"].SetActive(
                    musicInfo.IsFullComboList[DifficultyType.CHALLENGE]);
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
                if (musicInfo.MusicScoreRankList[DifficultyType.EASY] != ScoreRank.NON)
                {
                    difficultObject[DifficultyType.EASY.ToString() + "_" + musicInfo.MusicScoreRankList[DifficultyType.EASY].ToString()].SetActive(true);
                }
                if (musicInfo.MusicScoreRankList[DifficultyType.NORMAL] != ScoreRank.NON)
                {
                    difficultObject[DifficultyType.NORMAL.ToString() + "_" + musicInfo.MusicScoreRankList[DifficultyType.NORMAL].ToString()].SetActive(true);
                }
                if (musicInfo.MusicScoreRankList[DifficultyType.HARD] != ScoreRank.NON)
                {
                    difficultObject[DifficultyType.HARD.ToString() + "_" + musicInfo.MusicScoreRankList[DifficultyType.HARD].ToString()].SetActive(true);
                }
                if (musicInfo.MusicScoreRankList[DifficultyType.EXTREME] != ScoreRank.NON)
                {
                    difficultObject[DifficultyType.EXTREME.ToString() + "_" + musicInfo.MusicScoreRankList[DifficultyType.EXTREME].ToString()].SetActive(true);
                }
                if (musicInfo.MusicScoreRankList[DifficultyType.CHALLENGE] != ScoreRank.NON)
                {
                    difficultObject[DifficultyType.CHALLENGE.ToString() + "_" + musicInfo.MusicScoreRankList[DifficultyType.CHALLENGE].ToString()].SetActive(true);
                }
            }

            // ********************************************************************************
            // �y�ȑI���A�C�R��
            // ********************************************************************************
            // �I�����Ă���y�Ȃ̑傫����ύX
            switch (musicInfo.MusicNumber)
            {
                case 1:
                    musicIconObject1.transform.localScale = selectScale;
                    musicIconObject2.transform.localScale = nonSelectScale;
                    musicIconObject3.transform.localScale = nonSelectScale;
                    musicIconObject4.transform.localScale = nonSelectScale;
                    musicIconObject5.transform.localScale = nonSelectScale;
                    break;
                case 2:
                    musicIconObject1.transform.localScale = nonSelectScale;
                    musicIconObject2.transform.localScale = selectScale;
                    musicIconObject3.transform.localScale = nonSelectScale;
                    musicIconObject4.transform.localScale = nonSelectScale;
                    musicIconObject5.transform.localScale = nonSelectScale;
                    break;
                case 3:
                    musicIconObject1.transform.localScale = nonSelectScale;
                    musicIconObject2.transform.localScale = nonSelectScale;
                    musicIconObject3.transform.localScale = selectScale;
                    musicIconObject4.transform.localScale = nonSelectScale;
                    musicIconObject5.transform.localScale = nonSelectScale;
                    break;
                case 4:
                    musicIconObject1.transform.localScale = nonSelectScale;
                    musicIconObject2.transform.localScale = nonSelectScale;
                    musicIconObject3.transform.localScale = nonSelectScale;
                    musicIconObject4.transform.localScale = selectScale;
                    musicIconObject5.transform.localScale = nonSelectScale;
                    break;
                case 5:
                    musicIconObject1.transform.localScale = nonSelectScale;
                    musicIconObject2.transform.localScale = nonSelectScale;
                    musicIconObject3.transform.localScale = nonSelectScale;
                    musicIconObject4.transform.localScale = nonSelectScale;
                    musicIconObject5.transform.localScale = selectScale;
                    break;
            }
        }

        // ********************************************************************************
        // �y�ȃ��X�g���X�V
        // ********************************************************************************
        public void SetMusicList(MusicListModel musicListModel)
        {
            // ********************************************************************************
            // �y�ȃC���[�W��K�p
            // ********************************************************************************
            if (musicListModel.MusicIcon1 != null)
            {
                musicIconObject1.SetActive(true);
                musicIcon1.sprite = musicListModel.MusicIcon1;
            }
            if (musicListModel.MusicIcon2 != null)
            {
                musicIconObject2.SetActive(true);
                musicIcon2.sprite = musicListModel.MusicIcon2;
            }
            if (musicListModel.MusicIcon3 != null)
            {
                musicIconObject3.SetActive(true);
                musicIcon3.sprite = musicListModel.MusicIcon3;
            }
            if (musicListModel.MusicIcon4 != null)
            {
                musicIconObject4.SetActive(true);
                musicIcon4.sprite = musicListModel.MusicIcon4;
            }
            if (musicListModel.MusicIcon5 != null)
            {
                musicIconObject5.SetActive(true);
                musicIcon5.sprite = musicListModel.MusicIcon5;
            }
        }
    }
}
