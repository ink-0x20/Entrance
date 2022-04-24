using System;
using UnityEngine;

using Entrance.Common;
using Entrance.Utils;

using UniRx;

namespace Entrance.Model
{
    public class SelectDifficultModel
    {
        // **************************************************
        // [UniRx�Ď��C�x���g]��Փx�I��
        // **************************************************
        // ��Փx�Ǘ��ԍ�
        private readonly ReactiveProperty<int> difficultNumber = new ReactiveProperty<int>(0);
        public IReadOnlyReactiveProperty<int> DifficultNumber => difficultNumber;
        public void SelectDifficult(int difficultNumber)
        {
            if (difficultList.Value.DifficultFlg1 && difficultNumber == 1)
            {
                this.difficultNumber.Value = difficultNumber;
                difficultyType = DifficultyType.NORMAL;
            }
            else if (difficultList.Value.DifficultFlg2 && difficultNumber == 2)
            {
                this.difficultNumber.Value = difficultNumber;
                difficultyType = DifficultyType.HARD;
            }
            else if (difficultList.Value.DifficultFlg3 && difficultNumber == 3)
            {
                this.difficultNumber.Value = difficultNumber;
                difficultyType = DifficultyType.EASY;
            }
            else if (difficultList.Value.DifficultFlg4 && difficultNumber == 4)
            {
                this.difficultNumber.Value = difficultNumber;
                difficultyType = DifficultyType.EXTREME;
            }
            else if (difficultList.Value.DifficultFlg5 && difficultNumber == 5)
            {
                this.difficultNumber.Value = difficultNumber;
                difficultyType = DifficultyType.CHALLENGE;
            }
        }

        // **************************************************
        // [UniRx�Ď��C�x���g]�y�ȑI��
        // **************************************************
        // �y�ȃ��X�g
        private readonly ReactiveProperty<DifficultListModel> difficultList = new ReactiveProperty<DifficultListModel>();
        public IReadOnlyReactiveProperty<DifficultListModel> DifficultList => difficultList;

        // **************************************************
        // ��Փx�ێ�
        // **************************************************
        private DifficultyType difficultyType;
        public DifficultyType DifficultyType { get => difficultyType; }

        // ********************************************************************************
        // ��������
        // ********************************************************************************
        public void Initialize(SessionCommon sessionCommon)
        {
            // ********************************************************************************
            // ���ʊǗ�
            // ********************************************************************************
            TextAsset[] musicList = Resources.LoadAll<TextAsset>("MusicData");
            bool difficultFlg1 = false;
            bool difficultFlg2 = false;
            bool difficultFlg3 = false;
            bool difficultFlg4 = false;
            bool difficultFlg5 = false;
            int length = musicList.Length;
            for (int i = 0; i < length; i++)
            {
                // �L���b�V��
                TextAsset music = musicList[i];
                // �t�@�C��������y�ȏ����擾�i�J�e�S���E��Փx�j
                string musicName = music.name.Substring(0,
                    music.name.LastIndexOf("_", music.name.LastIndexOf("_") - 1)
                    );
                string musicInfo = music.name.Substring(
                    music.name.LastIndexOf("_", music.name.LastIndexOf("_") - 1) + 1
                    );
                if (music.name.Contains(sessionCommon.MusicName))
                {
                    DifficultyType difficultType = (DifficultyType)Enum.Parse(typeof(DifficultyType), music.name.Substring(music.name.LastIndexOf("_") + 1));
                    switch (difficultType)
                    {
                        case DifficultyType.EASY:
                            difficultFlg3 = true;
                            break;
                        case DifficultyType.NORMAL:
                            difficultFlg1 = true;
                            break;
                        case DifficultyType.HARD:
                            difficultFlg2 = true;
                            break;
                        case DifficultyType.EXTREME:
                            difficultFlg4 = true;
                            break;
                        case DifficultyType.CHALLENGE:
                            difficultFlg5 = true;
                            break;
                    }
                }
            }
            if (difficultFlg3 && difficultNumber.Value == 0)
            {
                difficultNumber.Value = 3;
                difficultyType = DifficultyType.EASY;
            }
            else if (difficultFlg1 && difficultNumber.Value == 0)
            {
                difficultNumber.Value = 1;
                difficultyType = DifficultyType.NORMAL;
            }
            else if (difficultFlg2 && difficultNumber.Value == 0)
            {
                difficultNumber.Value = 2;
                difficultyType = DifficultyType.HARD;
            }
            else if (difficultFlg4 && difficultNumber.Value == 0)
            {
                difficultNumber.Value = 4;
                difficultyType = DifficultyType.EXTREME;
            }
            else if (difficultFlg5 && difficultNumber.Value == 0)
            {
                difficultNumber.Value = 5;
                difficultyType = DifficultyType.CHALLENGE;
            }
            DifficultListModel difficultListModel = new DifficultListModel(
                  difficultFlg1
                , difficultFlg2
                , difficultFlg3
                , difficultFlg4
                , difficultFlg5
                );
            difficultList.Value = difficultListModel;
        }
    }
}
