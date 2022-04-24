using UnityEngine;

namespace Entrance.Model
{
    public struct MusicListModel
    {
        // **************************************************
        // 表示用楽曲名
        // **************************************************
        private Sprite musicIcon1;
        private Sprite musicIcon2;
        private Sprite musicIcon3;
        private Sprite musicIcon4;
        private Sprite musicIcon5;

        // **************************************************
        // コンストラクタ
        // **************************************************
        public MusicListModel(Sprite musicIcon1, Sprite musicIcon2, Sprite musicIcon3, Sprite musicIcon4, Sprite musicIcon5)
        {
            this.musicIcon1 = musicIcon1;
            this.musicIcon2 = musicIcon2;
            this.musicIcon3 = musicIcon3;
            this.musicIcon4 = musicIcon4;
            this.musicIcon5 = musicIcon5;
        }

        public Sprite MusicIcon1 { get => musicIcon1; set => musicIcon1 = value; }
        public Sprite MusicIcon2 { get => musicIcon2; set => musicIcon2 = value; }
        public Sprite MusicIcon3 { get => musicIcon3; set => musicIcon3 = value; }
        public Sprite MusicIcon4 { get => musicIcon4; set => musicIcon4 = value; }
        public Sprite MusicIcon5 { get => musicIcon5; set => musicIcon5 = value; }
    }
}
