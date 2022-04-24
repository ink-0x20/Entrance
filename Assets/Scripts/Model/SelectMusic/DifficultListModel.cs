namespace Entrance.Model
{
    public struct DifficultListModel
    {
        // **************************************************
        // 表示用難易度
        // **************************************************
        private bool difficultFlg1;
        private bool difficultFlg2;
        private bool difficultFlg3;
        private bool difficultFlg4;
        private bool difficultFlg5;

        // **************************************************
        // コンストラクタ
        // **************************************************
        public DifficultListModel(bool difficultFlg1, bool difficultFlg2, bool difficultFlg3, bool difficultFlg4, bool difficultFlg5)
        {
            this.difficultFlg1 = difficultFlg1;
            this.difficultFlg2 = difficultFlg2;
            this.difficultFlg3 = difficultFlg3;
            this.difficultFlg4 = difficultFlg4;
            this.difficultFlg5 = difficultFlg5;
        }

        public bool DifficultFlg1 { get => difficultFlg1; set => difficultFlg1 = value; }
        public bool DifficultFlg2 { get => difficultFlg2; set => difficultFlg2 = value; }
        public bool DifficultFlg3 { get => difficultFlg3; set => difficultFlg3 = value; }
        public bool DifficultFlg4 { get => difficultFlg4; set => difficultFlg4 = value; }
        public bool DifficultFlg5 { get => difficultFlg5; set => difficultFlg5 = value; }
    }
}
