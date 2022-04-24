using UnityEngine;

namespace Entrance.Model
{
    public struct MusicData
    {
        // **************************************************
        // 内部情報定義
        // **************************************************
        // ロングノーツがあるか
        private bool longNotesFlg;
        // ロングノーツの数
        private int length;

        // **************************************************
        // メインノーツ定義
        // **************************************************
        // レーン
        private int lane;
        // 各座標の移動速度（描画座標演算用）
        private float xSpeed;
        private float ySpeed;
        private float zSpeed;
        // 各座標の基準ノーツ座標（描画座標演算用）
        private float xBasePosition;
        private float yBasePosition;
        private float zBasePosition;
        // 各座標のロングノーツライン演算用の基準ノーツ座標（描画座標演算用）
        private float xNotesLineBasePosition;
        private float yNotesLineBasePosition;
        private float zNotesLineBasePosition;
        // 判定座標
        private Vector3 judgePosition;
        // ノーツライン開始描画用の判定ノーツ座標
        private Vector3 notesLineStartJudgeNotesPosition;
        // ノーツ生成時間
        private double generateTime;
        // ノーツ判定時間
        private double fromMissTime;
        private double fromBadTime;
        private double fromGoodTime;
        private double fromGreatTime;
        private double fromPerfectTime;
        private double toPerfectTime;
        private double toGreatTime;
        private double toGoodTime;
        private double toBadTime;
        private double toMissTime;
        // 特殊ノーツ
        private int special;
        // ノーツオブジェクト
        private GameObject notesObject;
        // フィーバーノーツオブジェクト
        private GameObject feverNotesObject;
        // 未判定フラグ
        private bool unjudgedFlg;

        // **************************************************
        // ロングノーツ定義
        // **************************************************
        // レーン
        private int[] lanes;
        // 各座標の移動速度（描画座標演算用）
        private float[] xSpeeds;
        private float[] ySpeeds;
        private float[] zSpeeds;
        // 各座標の基準ノーツ座標（描画座標演算用）
        private float[] xBasePositions;
        private float[] yBasePositions;
        private float[] zBasePositions;
        // 各座標のロングノーツライン演算用の基準ノーツ座標（描画座標演算用）
        private float[] xNotesLineBasePositions;
        private float[] yNotesLineBasePositions;
        private float[] zNotesLineBasePositions;
        // 判定座標
        private Vector3[] judgePositions;
        // ノーツライン開始描画用の判定ノーツ座標
        private Vector3[] notesLineStartJudgeNotesPositions;
        // ノーツライン終了描画用のベースノーツ座標
        private Vector3[] notesLineEndBaseNotesPositions;
        // ノーツ生成時間
        private double[] generateTimes;
        // ノーツ判定時間
        private double[] fromMissTimes;
        private double[] fromBadTimes;
        private double[] fromGoodTimes;
        private double[] fromGreatTimes;
        private double[] fromPerfectTimes;
        private double[] toPerfectTimes;
        private double[] toGreatTimes;
        private double[] toGoodTimes;
        private double[] toBadTimes;
        private double[] toMissTimes;
        // 特殊ノーツ
        private int[] specials;
        // ノーツオブジェクト
        private GameObject[] notesObjects;
        // フィーバーノーツオブジェクト
        private GameObject[] feverNotesObjects;
        // ノーツラインオブジェクト
        private GameObject[] notesLineObjects;
        // ノーツライン
        private LineRenderer[] notesLines;
        // 未判定フラグ
        private bool[] unjudgedFlgs;

        // **************************************************
        // シングルノーツ用コンストラクタ
        // **************************************************
        public MusicData(
            int lane,
            float xSpeed,
            float ySpeed,
            float zSpeed,
            float xBasePosition,
            float yBasePosition,
            float zBasePosition,
            float xNotesLineBasePosition,
            float yNotesLineBasePosition,
            float zNotesLineBasePosition,
            Vector3 judgePosition,
            Vector3 notesLineStartJudgeNotesPosition,
            double generateTime,
            double fromMissTime,
            double fromBadTime,
            double fromGoodTime,
            double fromGreatTime,
            double fromPerfectTime,
            double toPerfectTime,
            double toGreatTime,
            double toGoodTime,
            double toBadTime,
            double toMissTime,
            int special,
            GameObject notesObject,
            GameObject feverNotesObject
            )
        {
            // 内部情報定義
            this.longNotesFlg = false;
            this.length = 0;
            // メインノーツ定義
            this.lane = lane;
            this.xSpeed = xSpeed;
            this.ySpeed = ySpeed;
            this.zSpeed = zSpeed;
            this.xBasePosition = xBasePosition;
            this.yBasePosition = yBasePosition;
            this.zBasePosition = zBasePosition;
            this.xNotesLineBasePosition = xNotesLineBasePosition;
            this.yNotesLineBasePosition = yNotesLineBasePosition;
            this.zNotesLineBasePosition = zNotesLineBasePosition;
            this.judgePosition = judgePosition;
            this.notesLineStartJudgeNotesPosition = notesLineStartJudgeNotesPosition;
            this.generateTime = generateTime;
            this.fromMissTime = fromMissTime;
            this.fromBadTime = fromBadTime;
            this.fromGoodTime = fromGoodTime;
            this.fromGreatTime = fromGreatTime;
            this.fromPerfectTime = fromPerfectTime;
            this.toPerfectTime = toPerfectTime;
            this.toGreatTime = toGreatTime;
            this.toGoodTime = toGoodTime;
            this.toBadTime = toBadTime;
            this.toMissTime = toMissTime;
            this.special = special;
            this.notesObject = notesObject;
            this.feverNotesObject = feverNotesObject;
            this.unjudgedFlg = true;
            // ロングノーツ定義
            this.lanes = null;
            this.xSpeeds = null;
            this.ySpeeds = null;
            this.zSpeeds = null;
            this.xBasePositions = null;
            this.yBasePositions = null;
            this.zBasePositions = null;
            this.xNotesLineBasePositions = null;
            this.yNotesLineBasePositions = null;
            this.zNotesLineBasePositions = null;
            this.judgePositions = null;
            this.notesLineStartJudgeNotesPositions = null;
            this.notesLineEndBaseNotesPositions = null;
            this.generateTimes = null;
            this.fromMissTimes = null;
            this.fromBadTimes = null;
            this.fromGoodTimes = null;
            this.fromGreatTimes = null;
            this.fromPerfectTimes = null;
            this.toPerfectTimes = null;
            this.toGreatTimes = null;
            this.toGoodTimes = null;
            this.toBadTimes = null;
            this.toMissTimes = null;
            this.specials = null;
            this.notesObjects = null;
            this.notesLineObjects = null;
            this.feverNotesObjects = null;
            this.notesLines = null;
            this.unjudgedFlgs = null;
            // ノーツを非表示
            notesObject.SetActive(false);
        }

        // **************************************************
        // ロングノーツ用コンストラクタ
        // **************************************************
        public MusicData(
            int length,
            int lane,
            float xSpeed,
            float ySpeed,
            float zSpeed,
            float xBasePosition,
            float yBasePosition,
            float zBasePosition,
            float xNotesLineBasePosition,
            float yNotesLineBasePosition,
            float zNotesLineBasePosition,
            Vector3 judgePosition,
            Vector3 notesLineStartJudgeNotesPosition,
            double generateTime,
            double fromMissTime,
            double fromBadTime,
            double fromGoodTime,
            double fromGreatTime,
            double fromPerfectTime,
            double toPerfectTime,
            double toGreatTime,
            double toGoodTime,
            double toBadTime,
            double toMissTime,
            int special,
            GameObject notesObject,
            GameObject feverNotesObject,
            int[] lanes,
            float[] xSpeeds,
            float[] ySpeeds,
            float[] zSpeeds,
            float[] xBasePositions,
            float[] yBasePositions,
            float[] zBasePositions,
            float[] xNotesLineBasePositions,
            float[] yNotesLineBasePositions,
            float[] zNotesLineBasePositions,
            Vector3[] judgePositions,
            Vector3[] notesLineStartJudgeNotesPositions,
            Vector3[] notesLineEndBaseNotesPositions,
            double[] generateTimes,
            double[] fromMissTimes,
            double[] fromBadTimes,
            double[] fromGoodTimes,
            double[] fromGreatTimes,
            double[] fromPerfectTimes,
            double[] toPerfectTimes,
            double[] toGreatTimes,
            double[] toGoodTimes,
            double[] toBadTimes,
            double[] toMissTimes,
            int[] specials,
            GameObject[] notesObjects,
            GameObject[] feverNotesObjects,
            GameObject[] notesLineObjects,
            LineRenderer[] notesLines,
            bool[] unjudgedFlgs
            )
        {
            // 内部情報定義
            this.longNotesFlg = true;
            this.length = length;
            // メインノーツ定義
            this.lane = lane;
            this.xSpeed = xSpeed;
            this.ySpeed = ySpeed;
            this.zSpeed = zSpeed;
            this.xBasePosition = xBasePosition;
            this.yBasePosition = yBasePosition;
            this.zBasePosition = zBasePosition;
            this.xNotesLineBasePosition = xNotesLineBasePosition;
            this.yNotesLineBasePosition = yNotesLineBasePosition;
            this.zNotesLineBasePosition = zNotesLineBasePosition;
            this.judgePosition = judgePosition;
            this.notesLineStartJudgeNotesPosition = notesLineStartJudgeNotesPosition;
            this.generateTime = generateTime;
            this.fromMissTime = fromMissTime;
            this.fromBadTime = fromBadTime;
            this.fromGoodTime = fromGoodTime;
            this.fromGreatTime = fromGreatTime;
            this.fromPerfectTime = fromPerfectTime;
            this.toPerfectTime = toPerfectTime;
            this.toGreatTime = toGreatTime;
            this.toGoodTime = toGoodTime;
            this.toBadTime = toBadTime;
            this.toMissTime = toMissTime;
            this.special = special;
            this.notesObject = notesObject;
            this.feverNotesObject = feverNotesObject;
            this.unjudgedFlg = true;
            // ロングノーツ定義
            this.lanes = lanes;
            this.xSpeeds = xSpeeds;
            this.ySpeeds = ySpeeds;
            this.zSpeeds = zSpeeds;
            this.xBasePositions = xBasePositions;
            this.yBasePositions = yBasePositions;
            this.zBasePositions = zBasePositions;
            this.xNotesLineBasePositions = xNotesLineBasePositions;
            this.yNotesLineBasePositions = yNotesLineBasePositions;
            this.zNotesLineBasePositions = zNotesLineBasePositions;
            this.judgePositions = judgePositions;
            this.notesLineStartJudgeNotesPositions = notesLineStartJudgeNotesPositions;
            this.notesLineEndBaseNotesPositions = notesLineEndBaseNotesPositions;
            this.generateTimes = generateTimes;
            this.fromMissTimes = fromMissTimes;
            this.fromBadTimes = fromBadTimes;
            this.fromGoodTimes = fromGoodTimes;
            this.fromGreatTimes = fromGreatTimes;
            this.fromPerfectTimes = fromPerfectTimes;
            this.toPerfectTimes = toPerfectTimes;
            this.toGreatTimes = toGreatTimes;
            this.toGoodTimes = toGoodTimes;
            this.toBadTimes = toBadTimes;
            this.toMissTimes = toMissTimes;
            this.specials = specials;
            this.notesObjects = notesObjects;
            this.feverNotesObjects = feverNotesObjects;
            this.notesLineObjects = notesLineObjects;
            this.notesLines = notesLines;
            this.unjudgedFlgs = unjudgedFlgs;
            // ノーツを非表示
            notesObject.SetActive(false);
            for (int i = 0; i < length; i++)
            {
                notesObjects[i].SetActive(false);
                notesLineObjects[i].SetActive(false);
            }
        }

        public bool LongNotesFlg { get => longNotesFlg; }
        public int Length { get => length; }
        public int Lane { get => lane; set => lane = value; }
        public float XSpeed { get => xSpeed; set => xSpeed = value; }
        public float YSpeed { get => ySpeed; set => ySpeed = value; }
        public float ZSpeed { get => zSpeed; set => zSpeed = value; }
        public float XBasePosition { get => xBasePosition; set => xBasePosition = value; }
        public float YBasePosition { get => yBasePosition; set => yBasePosition = value; }
        public float ZBasePosition { get => zBasePosition; set => zBasePosition = value; }
        public float XNotesLineBasePosition { get => xNotesLineBasePosition; set => xNotesLineBasePosition = value; }
        public float YNotesLineBasePosition { get => yNotesLineBasePosition; set => yNotesLineBasePosition = value; }
        public float ZNotesLineBasePosition { get => zNotesLineBasePosition; set => zNotesLineBasePosition = value; }
        public Vector3 JudgePosition { get => judgePosition; set => judgePosition = value; }
        public Vector3 NotesLineStartJudgeNotesPosition { get => notesLineStartJudgeNotesPosition; set => notesLineStartJudgeNotesPosition = value; }
        public double GenerateTime { get => generateTime; set => generateTime = value; }
        public double FromMissTime { get => fromMissTime; set => fromMissTime = value; }
        public double FromBadTime { get => fromBadTime; set => fromBadTime = value; }
        public double FromGoodTime { get => fromGoodTime; set => fromGoodTime = value; }
        public double FromGreatTime { get => fromGreatTime; set => fromGreatTime = value; }
        public double FromPerfectTime { get => fromPerfectTime; set => fromPerfectTime = value; }
        public double ToPerfectTime { get => toPerfectTime; set => toPerfectTime = value; }
        public double ToGreatTime { get => toGreatTime; set => toGreatTime = value; }
        public double ToGoodTime { get => toGoodTime; set => toGoodTime = value; }
        public double ToBadTime { get => toBadTime; set => toBadTime = value; }
        public double ToMissTime { get => toMissTime; set => toMissTime = value; }
        public int Special { get => special; set => special = value; }
        public GameObject NotesObject { get => notesObject; set => notesObject = value; }
        public GameObject FeverNotesObject { get => feverNotesObject; set => feverNotesObject = value; }
        public bool UnjudgedFlg { get => unjudgedFlg; set => unjudgedFlg = value; }
        public int[] Lanes { get => lanes; set => lanes = value; }
        public float[] XSpeeds { get => xSpeeds; set => xSpeeds = value; }
        public float[] YSpeeds { get => ySpeeds; set => ySpeeds = value; }
        public float[] ZSpeeds { get => zSpeeds; set => zSpeeds = value; }
        public float[] XBasePositions { get => xBasePositions; set => xBasePositions = value; }
        public float[] YBasePositions { get => yBasePositions; set => yBasePositions = value; }
        public float[] ZBasePositions { get => zBasePositions; set => zBasePositions = value; }
        public float[] XNotesLineBasePositions { get => xNotesLineBasePositions; set => xNotesLineBasePositions = value; }
        public float[] YNotesLineBasePositions { get => yNotesLineBasePositions; set => yNotesLineBasePositions = value; }
        public float[] ZNotesLineBasePositions { get => zNotesLineBasePositions; set => zNotesLineBasePositions = value; }
        public Vector3[] JudgePositions { get => judgePositions; set => judgePositions = value; }
        public Vector3[] NotesLineStartJudgeNotesPositions { get => notesLineStartJudgeNotesPositions; set => notesLineStartJudgeNotesPositions = value; }
        public Vector3[] NotesLineEndBaseNotesPositions { get => notesLineEndBaseNotesPositions; set => notesLineEndBaseNotesPositions = value; }
        public double[] GenerateTimes { get => generateTimes; set => generateTimes = value; }
        public double[] FromMissTimes { get => fromMissTimes; set => fromMissTimes = value; }
        public double[] FromBadTimes { get => fromBadTimes; set => fromBadTimes = value; }
        public double[] FromGoodTimes { get => fromGoodTimes; set => fromGoodTimes = value; }
        public double[] FromGreatTimes { get => fromGreatTimes; set => fromGreatTimes = value; }
        public double[] FromPerfectTimes { get => fromPerfectTimes; set => fromPerfectTimes = value; }
        public double[] ToPerfectTimes { get => toPerfectTimes; set => toPerfectTimes = value; }
        public double[] ToGreatTimes { get => toGreatTimes; set => toGreatTimes = value; }
        public double[] ToGoodTimes { get => toGoodTimes; set => toGoodTimes = value; }
        public double[] ToBadTimes { get => toBadTimes; set => toBadTimes = value; }
        public double[] ToMissTimes { get => toMissTimes; set => toMissTimes = value; }
        public int[] Specials { get => specials; set => specials = value; }
        public GameObject[] NotesObjects { get => notesObjects; set => notesObjects = value; }
        public GameObject[] FeverNotesObjects { get => feverNotesObjects; set => feverNotesObjects = value; }
        public GameObject[] NotesLineObjects { get => notesLineObjects; set => notesLineObjects = value; }
        public LineRenderer[] NotesLines { get => notesLines; set => notesLines = value; }
        public bool[] UnjudgedFlgs { get => unjudgedFlgs; set => unjudgedFlgs = value; }
    }
}
