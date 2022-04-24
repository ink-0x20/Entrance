using UnityEngine;
using System.Collections.Generic;

namespace Entrance.Common
{
    [System.Serializable]
    public class SessionCommon
    {
        // **************************************************
        // �T�[�o�[���
        // **************************************************
        // �A�N�Z�X�L�[�̑��݂ɂ��T�[�o�[�ʐM�L���t���O
        private bool defaultLoadFlg = false;
        public bool DefaultLoadFlg { get => defaultLoadFlg; set => defaultLoadFlg = value; }
        // �I�u�W�F�N�gID
        private string objectId = null;
        public string ObjectId { get => objectId; set => objectId = value; }

        // **************************************************
        // ���C�u�V�X�e���ݒ�
        // **************************************************
        // �m�[�c�ړ����ԁi�m�[�c���x�ɂ���ăV�X�e�����ŉ��Z�j
        private float notesMovingTime = 0.0f;
        public float NotesMovingTime { get => notesMovingTime; }
        // �e�픻��̔��莞�ԁi�V�r�A���x���ɂ���ăV�X�e�����ŉ��Z�j
        private double perfectJudgeTime = 0.0;
        private double greatJudgeTime = 0.0;
        private double goodJudgeTime = 0.0;
        private double badJudgeTime = 0.0;
        private double missJudgeTime = 0.0;
        public double PerfectJudgeTime { get => perfectJudgeTime; }
        public double GreatJudgeTime { get => greatJudgeTime; }
        public double GoodJudgeTime { get => goodJudgeTime; }
        public double BadJudgeTime { get => badJudgeTime; }
        public double MissJudgeTime { get => missJudgeTime; }
        // �^�C�~���O�������ԁi�^�C�~���O�ɂ���ăV�X�e�����ŉ��Z�j
        private double timingTime = 0.0;
        public double TimingTime { get => timingTime; }

        // **************************************************
        // ���C�u���[�U�ݒ�
        // **************************************************
        // �m�[�c���x�i10�`120�����ےl��1�`12�j
        [SerializeField]
        private int notesSpeed = 10;
        public int NotesSpeed 
        {
            get => notesSpeed;
            set
            {
                // �m�[�c���x
                notesSpeed = value;
                SetNotesMovingTime();
            }
        }
        // �V�r�A���x���i1�`20�j
        [SerializeField]
        private int severeLevel = 1;
        public int SevereLevel
        {
            get => severeLevel;
            set
            {
                // �V�r�A���x��
                severeLevel = value;
                SetNotesJudgeTime();
            }
        }
        // �^�C�~���O�i+-20�j
        [SerializeField]
        private int timing = 0;
        public int Timing
        {
            get => timing;
            set
            {
                // �^�C�~���O
                timing = value;
                SetTimingTime();
            }
        }

        // �w�i�̖��邳�i50%�`100%�j
        [SerializeField]
        private int backgroundBrightness = 100;
        public int BackgroundBrightness { get => backgroundBrightness; set => backgroundBrightness = value <= 50 ? 50 : 100 <= value ? 100 : value; }
        // ���[���̕s�����x�i0%�`100%�j
        [SerializeField]
        private int laneOpacity = 100;
        public int LaneOpacity { get => laneOpacity; set => laneOpacity = value <= 0 ? 0 : 100 <= value ? 100 : value; }

        // ���C�uBGM���ʁi0�`1�j
        [SerializeField]
        private float liveVolumeBGM = 1.0f;
        public float LiveVolumeBGM { get => liveVolumeBGM; set => liveVolumeBGM = value <= 0.0f ? 0.0f : 1.0f <= value ? 1.0f : value; }
        // ���C�uSE���ʁi0�`1�j
        [SerializeField]
        private float liveVolumeSE = 1.0f;
        public float LiveVolumeSE { get => liveVolumeSE; set => liveVolumeSE = value <= 0.0f ? 0.0f : 1.0f <= value ? 1.0f : value; }
        // ���C�u�{�C�X���ʁi0�`1�j
        [SerializeField]
        private float liveVolumeVoice = 1.0f;
        public float LiveVolumeVoice { get => liveVolumeVoice; set => liveVolumeVoice = value <= 0.0f ? 0.0f : 1.0f <= value ? 1.0f : value; }
        // �V�X�e��BGM���ʁi0�`1�j
        [SerializeField]
        private float systemVolumeBGM = 1.0f;
        public float SystemVolumeBGM { get => systemVolumeBGM; set => systemVolumeBGM = value <= 0.0f ? 0.0f : 1.0f <= value ? 1.0f : value; }
        // �V�X�e��SE���ʁi0�`1�j
        [SerializeField]
        private float systemVolumeSE = 1.0f;
        public float SystemVolumeSE { get => systemVolumeSE; set => systemVolumeSE = value <= 0.0f ? 0.0f : 1.0f <= value ? 1.0f : value; }
        // �V�X�e���{�C�X���ʁi0�`1�j
        [SerializeField]
        private float systemVolumeVoice = 1.0f;
        public float SystemVolumeVoice { get => systemVolumeVoice; set => systemVolumeVoice = value <= 0.0f ? 0.0f : 1.0f <= value ? 1.0f : value; }

        // **************************************************
        // ���C�u�I�v�V����
        // **************************************************
        // �I�[�g���[�h
        [SerializeField]
        private bool autoMode = false;
        public bool AutoMode { get => autoMode; set => autoMode = value; }

        // �~���[���[�h
        [SerializeField]
        private bool mirrorMode = false;
        public bool MirrorMode { get => mirrorMode; set => mirrorMode = value; }

        // �J�b�g�C�����o
        // �X�L���E�����\��
        // �����������C��
        // �^�b�v���̐U��
        // MV���[�h�@�y�ʁE���i����
        // ��ʌŒ�
        // ���C�u�O�ݒ�m�F

        // **************************************************
        // �y�ȃf�[�^
        // **************************************************
        // �y�ȊǗ��ԍ�
        [SerializeField]
        private int musicNumber = 1;
        public int MusicNumber { get => musicNumber; set => musicNumber = value; }
        // �y�Ȗ�
        [SerializeField]
        private string musicName = null;
        public string MusicName { get => musicName; set => musicName = value; }
        // �J�e�S��
        [SerializeField]
        private CategoryType categoryType = CategoryType.ENTRANCE;
        public CategoryType CategoryType { get => categoryType; set => categoryType = value; }
        // ��Փx
        [SerializeField]
        private DifficultyType difficultyType = DifficultyType.EASY;
        public DifficultyType DifficultyType { get => difficultyType; set => difficultyType = value; }
        // ��Փx�ꗗ
        private Dictionary<string, int> musicLevelList;
        public Dictionary<string, int> MusicLevelList { get => musicLevelList; set => musicLevelList = value; }
        // ���C���A�C�R��
        private Sprite musicIcon;
        public Sprite MusicIcon { get => musicIcon; set => musicIcon = value; }

        // ���j�b�g���

        // **************************************************
        // ���C�u����
        // **************************************************
        // �X�R�A
        private float score = 0.0f;
        public float Score { get => score; set => score = value; }
        // �e����
        private int perfect = 0;
        public int Perfect { get => perfect; set => perfect = value; }
        private int great = 0;
        public int Great { get => great; set => great = value; }
        private int good = 0;
        public int Good { get => good; set => good = value; }
        private int bad = 0;
        public int Bad { get => bad; set => bad = value; }
        private int miss = 0;
        public int Miss { get => miss; set => miss = value; }
        // �R���{
        private int combo = 0;
        public int Combo { get => combo; set => combo = value; }
        // �ő�R���{
        private int maxCombo = 0;
        public int MaxCombo { get => maxCombo; set => maxCombo = value; }

        // �X�R�A����
        [SerializeField]
        private DictionaryJson<string, ScoreDetail> scoreHistory = new DictionaryJson<string, ScoreDetail>();
        public DictionaryJson<string, ScoreDetail> ScoreHistory { get => scoreHistory; }

        // ********************************************************************************
        // ���݃v���C�����X�R�A�������擾����
        // ********************************************************************************
        public ScoreDetail GetScoreHistory()
        {
            return GetScoreHistory(musicName, categoryType, difficultyType);
        }

        // ********************************************************************************
        // ���݂̍ō��X�R�A�������擾����
        // ********************************************************************************
        public ScoreDetail GetBestScoreHistory()
        {
            return GetBestScoreHistory(musicName, categoryType, difficultyType);
        }

        // ********************************************************************************
        // ����̃X�R�A�������擾����
        // ********************************************************************************
        public ScoreDetail GetScoreHistory(string musicName, CategoryType categoryType, DifficultyType difficultyType)
        {
            return scoreHistory[musicName + "_" + categoryType.ToString() + "_" + difficultyType.ToString()];
        }

        // ********************************************************************************
        // ����̍ō��X�R�A�������擾����
        // ********************************************************************************
        public ScoreDetail GetBestScoreHistory(string musicName, CategoryType categoryType, DifficultyType difficultyType)
        {
            string key = musicName + "_" + categoryType.ToString() + "_" + difficultyType.ToString() + "_Best";
            if (scoreHistory.ContainsKey(key))
            {
                return scoreHistory[key];
            }
            return new ScoreDetail(0, ScoreRank.NON, 0, 0, 0, 0, 0, 0, false, false);
        }

        // ********************************************************************************
        // �X�R�A������ǉ�����
        // ********************************************************************************
        public void AddScoreHistory(ScoreDetail scoreDetail)
        {
            string key = musicName + "_" + categoryType.ToString() + "_" + difficultyType.ToString();
            if (scoreHistory.ContainsKey(key))
            {
                // �X�V�i�㏑���j
                scoreHistory[key] = scoreDetail;
                if (scoreHistory[key + "_Best"].Score < scoreDetail.Score)
                {
                    scoreHistory[key + "_Best"] = scoreDetail;
                }
            }
            else
            {
                // �V�K�ǉ�
                scoreHistory.Add(key, scoreDetail);
                scoreHistory.Add(key + "_Best", scoreDetail);
            }
        }


        // ********************************************************************************
        // �Z�b�V�����ǂݍ��݌�̏������s��
        // ********************************************************************************
        public void OnLoaded()
        {
            SetNotesMovingTime();
            SetNotesJudgeTime();
            SetTimingTime();
        }

        // ********************************************************************************
        // �m�[�c�ړ����Ԃ����Z��(�x�[�X�l - �m�[�c�X�s�[�h) / �����l
        // ********************************************************************************
        private void SetNotesMovingTime()
        {
            notesMovingTime = (129 - notesSpeed) / 35.0f;
        }

        // ********************************************************************************
        // �m�[�c�̊e���莞�Ԃ����Z��(����� - ((���x�� - 1) * �����{��)) * ����
        // ********************************************************************************
        private void SetNotesJudgeTime()
        {
            double baseTime = 0.8 - ((severeLevel - 1) * 0.039);
            perfectJudgeTime = baseTime * 0.35;
            greatJudgeTime = baseTime * 0.7;
            goodJudgeTime = baseTime * 0.8;
            badJudgeTime = baseTime * 0.9;
            missJudgeTime = baseTime * 1;
        }

        // ********************************************************************************
        // �^�C�~���O�������Ԃ����Z
        // ********************************************************************************
        private void SetTimingTime()
        {
            timingTime = timing * 0.015;
        }
    }
}
