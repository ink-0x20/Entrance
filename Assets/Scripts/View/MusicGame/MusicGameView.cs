using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

using Entrance.Common;
using Entrance.DTO;
using Entrance.Model;
using Entrance.Utils;

using TMPro;

namespace Entrance.View
{
    public class MusicGameView : MonoBehaviour
    {
        // **************************************************
        // �C���X�y�N�^�I�u�W�F�N�g
        // **************************************************
        // UI�L�����o�X
        [SerializeField]
        private GameObject uiCanvasObject = default;
        // �X�R�A�o�[
        [SerializeField]
        private Slider scoreSlider = default;
        [SerializeField]
        private Image scoreSliderImage = default;
        // �X�R�A�o�[�F
        [SerializeField]
        private Color scoreColorD = default;
        [SerializeField]
        private Color scoreColorC = default;
        [SerializeField]
        private Color scoreColorB = default;
        [SerializeField]
        private Color scoreColorA = default;
        [SerializeField]
        private Color scoreColorS = default;
        // �X�R�A�e�L�X�g
        [SerializeField]
        private Text scoreText = default;
        // �x�[�X�m�[�c
        [SerializeField]
        private GameObject baseNotes0 = default;
        // �R���{
        [SerializeField]
        private GameObject comboNumberObject = default;
        [SerializeField]
        private TMP_Text comboNumber = default;
        [SerializeField]
        private GameObject comboTextObject = default;
        // �m�[�c�v���n�u
        [SerializeField]
        private GameObject longNotesPrefab = default;
        [SerializeField]
        private GameObject longNotesDouble1Prefab = default;
        [SerializeField]
        private GameObject longNotesDouble2Prefab = default;
        [SerializeField]
        private GameObject longNotesLinePrefab = default;
        [SerializeField]
        private GameObject normalNotesPrefab = default;
        [SerializeField]
        private GameObject normalNotesDouble1Prefab = default;
        [SerializeField]
        private GameObject normalNotesDouble1FeverPrefab = default;
        [SerializeField]
        private GameObject normalNotesDouble1Special1Prefab = default;
        [SerializeField]
        private GameObject normalNotesDouble2Prefab = default;
        [SerializeField]
        private GameObject normalNotesDouble2FeverPrefab = default;
        [SerializeField]
        private GameObject normalNotesDouble2Special1Prefab = default;
        [SerializeField]
        private GameObject normalNotesFeverPrefab = default;
        [SerializeField]
        private GameObject normalNotesSpecial1Prefab = default;

        // **************************************************
        // �ϐ��錾
        // **************************************************
        // �x�[�X�m�[�c
        private Vector3 basePosition;

        // **************************************************
        // �C�x���g���X�i�[��`
        // **************************************************
        // ���胊�X�i�[
        public Action perfectListener;
        public Action greatListener;
        public Action goodListener;
        public Action badListener;
        public Action missListener;
        // �R���{���X�i�[
        public Action comboPlusListener;
        public Action comboResetListener;
        // �X�R�A���X�i�[
        public Action normalScoreListener;
        public Action feverScoreListener;
        // SE
        public Action normalNotesSE;
        // ���U���g��ʑJ��
        public Action resultScene;

        // **************************************************
        // ����e�L�X�g�z��
        // **************************************************
        // �e���[�����Ƃ̔���e�L�X�g�\���R���[�`���z��
        private Coroutine[] textCoroutine;
        // �e���[�����Ƃ̊e����e�L�X�g�I�u�W�F�N�g�z��
        private GameObject[] perfectText;
        private GameObject[] greatText;
        private GameObject[] goodText;
        private GameObject[] badText;
        private GameObject[] missText;

        // **************************************************
        // �R���{�e�L�X�g
        // **************************************************
        // �R���{�e�L�X�g�\���R���[�`��
        private Coroutine comboCoroutine;

        // **************************************************
        // ���ʏ�񃊃X�g
        // **************************************************
        private List<MusicData> musicDataList = new List<MusicData>();

        // **************************************************
        // �t�B�[�o�[�^�C��
        // **************************************************
        private GameObject[] nonFeverObject;
        private GameObject[] feverObject;
        private int feverCount = 0;
        private bool feverFlg = false;
        public float maxScore = 0.0f;
        public int maxCombo = 0;

        // **************************************************
        // ���U���g���
        // **************************************************
        private double sceneTime;

        // ********************************************************************************
        // ��������
        // ********************************************************************************
        public void Initialize(SessionCommon sessionCommon)
        {
            // ********************************************************************************
            // ���ʃf�[�^���擾���A�m�[�c�����̏���
            // ********************************************************************************
            MusicDTO.EditData musicData = JsonUtility.FromJson<MusicDTO.EditData>(Resources.Load<TextAsset>(ResourcesPathUtils.MusicData(sessionCommon)).ToString());
            float bpm = musicData.BPM;
            // ����offset�����Ԋ��Z
            double offset = musicData.offset / 80000.0;
            int maxLane = musicData.maxLane;
            // ���[���̐��ɂ��A�e�z�񏉊���
            float[] xMoveDistanceArray = new float[maxLane];
            float[] yMoveDistanceArray = new float[maxLane];
            float[] zMoveDistanceArray = new float[maxLane];
            float[] xJudgeNotesPosition = new float[maxLane];
            float[] yJudgeNotesPosition = new float[maxLane];
            float[] zJudgeNotesPosition = new float[maxLane];
            textCoroutine = new Coroutine[maxLane];
            perfectText = new GameObject[maxLane];
            greatText = new GameObject[maxLane];
            goodText = new GameObject[maxLane];
            badText = new GameObject[maxLane];
            missText = new GameObject[maxLane];
            // �x�[�X�|�W�V���������擾
            float xBasePosition = baseNotes0.transform.position.x;
            float yBasePosition = baseNotes0.transform.position.y;
            float zBasePosition = baseNotes0.transform.position.z;
            basePosition = baseNotes0.transform.position;
            // ����m�[�c�����[�v
            for (int i = 0; i < maxLane; i++)
            {
                // ����e�L�X�g���������L���b�V��
                GameObject judgeNotes = GameObject.Find("JudgeNotes" + i);
                xJudgeNotesPosition[i] = judgeNotes.transform.position.x;
                yJudgeNotesPosition[i] = judgeNotes.transform.position.y;
                zJudgeNotesPosition[i] = judgeNotes.transform.position.z;
                perfectText[i] = judgeNotes.transform.Find("Perfect").gameObject;
                greatText[i] = judgeNotes.transform.Find("Great").gameObject;
                goodText[i] = judgeNotes.transform.Find("Good").gameObject;
                badText[i] = judgeNotes.transform.Find("Bad").gameObject;
                missText[i] = judgeNotes.transform.Find("Miss").gameObject;
                // ����m�[�c�����擾���A�ړ��������Z
                xMoveDistanceArray[i] = -(xBasePosition - judgeNotes.transform.position.x);
                yMoveDistanceArray[i] = -(yBasePosition - judgeNotes.transform.position.y);
                zMoveDistanceArray[i] = 0;
            }
            // �����O�m�[�c���C�������J���[�𐶐��i�����ۂ��F�j
            Color initLongLineColor = new Color(1f, 1f, 1f, 0.6f);
            // �m�[�c���莞��
            double miss = sessionCommon.MissJudgeTime;
            double bad = sessionCommon.BadJudgeTime;
            double good = sessionCommon.GoodJudgeTime;
            double great = sessionCommon.GreatJudgeTime;
            double perfect = sessionCommon.PerfectJudgeTime;

            // ********************************************************************************
            // ���ʉ��
            // ********************************************************************************
            // �L���b�V��
            int length = musicData.notes.Count;
            // �����������胊�X�g
            List<string> tmpNotesList = new List<string>();
            List<string> doubleNotesList = new List<string>();
            for (int i = 0; i < length; i++)
            {
                // �L���b�V��
                MusicDTO.Note notes = musicData.notes[i];
                // ���C���m�[�c�̃^�C�~���O��ۑ�
                string nowDoubleNotes = notes.LPB.ToString() + "|" + notes.num.ToString();
                if (tmpNotesList.Contains(nowDoubleNotes))
                {
                    if (!doubleNotesList.Contains(nowDoubleNotes))
                    {
                        doubleNotesList.Add(nowDoubleNotes);
                    }
                }
                else
                {
                    tmpNotesList.Add(nowDoubleNotes);
                }
                // �X�R�A�v�Z
                if (notes.special == 2)
                {
                    maxScore += 950.0f;
                }
                else
                {
                    maxScore += 420.0f;
                }
                // �R���{�v�Z
                maxCombo++;
                int longNotesCount = notes.notes.Count;
                for (int j = 0; j < longNotesCount; j++)
                {
                    nowDoubleNotes = notes.notes[j].LPB.ToString() + "|" + notes.notes[j].num.ToString();
                    if (tmpNotesList.Contains(nowDoubleNotes))
                    {
                        if (!doubleNotesList.Contains(nowDoubleNotes))
                        {
                            doubleNotesList.Add(nowDoubleNotes);
                        }
                    }
                    else
                    {
                        tmpNotesList.Add(nowDoubleNotes);
                    }
                    // �X�R�A�v�Z
                    if (notes.special == 2)
                    {
                        maxScore += 950.0f;
                    }
                    else
                    {
                        maxScore += 420.0f;
                    }
                    // �R���{�v�Z
                    maxCombo++;
                }
            }
            // ���������؂�ւ��t���O�itrue=1�F, false=2�I�����W�F�j
            bool doubleTapFlg = false;
            Dictionary<string, bool> doubleTapColorMap = new Dictionary<string, bool>();
            int doubleNotesListLength = doubleNotesList.Count;
            for (int i = 0; i < doubleNotesListLength; i++)
            {
                doubleTapFlg = !doubleTapFlg;
                doubleTapColorMap.Add(doubleNotesList[i], doubleTapFlg);
            }

            // ********************************************************************************
            // ���ʂ��m�[�c�f�[�^�֕ϊ�
            // ********************************************************************************
            for (int i = 0; i < length; i++)
            {
                // ********************************************************************************
                // ���ʃf�[�^�L���b�V��
                // ********************************************************************************
                MusicDTO.Note notes = musicData.notes[i];

                // ********************************************************************************
                // ���ʌv�Z
                // ********************************************************************************
                // ���C���m�[�c
                GameObject mainNotes;
                // �t�B�[�o�[�m�[�c
                GameObject feverNotes = null;
                // �m�[�c��������
                double generateTime = 60 / bpm / notes.LPB * notes.num + offset;
                // �����O�m�[�c��
                int longNotesCount = notes.notes.Count;
                // �����O�m�[�c�z�񏉊���
                int[] lanes = new int[longNotesCount];
                float[] xSpeeds = new float[longNotesCount];
                float[] ySpeeds = new float[longNotesCount];
                float[] zSpeeds = new float[longNotesCount];
                float[] xBasePositions = new float[longNotesCount];
                float[] yBasePositions = new float[longNotesCount];
                float[] zBasePositions = new float[longNotesCount];
                float[] xNotesLineBasePositions = new float[longNotesCount];
                float[] yNotesLineBasePositions = new float[longNotesCount];
                float[] zNotesLineBasePositions = new float[longNotesCount];
                Vector3[] judgePositions = new Vector3[longNotesCount];
                Vector3[] notesLineStartJudgeNotesPositions = new Vector3[longNotesCount];
                Vector3[] notesLineEndBaseNotesPositions = new Vector3[longNotesCount];
                double[] generateTimes = new double[longNotesCount];
                double[] fromMissTimes = new double[longNotesCount];
                double[] fromBadTimes = new double[longNotesCount];
                double[] fromGoodTimes = new double[longNotesCount];
                double[] fromGreatTimes = new double[longNotesCount];
                double[] fromPerfectTimes = new double[longNotesCount];
                double[] toPerfectTimes = new double[longNotesCount];
                double[] toGreatTimes = new double[longNotesCount];
                double[] toGoodTimes = new double[longNotesCount];
                double[] toBadTimes = new double[longNotesCount];
                double[] toMissTimes = new double[longNotesCount];
                int[] specials = new int[longNotesCount];
                GameObject[] notesObjects = new GameObject[longNotesCount];
                GameObject[] feverNotesObjects = new GameObject[longNotesCount];
                GameObject[] notesLineObjects = new GameObject[longNotesCount];
                LineRenderer[] notesLines = new LineRenderer[longNotesCount];
                bool[] unjudgedFlgs = new bool[longNotesCount];

                switch ((NotesType)notes.type)
                {
                    case NotesType.Single:
                        // ���C���m�[�c�𐶐�
                        if (doubleTapColorMap.ContainsKey(notes.LPB.ToString() + "|" + notes.num.ToString()))
                        {
                            // ��������
                            if (doubleTapColorMap[notes.LPB.ToString() + "|" + notes.num.ToString()])
                            {
                                // ��������1
                                if (notes.special == 2)
                                {
                                    // �t�B�[�o�[���m�[�c
                                    feverNotes = Instantiate(normalNotesDouble1FeverPrefab, basePosition, Quaternion.identity, uiCanvasObject.transform);
                                }
                                if (notes.special == 1)
                                {
                                    // ���C���t�B�[�o�[������m�[�c
                                    mainNotes = Instantiate(normalNotesDouble1Special1Prefab, basePosition, Quaternion.identity, uiCanvasObject.transform);
                                }
                                else
                                {
                                    // ���C���ʏ�m�[�c
                                    mainNotes = Instantiate(normalNotesDouble1Prefab, basePosition, Quaternion.identity, uiCanvasObject.transform);
                                }
                            }
                            else
                            {
                                // ��������2
                                if (notes.special == 2)
                                {
                                    // �t�B�[�o�[���m�[�c
                                    feverNotes = Instantiate(normalNotesDouble2FeverPrefab, basePosition, Quaternion.identity, uiCanvasObject.transform);
                                }
                                if (notes.special == 1)
                                {
                                    // ���C���t�B�[�o�[������m�[�c
                                    mainNotes = Instantiate(normalNotesDouble2Special1Prefab, basePosition, Quaternion.identity, uiCanvasObject.transform);
                                }
                                else
                                {
                                    // ���C���ʏ�m�[�c
                                    mainNotes = Instantiate(normalNotesDouble2Prefab, basePosition, Quaternion.identity, uiCanvasObject.transform);
                                }
                            }
                        }
                        else
                        {
                            // �P��
                            if (notes.special == 2)
                            {
                                // �t�B�[�o�[���m�[�c
                                feverNotes = Instantiate(normalNotesFeverPrefab, basePosition, Quaternion.identity, uiCanvasObject.transform);
                            }
                            if (notes.special == 1)
                            {
                                // ���C���t�B�[�o�[������m�[�c
                                mainNotes = Instantiate(normalNotesSpecial1Prefab, basePosition, Quaternion.identity, uiCanvasObject.transform);
                            }
                            else
                            {
                                // ���C���ʏ�m�[�c
                                mainNotes = Instantiate(normalNotesPrefab, basePosition, Quaternion.identity, uiCanvasObject.transform);
                            }
                        }
                        // ���X�g�ɕۑ�
                        musicDataList.Add(new MusicData(
                              notes.lane
                            , xMoveDistanceArray[notes.lane] / sessionCommon.NotesMovingTime
                            , yMoveDistanceArray[notes.lane] / sessionCommon.NotesMovingTime
                            , 0
                            , xBasePosition
                            , yBasePosition
                            , zBasePosition
                            , xBasePosition - 3
                            , yBasePosition
                            , zBasePosition
                            , new Vector3(xJudgeNotesPosition[notes.lane], yJudgeNotesPosition[notes.lane], zJudgeNotesPosition[notes.lane])
                            , new Vector3(xJudgeNotesPosition[notes.lane] - 3, yJudgeNotesPosition[notes.lane], zJudgeNotesPosition[notes.lane])
                            , generateTime
                            , generateTime + sessionCommon.NotesMovingTime - miss
                            , generateTime + sessionCommon.NotesMovingTime - bad
                            , generateTime + sessionCommon.NotesMovingTime - good
                            , generateTime + sessionCommon.NotesMovingTime - great
                            , generateTime + sessionCommon.NotesMovingTime - perfect
                            , generateTime + sessionCommon.NotesMovingTime + perfect
                            , generateTime + sessionCommon.NotesMovingTime + great
                            , generateTime + sessionCommon.NotesMovingTime + good
                            , generateTime + sessionCommon.NotesMovingTime + bad
                            , generateTime + sessionCommon.NotesMovingTime + miss
                            , notes.special
                            , mainNotes
                            , feverNotes
                            ));
                        break;
                    case NotesType.StraightLineLong:
                        // ���C���m�[�c�𐶐�
                        if (doubleTapColorMap.ContainsKey(notes.LPB.ToString() + "|" + notes.num.ToString()))
                        {
                            // ��������
                            if (doubleTapColorMap[notes.LPB.ToString() + "|" + notes.num.ToString()])
                            {
                                // ��������1
                                if (notes.special == 2)
                                {
                                    // �t�B�[�o�[���m�[�c
                                    feverNotes = Instantiate(normalNotesDouble1FeverPrefab, basePosition, Quaternion.identity, uiCanvasObject.transform);
                                }
                                if (notes.special == 1)
                                {
                                    // ���C���t�B�[�o�[������m�[�c
                                    mainNotes = Instantiate(normalNotesDouble1Special1Prefab, basePosition, Quaternion.identity, uiCanvasObject.transform);
                                }
                                else
                                {
                                    // ���C���ʏ�m�[�c
                                    mainNotes = Instantiate(normalNotesDouble1Prefab, basePosition, Quaternion.identity, uiCanvasObject.transform);
                                }
                            }
                            else
                            {
                                // ��������2
                                if (notes.special == 2)
                                {
                                    // �t�B�[�o�[���m�[�c
                                    feverNotes = Instantiate(normalNotesDouble2FeverPrefab, basePosition, Quaternion.identity, uiCanvasObject.transform);
                                }
                                if (notes.special == 1)
                                {
                                    // ���C���t�B�[�o�[������m�[�c
                                    mainNotes = Instantiate(normalNotesDouble2Special1Prefab, basePosition, Quaternion.identity, uiCanvasObject.transform);
                                }
                                else
                                {
                                    // ���C���ʏ�m�[�c
                                    mainNotes = Instantiate(normalNotesDouble2Prefab, basePosition, Quaternion.identity, uiCanvasObject.transform);
                                }
                            }
                        }
                        else
                        {
                            // �P��
                            if (notes.special == 2)
                            {
                                // �t�B�[�o�[���m�[�c
                                feverNotes = Instantiate(normalNotesFeverPrefab, basePosition, Quaternion.identity, uiCanvasObject.transform);
                            }
                            if (notes.special == 1)
                            {
                                // ���C���t�B�[�o�[������m�[�c
                                mainNotes = Instantiate(normalNotesSpecial1Prefab, basePosition, Quaternion.identity, uiCanvasObject.transform);
                            }
                            else
                            {
                                // ���C���ʏ�m�[�c
                                mainNotes = Instantiate(normalNotesPrefab, basePosition, Quaternion.identity, uiCanvasObject.transform);
                            }
                        }
                        for (int j = 0; j < longNotesCount; j++)
                        {
                            lanes[j] = notes.notes[j].lane;
                            xSpeeds[j] = xMoveDistanceArray[notes.lane] / sessionCommon.NotesMovingTime;
                            ySpeeds[j] = yMoveDistanceArray[notes.lane] / sessionCommon.NotesMovingTime;
                            zSpeeds[j] = zMoveDistanceArray[notes.lane] / sessionCommon.NotesMovingTime;
                            xBasePositions[j] = xBasePosition;
                            yBasePositions[j] = yBasePosition;
                            zBasePositions[j] = zBasePosition;
                            xNotesLineBasePositions[j] = xBasePosition;
                            yNotesLineBasePositions[j] = yBasePosition;
                            zNotesLineBasePositions[j] = zBasePosition;
                            judgePositions[j] = new Vector3(xJudgeNotesPosition[notes.lane], yJudgeNotesPosition[notes.lane], zJudgeNotesPosition[notes.lane]);
                            notesLineStartJudgeNotesPositions[j] = new Vector3(xJudgeNotesPosition[notes.lane], yJudgeNotesPosition[notes.lane], zJudgeNotesPosition[notes.lane]);
                            notesLineEndBaseNotesPositions[j] = new Vector3(xBasePosition, yBasePosition, zBasePosition);
                            generateTimes[j] = 60 / bpm / notes.notes[j].LPB * notes.notes[j].num + offset;
                            fromMissTimes[j] = generateTimes[j] + sessionCommon.NotesMovingTime - miss;
                            fromBadTimes[j] = generateTimes[j] + sessionCommon.NotesMovingTime - bad;
                            fromGoodTimes[j] = generateTimes[j] + sessionCommon.NotesMovingTime - good;
                            fromGreatTimes[j] = generateTimes[j] + sessionCommon.NotesMovingTime - great;
                            fromPerfectTimes[j] = generateTimes[j] + sessionCommon.NotesMovingTime - perfect;
                            toPerfectTimes[j] = generateTimes[j] + sessionCommon.NotesMovingTime + perfect;
                            toGreatTimes[j] = generateTimes[j] + sessionCommon.NotesMovingTime + great;
                            toGoodTimes[j] = generateTimes[j] + sessionCommon.NotesMovingTime + good;
                            toBadTimes[j] = generateTimes[j] + sessionCommon.NotesMovingTime + bad;
                            toMissTimes[j] = generateTimes[j] + sessionCommon.NotesMovingTime + miss;
                            specials[j] = notes.notes[j].special;
                            if (doubleTapColorMap.ContainsKey(notes.notes[j].LPB.ToString() + "|" + notes.notes[j].num.ToString()))
                            {
                                if (doubleTapColorMap[notes.notes[j].LPB.ToString() + "|" + notes.notes[j].num.ToString()])
                                {
                                    notesObjects[j] = Instantiate(longNotesDouble1Prefab, basePosition, Quaternion.identity, uiCanvasObject.transform);
                                }
                                else
                                {
                                    notesObjects[j] = Instantiate(longNotesDouble2Prefab, basePosition, Quaternion.identity, uiCanvasObject.transform);
                                }
                            }
                            else
                            {
                                notesObjects[j] = Instantiate(longNotesPrefab, basePosition, Quaternion.identity, uiCanvasObject.transform);
                            }
                            notesLineObjects[j] = Instantiate(longNotesLinePrefab, basePosition, Quaternion.identity, uiCanvasObject.transform);
                            notesLines[j] = notesLineObjects[j].GetComponent<LineRenderer>();
                            notesLines[j].material = new Material(Shader.Find("Sprites/Default"));
                            notesLines[j].startColor = initLongLineColor;
                            notesLines[j].endColor = initLongLineColor;
                            unjudgedFlgs[j] = true;
                        }
                        musicDataList.Add(new MusicData(
                              notes.notes.Count
                            , notes.lane
                            , xMoveDistanceArray[notes.lane] / sessionCommon.NotesMovingTime
                            , yMoveDistanceArray[notes.lane] / sessionCommon.NotesMovingTime
                            , 0
                            , xBasePosition
                            , yBasePosition
                            , zBasePosition
                            , xBasePosition
                            , yBasePosition
                            , zBasePosition
                            , new Vector3(xJudgeNotesPosition[notes.lane], yJudgeNotesPosition[notes.lane], zJudgeNotesPosition[notes.lane])
                            , new Vector3(xJudgeNotesPosition[notes.lane], yJudgeNotesPosition[notes.lane], zJudgeNotesPosition[notes.lane])
                            , generateTime
                            , generateTime + sessionCommon.NotesMovingTime - miss
                            , generateTime + sessionCommon.NotesMovingTime - bad
                            , generateTime + sessionCommon.NotesMovingTime - good
                            , generateTime + sessionCommon.NotesMovingTime - great
                            , generateTime + sessionCommon.NotesMovingTime - perfect
                            , generateTime + sessionCommon.NotesMovingTime + perfect
                            , generateTime + sessionCommon.NotesMovingTime + great
                            , generateTime + sessionCommon.NotesMovingTime + good
                            , generateTime + sessionCommon.NotesMovingTime + bad
                            , generateTime + sessionCommon.NotesMovingTime + miss
                            , notes.special
                            , mainNotes
                            , feverNotes
                            , lanes
                            , xSpeeds
                            , ySpeeds
                            , zSpeeds
                            , xBasePositions
                            , yBasePositions
                            , zBasePositions
                            , xNotesLineBasePositions
                            , yNotesLineBasePositions
                            , zNotesLineBasePositions
                            , judgePositions
                            , notesLineStartJudgeNotesPositions
                            , notesLineEndBaseNotesPositions
                            , generateTimes
                            , fromMissTimes
                            , fromBadTimes
                            , fromGoodTimes
                            , fromGreatTimes
                            , fromPerfectTimes
                            , toPerfectTimes
                            , toGreatTimes
                            , toGoodTimes
                            , toBadTimes
                            , toMissTimes
                            , specials
                            , notesObjects
                            , feverNotesObjects
                            , notesLineObjects
                            , notesLines
                            , unjudgedFlgs
                            ));
                        break;
                    case NotesType.RightCurveLong:
                        break;
                    case NotesType.LeftCurveLong:
                        break;
                    case NotesType.TopFlick:
                        break;
                    case NotesType.TopRightFlick:
                        break;
                    case NotesType.RightFlick:
                        break;
                    case NotesType.BottomRightFlick:
                        break;
                    case NotesType.BottomFlick:
                        break;
                    case NotesType.BottomLeftFlick:
                        break;
                    case NotesType.LeftFlick:
                        break;
                    case NotesType.TopLeftFlick:
                        break;
                    default:
                        break;
                }
            }
        }

        // ********************************************************************************
        // �I�t�Z�b�g��ǉ�
        // ********************************************************************************
        public void AddOffset(double baseTime ,float musicLength, SessionCommon sessionCommon)
        {
            int length = musicDataList.Count;
            for (int i = 0; i < length; i++)
            {
                MusicData data = musicDataList[i];
                data.GenerateTime = baseTime + data.GenerateTime;
                data.FromMissTime = baseTime + data.FromMissTime;
                data.FromBadTime = baseTime + data.FromBadTime;
                data.FromGoodTime = baseTime + data.FromGoodTime;
                data.FromGreatTime = baseTime + data.FromGreatTime;
                data.FromPerfectTime = baseTime + data.FromPerfectTime;
                data.ToPerfectTime = baseTime + data.ToPerfectTime;
                data.ToGreatTime = baseTime + data.ToGreatTime;
                data.ToGoodTime = baseTime + data.ToGoodTime;
                data.ToBadTime = baseTime + data.ToBadTime;
                data.ToMissTime = baseTime + data.ToMissTime;
                if (data.LongNotesFlg)
                {
                    for (int j = 0; j < data.Length; j++)
                    {
                        data.GenerateTimes[j] = baseTime + data.GenerateTimes[j];
                        data.FromMissTimes[j] = baseTime + data.FromMissTimes[j];
                        data.FromBadTimes[j] = baseTime + data.FromBadTimes[j];
                        data.FromGoodTimes[j] = baseTime + data.FromGoodTimes[j];
                        data.FromGreatTimes[j] = baseTime + data.FromGreatTimes[j];
                        data.FromPerfectTimes[j] = baseTime + data.FromPerfectTimes[j];
                        data.ToPerfectTimes[j] = baseTime + data.ToPerfectTimes[j];
                        data.ToGreatTimes[j] = baseTime + data.ToGreatTimes[j];
                        data.ToGoodTimes[j] = baseTime + data.ToGoodTimes[j];
                        data.ToBadTimes[j] = baseTime + data.ToBadTimes[j];
                        data.ToMissTimes[j] = baseTime + data.ToMissTimes[j];
                    }
                }
                musicDataList[i] = data;
            }
            sceneTime = baseTime + musicLength + sessionCommon.NotesMovingTime + sessionCommon.TimingTime;
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

            double currentTime = AudioSettings.dspTime;
            // ********************************************************************************
            // ���͊Ď�
            // ********************************************************************************
            if (KeyboardPressUtils.All1())
            {
                JudgeNotesPress(0, currentTime);
            }
            else if (KeyboardReleaseUtils.All1())
            {
                JudgeNotesRelease(0, currentTime);
            }
            if (KeyboardPressUtils.All2())
            {
                JudgeNotesPress(1, currentTime);
            }
            else if (KeyboardReleaseUtils.All2())
            {
                JudgeNotesRelease(1, currentTime);
            }
            if (KeyboardPressUtils.All3())
            {
                JudgeNotesPress(2, currentTime);
            }
            else if (KeyboardReleaseUtils.All3())
            {
                JudgeNotesRelease(2, currentTime);
            }
            if (KeyboardPressUtils.All4())
            {
                JudgeNotesPress(3, currentTime);
            }
            else if (KeyboardReleaseUtils.All4())
            {
                JudgeNotesRelease(3, currentTime);
            }
            if (KeyboardPressUtils.All5())
            {
                JudgeNotesPress(4, currentTime);
            }
            else if (KeyboardReleaseUtils.All5())
            {
                JudgeNotesRelease(4, currentTime);
            }
            if (KeyboardPressUtils.All6())
            {
                JudgeNotesPress(5, currentTime);
            }
            else if (KeyboardReleaseUtils.All6())
            {
                JudgeNotesRelease(5, currentTime);
            }
            if (KeyboardPressUtils.All7())
            {
                JudgeNotesPress(6, currentTime);
            }
            else if (KeyboardReleaseUtils.All7())
            {
                JudgeNotesRelease(6, currentTime);
            }
            if (KeyboardPressUtils.All8())
            {
                JudgeNotesPress(7, currentTime);
            }
            else if (KeyboardReleaseUtils.All8())
            {
                JudgeNotesRelease(7, currentTime);
            }
            if (KeyboardPressUtils.All9())
            {
                JudgeNotesPress(8, currentTime);
            }
            else if (KeyboardReleaseUtils.All9())
            {
                JudgeNotesRelease(8, currentTime);
            }

            // ********************************************************************************
            // �m�[�c�`��
            // ********************************************************************************
            DrawNotes(currentTime);

            // ********************************************************************************
            // �I������
            // ********************************************************************************
            if (sceneTime < currentTime)
            {
                SystemUtils.SafeCall(resultScene);
            }
            if (KeyboardPressUtils.Enter())
            {
                SystemUtils.SafeCall(resultScene);
            }
        }

        // ********************************************************************************
        // �m�[�c����
        // ********************************************************************************
        private void JudgeNotesPress(int lane, double currentTime)
        {
            // ���[�v�񐔂��L���b�V���ɕۑ�
            int length = musicDataList.Count;
            for (int i = 0; i < length; i++)
            {
                MusicData musicData = musicDataList[i];
                // ********************************************************************************
                // �s�K�v���[�v�r��
                // ********************************************************************************
                if (currentTime < musicData.GenerateTime)
                {
                    // �܂��������Ă��Ȃ��m�[�c�ȍ~�͂����s�v�̂��߃��[�v�I��
                    break;
                }
                // ********************************************************************************
                // ���C���m�[�c�𔻒肷��
                // ********************************************************************************
                if (musicData.UnjudgedFlg && musicData.Lane == lane
                    && musicData.FromMissTime < currentTime && currentTime < musicData.ToMissTime)
                {
                    if (musicData.FromPerfectTime < currentTime && currentTime < musicData.ToPerfectTime)
                    {
                        // Perfect
                        PerfectJudgeNotes(i, musicData);
                    }
                    else if (musicData.FromGreatTime < currentTime && currentTime < musicData.ToGreatTime)
                    {
                        // Great
                        GreatJudgeNotes(i, musicData);
                    }
                    else if (musicData.FromGoodTime < currentTime && currentTime < musicData.ToGoodTime)
                    {
                        // Good
                        GoodJudgeNotes(musicData);
                    }
                    else if (musicData.FromBadTime < currentTime && currentTime < musicData.ToBadTime)
                    {
                        // Bad
                        BadJudgeNotes(musicData);
                    }
                    else
                    {
                        // Miss
                        MissJudgeNotes(musicData);
                    }
                    return;
                }
            }
        }
        private void JudgeNotesRelease(int lane, double currentTime)
        {
            // ���[�v�񐔂��L���b�V���ɕۑ�
            int length = musicDataList.Count;
            for (int i = 0; i < length; i++)
            {
                MusicData musicData = musicDataList[i];
                // ********************************************************************************
                // �s�K�v���[�v�r��
                // ********************************************************************************
                if (currentTime < musicData.GenerateTime)
                {
                    // �܂��������Ă��Ȃ��m�[�c�ȍ~�͂����s�v�̂��߃��[�v�I��
                    break;
                }
                if (!musicData.UnjudgedFlg && musicData.Lane == lane)
                {
                    int lastIndex = musicData.Length - 1;
                    if (musicData.FromPerfectTimes[lastIndex] < currentTime && currentTime < musicData.ToPerfectTimes[lastIndex])
                    {
                        // Perfect
                        PerfectJudgeNotes(i, musicData);
                    }
                    else if (musicData.FromGreatTimes[lastIndex] < currentTime && currentTime < musicData.ToGreatTimes[lastIndex])
                    {
                        // Great
                        GreatJudgeNotes(i, musicData);
                    }
                    else if (musicData.FromGoodTimes[lastIndex] < currentTime && currentTime < musicData.ToGoodTimes[lastIndex])
                    {
                        // Good
                        GoodJudgeNotes(musicData);
                    }
                    else if (musicData.FromBadTimes[lastIndex] < currentTime && currentTime < musicData.ToBadTimes[lastIndex])
                    {
                        // Bad
                        BadJudgeNotes(musicData);
                    }
                    else
                    {
                        // Miss
                        MissJudgeNotes(musicData);
                    }
                    return;
                }
            }
        }

        // ********************************************************************************
        // �m�[�c�̊e�픻����Ǘ�
        // ********************************************************************************
        public void PerfectJudgeNotes(int index, MusicData musicData)
        {
            DisplayJudgePerfect(musicData.Lane);
            SystemUtils.SafeCall(perfectListener);
            SystemUtils.SafeCall(comboPlusListener);
            Fever(musicData);
            if (feverFlg && musicData.Special == 2)
            {
                SystemUtils.SafeCall(feverScoreListener);
            }
            else
            {
                SystemUtils.SafeCall(normalScoreListener);
            }
            OnceDestroyNotes(index, musicData);
            SystemUtils.SafeCall(normalNotesSE);
        }
        public void GreatJudgeNotes(int index, MusicData musicData)
        {
            DisplayJudgeGreat(musicData.Lane);
            SystemUtils.SafeCall(greatListener);
            SystemUtils.SafeCall(comboPlusListener);
            Fever(musicData);
            if (feverFlg && musicData.Special == 2)
            {
                SystemUtils.SafeCall(feverScoreListener);
            }
            else
            {
                SystemUtils.SafeCall(normalScoreListener);
            }
            OnceDestroyNotes(index, musicData);
            SystemUtils.SafeCall(normalNotesSE);
        }
        public void GoodJudgeNotes(MusicData musicData)
        {
            DisplayJudgeGood(musicData.Lane);
            SystemUtils.SafeCall(goodListener);
            SystemUtils.SafeCall(comboResetListener);
            DestroyNotes(musicData);
            SystemUtils.SafeCall(normalNotesSE);
        }
        public void BadJudgeNotes(MusicData musicData)
        {
            DisplayJudgeBad(musicData.Lane);
            SystemUtils.SafeCall(badListener);
            SystemUtils.SafeCall(comboResetListener);
            DestroyNotes(musicData);
            SystemUtils.SafeCall(normalNotesSE);
        }
        public void MissJudgeNotes(MusicData musicData)
        {
            DisplayJudgeMiss(musicData.Lane);
            SystemUtils.SafeCall(missListener);
            SystemUtils.SafeCall(comboResetListener);
            DestroyNotes(musicData);
        }
        private void OnceDestroyNotes(int index, MusicData musicData)
        {
            if (musicData.UnjudgedFlg)
            {
                if (musicData.LongNotesFlg)
                {
                    musicData.UnjudgedFlg = false;
                    musicData.NotesObject.transform.position = musicData.JudgePosition;
                    musicDataList[index] = musicData;
                }
                else
                {
                    musicData.NotesObject.SetActive(false);
                    musicDataList.Remove(musicData);
                }
                return;
            }
            musicData.NotesObject.SetActive(false);
            int length = musicData.Length;
            if (0 < length)
            {
                musicData.NotesLineObjects[0].SetActive(false);
                musicData.NotesObjects[0].SetActive(false);
                musicDataList.Remove(musicData);
            }
        }
        private void DestroyNotes(MusicData musicData)
        {
            musicData.NotesObject.SetActive(false);
            if (musicData.LongNotesFlg)
            {
                int length = musicData.Length;
                for (int i = 0; i < length; i++)
                {
                    musicData.NotesLineObjects[i].SetActive(false);
                    musicData.NotesObjects[i].SetActive(false);
                }
            }
            musicDataList.Remove(musicData);
        }

        private void Fever(MusicData musicData)
        {
            if (musicData.Special == 1)
            {
                nonFeverObject[feverCount].SetActive(false);
                feverObject[feverCount].SetActive(true);
                feverCount++;
                feverFlg = 9 <= feverCount;
            }
        }
        public void Score(float score)
        {
            scoreText.text = score.ToString();
            switch (ScoreRankUtils.GetScoreRank(score, maxScore))
            {
                case ScoreRank.D:
                    scoreSliderImage.color = scoreColorD;
                    break;
                case ScoreRank.C:
                    scoreSliderImage.color = scoreColorC;
                    break;
                case ScoreRank.B:
                    scoreSliderImage.color = scoreColorB;
                    break;
                case ScoreRank.A:
                    scoreSliderImage.color = scoreColorA;
                    break;
                default:
                    scoreSliderImage.color = scoreColorS;
                    break;
            }
            scoreSlider.value = score / maxScore;
        }

        // ********************************************************************************
        // �m�[�c�̊e�픻��e�L�X�g��\��
        // ********************************************************************************
        private void DisplayJudgePerfect(int lane)
        {
            SafeStopCoroutine(textCoroutine[lane]);
            textCoroutine[lane] = StartCoroutine(DisplayPerfect(lane));
        }
        private void DisplayJudgeGreat(int lane)
        {
            SafeStopCoroutine(textCoroutine[lane]);
            textCoroutine[lane] = StartCoroutine(DisplayGreat(lane));
        }
        private void DisplayJudgeGood(int lane)
        {
            SafeStopCoroutine(textCoroutine[lane]);
            textCoroutine[lane] = StartCoroutine(DisplayGood(lane));
        }
        private void DisplayJudgeBad(int lane)
        {
            SafeStopCoroutine(textCoroutine[lane]);
            textCoroutine[lane] = StartCoroutine(DisplayBad(lane));
        }
        private void DisplayJudgeMiss(int lane)
        {
            SafeStopCoroutine(textCoroutine[lane]);
            textCoroutine[lane] = StartCoroutine(DisplayMiss(lane));
        }
        private IEnumerator DisplayPerfect(int lane)
        {
            // ���ׂĔ�\��
            perfectText[lane].SetActive(false);
            greatText[lane].SetActive(false);
            goodText[lane].SetActive(false);
            badText[lane].SetActive(false);
            missText[lane].SetActive(false);
            // ����e�L�X�g�\��
            perfectText[lane].SetActive(true);
            yield return new WaitForSeconds(0.6f);
            perfectText[lane].SetActive(false);
        }
        private IEnumerator DisplayGreat(int lane)
        {
            // ���ׂĔ�\��
            perfectText[lane].SetActive(false);
            greatText[lane].SetActive(false);
            goodText[lane].SetActive(false);
            badText[lane].SetActive(false);
            missText[lane].SetActive(false);
            // ����e�L�X�g�\��
            greatText[lane].SetActive(true);
            yield return new WaitForSeconds(0.6f);
            greatText[lane].SetActive(false);
        }
        private IEnumerator DisplayGood(int lane)
        {
            // ���ׂĔ�\��
            perfectText[lane].SetActive(false);
            greatText[lane].SetActive(false);
            goodText[lane].SetActive(false);
            badText[lane].SetActive(false);
            missText[lane].SetActive(false);
            // ����e�L�X�g�\��
            goodText[lane].SetActive(true);
            yield return new WaitForSeconds(0.6f);
            goodText[lane].SetActive(false);
        }
        private IEnumerator DisplayBad(int lane)
        {
            // ���ׂĔ�\��
            perfectText[lane].SetActive(false);
            greatText[lane].SetActive(false);
            goodText[lane].SetActive(false);
            badText[lane].SetActive(false);
            missText[lane].SetActive(false);
            // ����e�L�X�g�\��
            badText[lane].SetActive(true);
            yield return new WaitForSeconds(0.6f);
            badText[lane].SetActive(false);
        }
        private IEnumerator DisplayMiss(int lane)
        {
            // ���ׂĔ�\��
            perfectText[lane].SetActive(false);
            greatText[lane].SetActive(false);
            goodText[lane].SetActive(false);
            badText[lane].SetActive(false);
            missText[lane].SetActive(false);
            // ����e�L�X�g�\��
            missText[lane].SetActive(true);
            yield return new WaitForSeconds(0.6f);
            missText[lane].SetActive(false);
        }

        // ********************************************************************************
        // �m�[�c�`��
        // ********************************************************************************
        public void DrawNotes(double currentTime)
        {
            // ���[�v�񐔂��L���b�V���ɕۑ�
            int length = musicDataList.Count;
            for (int i = 0; i < length; i++)
            {
                MusicData musicData = musicDataList[i];
                // ********************************************************************************
                // �s�K�v���[�v�r��
                // ********************************************************************************
                if (currentTime < musicData.GenerateTime)
                {
                    // �܂��������Ă��Ȃ��m�[�c�ȍ~�͂����s�v�̂��߃��[�v�I��
                    break;
                }
                // ********************************************************************************
                // ���C���m�[�c�𔻒肷��
                // ********************************************************************************
                if (musicData.UnjudgedFlg)
                {
                    // ********************************************************************************
                    // ���R���Ŕ���
                    // ********************************************************************************
                    if (musicData.ToMissTime < currentTime)
                    {
                        // �~�X������s
                        MissJudgeNotes(musicData);
                        // ���X�g��������邽�߃C���f�b�N�X����
                        i--;
                        length--;
                        continue;
                    }
                    // ********************************************************************************
                    // �o�ߎ��Ԃ�����W�����Z���A���C���m�[�c�ʒu���X�V����
                    // ********************************************************************************
                    if (feverFlg && musicData.Special == 2)
                    {
                        musicData.FeverNotesObject.transform.position = new Vector3(
                              (float)(musicData.XSpeed * (currentTime - musicData.GenerateTime) + musicData.XBasePosition)
                            , (float)(musicData.YSpeed * (currentTime - musicData.GenerateTime) + musicData.YBasePosition)
                            , musicData.ZBasePosition);
                        musicData.FeverNotesObject.SetActive(true);
                    }
                    else
                    {
                        musicData.NotesObject.transform.position = new Vector3(
                              (float)(musicData.XSpeed * (currentTime - musicData.GenerateTime) + musicData.XBasePosition)
                            , (float)(musicData.YSpeed * (currentTime - musicData.GenerateTime) + musicData.YBasePosition)
                            , musicData.ZBasePosition);
                        musicData.NotesObject.SetActive(true);
                    }
                }
                // ********************************************************************************
                // �����O�m�[�c�����݂���ꍇ�̂݁A�����O�m�[�c������s��
                // ********************************************************************************
                if (musicData.LongNotesFlg)
                {
                    // ********************************************************************************
                    // ���R���Ŕ���
                    // ********************************************************************************
                    if (musicData.ToMissTimes[musicData.Length - 1] < currentTime)
                    {
                        // �~�X������s
                        MissJudgeNotes(musicData);
                        // ���X�g��������邽�߃C���f�b�N�X����
                        i--;
                        length--;
                        continue;
                    }
                    int len = musicData.Length;
                    for (int j = 0; j < len; j++)
                    {
                        // ********************************************************************************
                        // �����O�m�[�c�𔻒肷��
                        // ********************************************************************************
                        if (musicData.UnjudgedFlgs[j])
                        {
                            // ********************************************************************************
                            // �����O�m�[�c���C���̕`�攻��
                            // ********************************************************************************
                            if (j == 0)
                            {
                                // ���C���m�[�c��������ł���ꍇ�͕`�悪�K�v
                                if (musicData.GenerateTime < currentTime)
                                {
                                    // ********************************************************************************
                                    // �����O�m�[�c���C���J�n�ʒu�����Z(����͏�ɕ`��K�{)
                                    // ********************************************************************************
                                    // ����m�[�c���A���C���m�[�c��ǔ�����2��
                                    if (musicData.UnjudgedFlg)
                                    {
                                        // ���C���m�[�c�����݂���ꍇ�A�o�ߎ��Ԃ��烁�C���m�[�c�̈ʒu���W�����Z
                                        musicData.NotesLines[j].SetPosition(0, new Vector3(
                                              (float)(musicData.XSpeed * (currentTime - musicData.GenerateTime) + musicData.XNotesLineBasePosition)
                                            , (float)(musicData.YSpeed * (currentTime - musicData.GenerateTime) + musicData.YNotesLineBasePosition)
                                            , (float)musicData.ZNotesLineBasePosition));
                                    }
                                    else
                                    {
                                        // ���C���m�[�c�����݂��Ȃ��ꍇ�A����m�[�c���W�ŏ㏑��
                                        musicData.NotesLines[j].SetPosition(0, musicData.NotesLineStartJudgeNotesPosition);
                                    }
                                    // ********************************************************************************
                                    // �����O�m�[�c���C���I���ʒu�����Z(����͏�ɕ`��K�{)
                                    // ********************************************************************************
                                    // �x�[�X�m�[�c���A�������O�m�[�c��ǔ�����2��
                                    if (musicData.GenerateTimes[j] < currentTime)
                                    {
                                        // �������O�m�[�c��ǔ�
                                        musicData.NotesLines[j].SetPosition(1, new Vector3(
                                              (float)(musicData.XSpeeds[j] * (currentTime - musicData.GenerateTimes[j]) + musicData.XNotesLineBasePositions[j])
                                            , (float)(musicData.YSpeeds[j] * (currentTime - musicData.GenerateTimes[j]) + musicData.YNotesLineBasePositions[j])
                                            , (float)musicData.ZNotesLineBasePositions[j]));
                                    }
                                    else
                                    {
                                        // �x�[�X�m�[�c
                                        musicData.NotesLines[j].SetPosition(1, musicData.NotesLineEndBaseNotesPositions[j]);
                                    }
                                    // ********************************************************************************
                                    // �����O�m�[�c���C���`��
                                    // ********************************************************************************
                                    musicData.NotesLineObjects[j].SetActive(true);
                                }
                            }
                            else
                            {
                                // ���C���m�[�c��������ł���ꍇ�͕`�悪�K�v
                                if (musicData.GenerateTime < currentTime)
                                {
                                    // ********************************************************************************
                                    // �����O�m�[�c���C���J�n�ʒu�����Z(����͏�ɕ`��K�{)
                                    // ********************************************************************************
                                    // ����m�[�c���A1�O�̃m�[�c��ǔ�����2��
                                    if (musicData.UnjudgedFlgs[j - 1])
                                    {
                                        // 1�O�̃����O�m�[�c�����݂���ꍇ�A1�O�̃����O�m�[�c��ǔ�
                                        musicData.NotesLines[j].SetPosition(0, new Vector3(
                                              (float)(musicData.XSpeeds[j] * (currentTime - musicData.GenerateTimes[j - 1]) + musicData.XNotesLineBasePositions[j])
                                            , (float)(musicData.YSpeeds[j] * (currentTime - musicData.GenerateTimes[j - 1]) + musicData.YNotesLineBasePositions[j])
                                            , (float)musicData.ZNotesLineBasePositions[j]));
                                    }
                                    else
                                    {
                                        // 1�O�̃����O�m�[�c�����݂��Ȃ��ꍇ�A����m�[�c���W�ŏ㏑��
                                        musicData.NotesLines[j].SetPosition(0, musicData.NotesLineStartJudgeNotesPositions[j]);
                                    }
                                    // ********************************************************************************
                                    // �����O�m�[�c���C���I���ʒu�����Z(����͏�ɕ`��K�{)
                                    // ********************************************************************************
                                    // �x�[�X�m�[�c���A�������O�m�[�c��ǔ�����2��
                                    if (musicData.GenerateTimes[j] < currentTime)
                                    {
                                        // �������O�m�[�c��ǔ�
                                        musicData.NotesLines[j].SetPosition(1, new Vector3(
                                              (float)(musicData.XSpeeds[j] * (currentTime - musicData.GenerateTimes[j]) + musicData.XNotesLineBasePositions[j])
                                            , (float)(musicData.YSpeeds[j] * (currentTime - musicData.GenerateTimes[j]) + musicData.YNotesLineBasePositions[j])
                                            , (float)musicData.ZNotesLineBasePositions[j]));
                                    }
                                    else
                                    {
                                        // �x�[�X�m�[�c
                                        musicData.NotesLines[j].SetPosition(1, musicData.NotesLineEndBaseNotesPositions[j]);
                                    }
                                    // ********************************************************************************
                                    // �����O�m�[�c���C���`��
                                    // ********************************************************************************
                                    musicData.NotesLineObjects[j].SetActive(true);
                                }
                            }
                            // ********************************************************************************
                            // �����O�m�[�c�̕`�攻��
                            // ********************************************************************************
                            // �����O�m�[�c������ł���Ε`�悪�K�v
                            if (musicData.GenerateTimes[j] < currentTime)
                            {
                                // ********************************************************************************
                                // �����O�m�[�c�̑傫���E�ʒu�����Z
                                // ********************************************************************************
                                // ���ݎ�������X�P�[�����������Z���K�p
                                float scale = (float)((currentTime - musicData.GenerateTimes[j]) / (musicData.ToGreatTimes[j] - musicData.GenerateTimes[j]));
                                Vector3 localScale = musicData.NotesObjects[j].transform.localScale;
                                localScale.x = scale;
                                localScale.y = scale;
                                musicData.NotesObjects[j].transform.localScale = localScale;
                                // ���W���Z
                                musicData.NotesObjects[j].transform.position = new Vector3(
                                      (float)(musicData.XSpeeds[j] * (currentTime - musicData.GenerateTimes[j]) + musicData.XBasePositions[j])
                                    , (float)(musicData.YSpeeds[j] * (currentTime - musicData.GenerateTimes[j]) + musicData.YBasePositions[j])
                                    , (float)musicData.ZNotesLineBasePositions[j]);
                                musicData.NotesObjects[j].SetActive(true);
                            }
                        }
                    }
                }
            }
        }

        // ********************************************************************************
        // �R���{�\��
        // ********************************************************************************
        public void Combo(int combo)
        {
            SafeStopCoroutine(comboCoroutine);
            if (combo == 0)
            {
                comboCoroutine = StartCoroutine(DisplayComboReset());
            }
            else
            {
                comboCoroutine = StartCoroutine(DisplayCombo(combo));
            }
        }

        private IEnumerator DisplayComboReset()
        {
            comboNumber.SetText("");
            comboNumberObject.SetActive(false);
            comboTextObject.SetActive(false);
            yield return null;
        }

        private IEnumerator DisplayCombo(int combo)
        {
            comboNumber.SetText(combo.ToString());
            comboNumberObject.SetActive(false);
            comboNumberObject.SetActive(true);
            comboTextObject.SetActive(false);
            comboTextObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }

        // ********************************************************************************
        // �R���[�`�����~
        // ********************************************************************************
        private void SafeStopCoroutine(Coroutine coroutine)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
        }
    }
}
