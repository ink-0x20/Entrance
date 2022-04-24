using UnityEngine;
using System.Collections.Generic;

using Entrance.Common;
using Entrance.DTO;
using Entrance.Utils;

using UniRx;

namespace Entrance.Model
{
    public class SelectMusicModel
    {
        // **************************************************
        // [UniRx�Ď��C�x���g]�I���y�ȏ��
        // **************************************************
        private readonly ReactiveProperty<SelectMusicInfo> musicInfo = new ReactiveProperty<SelectMusicInfo>();
        public IReadOnlyReactiveProperty<SelectMusicInfo> MusicInfo => musicInfo;

        // **************************************************
        // [UniRx�Ď��C�x���g]�y�ȊǗ��ԍ�
        // **************************************************
        private readonly ReactiveProperty<int> musicNumber = new ReactiveProperty<int>(0);
        public IReadOnlyReactiveProperty<int> MusicNumber => musicNumber;
        public void SelectMusic(int musicNumber)
        {
            if (musicNumber <= musicNameListLength && this.musicNumber.Value != musicNumber)
            {
                // �y�ȊǗ��ԍ���ݒ�
                this.musicNumber.Value = musicNumber;
                // �y�ȊǗ��ԍ�����y�Ȗ������
                musicName = musicNameList[musicNumber - 1];
                // BGM��؂�ւ�
                AudioUtils.PlayLiveMusic(musicClipList[musicName], liveVolume);
                // �X�R�A�����擾
                ScoreDetail scoreDetailEasy = GetBestScoreHistory(musicName, CategoryType.ENTRANCE, DifficultyType.EASY);
                ScoreDetail scoreDetailNormal = GetBestScoreHistory(musicName, CategoryType.ENTRANCE, DifficultyType.NORMAL);
                ScoreDetail scoreDetailHard = GetBestScoreHistory(musicName, CategoryType.ENTRANCE, DifficultyType.HARD);
                ScoreDetail scoreDetailExtreme = GetBestScoreHistory(musicName, CategoryType.ENTRANCE, DifficultyType.EXTREME);
                ScoreDetail scoreDetailChallenge = GetBestScoreHistory(musicName, CategoryType.ENTRANCE, DifficultyType.CHALLENGE);
                // �X�R�A�����N
                Dictionary<DifficultyType, ScoreRank> musicScoreRankList = new Dictionary<DifficultyType, ScoreRank>();
                musicScoreRankList.Add(DifficultyType.EASY, scoreDetailEasy.ScoreRank);
                musicScoreRankList.Add(DifficultyType.NORMAL, scoreDetailNormal.ScoreRank);
                musicScoreRankList.Add(DifficultyType.HARD, scoreDetailHard.ScoreRank);
                musicScoreRankList.Add(DifficultyType.EXTREME, scoreDetailExtreme.ScoreRank);
                musicScoreRankList.Add(DifficultyType.CHALLENGE, scoreDetailChallenge.ScoreRank);
                // �t���R���{
                Dictionary<DifficultyType, bool> musicFullComboList = new Dictionary<DifficultyType, bool>();
                musicFullComboList.Add(DifficultyType.EASY, scoreDetailEasy.IsFullCombo);
                musicFullComboList.Add(DifficultyType.NORMAL, scoreDetailNormal.IsFullCombo);
                musicFullComboList.Add(DifficultyType.HARD, scoreDetailHard.IsFullCombo);
                musicFullComboList.Add(DifficultyType.EXTREME, scoreDetailExtreme.IsFullCombo);
                musicFullComboList.Add(DifficultyType.CHALLENGE, scoreDetailChallenge.IsFullCombo);
                // �I���y�ȏ����X�V
                this.musicInfo.Value = new SelectMusicInfo(
                    musicNumber,
                    musicName,
                    musicIconList[musicName],
                    musicLevelList,
                    musicFullComboList,
                    musicScoreRankList);
            }
        }

        // **************************************************
        // [UniRx�Ď��C�x���g]�y�ȃ��X�g
        // **************************************************
        private readonly ReactiveProperty<MusicListModel> musicList = new ReactiveProperty<MusicListModel>();
        public IReadOnlyReactiveProperty<MusicListModel> MusicList => musicList;

        // **************************************************
        // �y�[�W�ԍ�
        // **************************************************
        private int maxPage = 0;
        private int page = 0;
        public void LeftPage()
        {
            if (0 < page)
            {
                page--;
            }
        }
        public void RightPage()
        {
            if (page < maxPage)
            {
                page++;
            }
        }

        // **************************************************
        // �y�ȏ��ێ�
        // **************************************************
        // �y�Ȗ�
        private string musicName = "";
        public string MusicName { get => musicName; }

        // ���C�u�y�ȉ���
        private float liveVolume = 0.0f;
        // �y�Ȑ�
        private int musicNameListLength = 0;
        // �y�Ȉꗗ
        private List<string> musicNameList = new List<string>();
        // �y�ȃA�C�R��
        private Dictionary<string, Sprite> musicIconList = new Dictionary<string, Sprite>();
        // �y�ȃN���b�v�ꗗ
        private Dictionary<string, AudioClip> musicClipList = new Dictionary<string, AudioClip>();
        // �y�ȓ�Փx�ꗗ
        private Dictionary<string, int> musicLevelList = new Dictionary<string, int>();
        // �X�R�A����
        private DictionaryJson<string, ScoreDetail> scoreHistory = new DictionaryJson<string, ScoreDetail>();

        // **************************************************
        // ���ʏ��
        // **************************************************

        // ********************************************************************************
        // ��������
        // ********************************************************************************
        public void Initialize(SessionCommon sessionCommon)
        {
            // ********************************************************************************
            // ���ʊǗ�
            // ********************************************************************************
            TextAsset[] musicList = Resources.LoadAll<TextAsset>("MusicData");
            int length = musicList.Length;
            // �y�ȃf�[�^�i�N���b�v�j�����ׂēǂݍ���
            for (int i = 0; i < length; i++)
            {
                // �L���b�V��
                TextAsset music = musicList[i];
                // �t�@�C��������y�ȏ����擾�i�Ȗ��j
                string musicNameDifficult = music.name.Substring(0,
                    music.name.LastIndexOf("_"));
                string musicName = musicNameDifficult.Substring(0,
                    musicNameDifficult.LastIndexOf("_"));
                // �y�ȃA�C�R���ǂݍ���
                if (!musicIconList.ContainsKey(musicName))
                {
                    musicIconList.Add(musicName, Resources.Load<Sprite>(ResourcesPathUtils.MusicIcon(musicName)));
                }
                // �y�ȃf�[�^�i�N���b�v�j�ǂݍ���
                if (!musicClipList.ContainsKey(musicName))
                {
                    musicClipList.Add(musicName, Resources.Load<AudioClip>(ResourcesPathUtils.Music(musicName)));
                }
                // �y�ȃ��X�g�ǉ�
                if (!musicNameList.Contains(musicName))
                {
                    musicNameList.Add(musicName);
                }
                // ��Փx
                MusicDTO.EditData musicData = JsonUtility.FromJson<MusicDTO.EditData>(music.ToString());
                musicLevelList.Add(music.name, musicData.level);
            }
            // �\���p�y��
            if (musicNameList.Count == 1)
            {
                this.musicList.Value = new MusicListModel(
                    musicIconList[musicNameList[0]],
                    null,
                    null,
                    null,
                    null
                    );
            }
            else if (musicClipList.Count == 2)
            {
                this.musicList.Value = new MusicListModel(
                    musicIconList[musicNameList[0]],
                    musicIconList[musicNameList[1]],
                    null,
                    null,
                    null
                    );
            }
            else if (musicClipList.Count == 3)
            {
                this.musicList.Value = new MusicListModel(
                    musicIconList[musicNameList[0]],
                    musicIconList[musicNameList[1]],
                    musicIconList[musicNameList[2]],
                    null,
                    null
                    );
            }
            else if (musicClipList.Count == 4)
            {
                this.musicList.Value = new MusicListModel(
                    musicIconList[musicNameList[0]],
                    musicIconList[musicNameList[1]],
                    musicIconList[musicNameList[2]],
                    musicIconList[musicNameList[3]],
                    null
                    );
            }
            else
            {
                this.musicList.Value = new MusicListModel(
                    musicIconList[musicNameList[0]],
                    musicIconList[musicNameList[1]],
                    musicIconList[musicNameList[2]],
                    musicIconList[musicNameList[3]],
                    musicIconList[musicNameList[4]]
                    );
            }
            // �y�Ȑ���ۑ�
            musicNameListLength = musicNameList.Count;
            // �ŏ��̋Ȃ�ێ�
            musicName = musicNameList[0];
            // �y�[�W�ő吔��ێ�
            maxPage = 0;
            // �X�R�A������ێ�
            scoreHistory = sessionCommon.ScoreHistory;
            // ���C�u�y�ȉ���
            liveVolume = sessionCommon.LiveVolumeBGM;
        }

        public void SetSelectedMusic(SessionCommon sessionCommon)
        {
            // ********************************************************************************
            // �ߋ��I���y�Ȃ��Z�b�g
            // ********************************************************************************
            // �y�ȊǗ��ԍ���ݒ�
            this.musicNumber.Value = sessionCommon.MusicNumber;
            // �y�ȊǗ��ԍ�����y�Ȗ������
            musicName = musicNameList[sessionCommon.MusicNumber - 1];
            // BGM��؂�ւ�
            AudioUtils.PlayLiveMusic(musicClipList[musicName], liveVolume);
            // �X�R�A�����擾
            ScoreDetail scoreDetailEasy = GetBestScoreHistory(musicName, CategoryType.ENTRANCE, DifficultyType.EASY);
            ScoreDetail scoreDetailNormal = GetBestScoreHistory(musicName, CategoryType.ENTRANCE, DifficultyType.NORMAL);
            ScoreDetail scoreDetailHard = GetBestScoreHistory(musicName, CategoryType.ENTRANCE, DifficultyType.HARD);
            ScoreDetail scoreDetailExtreme = GetBestScoreHistory(musicName, CategoryType.ENTRANCE, DifficultyType.EXTREME);
            ScoreDetail scoreDetailChallenge = GetBestScoreHistory(musicName, CategoryType.ENTRANCE, DifficultyType.CHALLENGE);
            // �X�R�A�����N
            Dictionary<DifficultyType, ScoreRank> musicScoreRankList = new Dictionary<DifficultyType, ScoreRank>();
            musicScoreRankList.Add(DifficultyType.EASY, scoreDetailEasy.ScoreRank);
            musicScoreRankList.Add(DifficultyType.NORMAL, scoreDetailNormal.ScoreRank);
            musicScoreRankList.Add(DifficultyType.HARD, scoreDetailHard.ScoreRank);
            musicScoreRankList.Add(DifficultyType.EXTREME, scoreDetailExtreme.ScoreRank);
            musicScoreRankList.Add(DifficultyType.CHALLENGE, scoreDetailChallenge.ScoreRank);
            // �t���R���{
            Dictionary<DifficultyType, bool> musicFullComboList = new Dictionary<DifficultyType, bool>();
            musicFullComboList.Add(DifficultyType.EASY, scoreDetailEasy.IsFullCombo);
            musicFullComboList.Add(DifficultyType.NORMAL, scoreDetailNormal.IsFullCombo);
            musicFullComboList.Add(DifficultyType.HARD, scoreDetailHard.IsFullCombo);
            musicFullComboList.Add(DifficultyType.EXTREME, scoreDetailExtreme.IsFullCombo);
            musicFullComboList.Add(DifficultyType.CHALLENGE, scoreDetailChallenge.IsFullCombo);
            // �I���y�ȏ����X�V
            this.musicInfo.Value = new SelectMusicInfo(
                sessionCommon.MusicNumber,
                musicName,
                musicIconList[musicName],
                musicLevelList,
                musicFullComboList,
                musicScoreRankList);
        }

        public void LinkageSession(SessionCommon sessionCommon)
        {
            // ��Փx�ꗗ
            sessionCommon.MusicLevelList = musicLevelList;
            // �A�C�R��
            sessionCommon.MusicIcon = musicIconList[musicName];
        }

        private ScoreDetail GetBestScoreHistory(string musicName, CategoryType categoryType, DifficultyType difficultyType)
        {
            string key = musicName + "_" + categoryType.ToString() + "_" + difficultyType.ToString() + "_Best";
            if (scoreHistory.ContainsKey(key))
            {
                return scoreHistory[key];
            }
            return new ScoreDetail(0, ScoreRank.NON, 0, 0, 0, 0, 0, 0, false, false);
        }
    }
}
