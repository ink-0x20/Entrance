using UnityEngine;

using Entrance.Common;
using Entrance.Model;
using Entrance.View;
using Entrance.Utils;

using UniRx;

namespace Entrance.Prezenter
{
    public class MusicGamePrezenter : BaseScreenPrezenter
    {
        // **************************************************
        // View
        // **************************************************
        [SerializeField]
        private MusicGameView musicGameView = default;

        // **************************************************
        // Model
        // **************************************************
        private MusicGameModel musicGameModel = new MusicGameModel();
        private NotesSE notesSE = new NotesSE();

        // ********************************************************************************
        // 初期化時に呼ばれます
        // ********************************************************************************
        public override void Initialize()
        {
            // ********************************************************************************
            // Viewの初期化（UIをアニメーション前の状態にする）
            // ********************************************************************************
            musicGameView.Initialize(sessionCommon);
            notesSE.Initialize(sessionCommon);

            // ********************************************************************************
            // 追加で生成するべきUIパーツの生成と配置
            // ********************************************************************************

            // ********************************************************************************
            // Bind処理（Modelの値の変更の監視設定）
            // ********************************************************************************
            // コンボテキスト
            musicGameModel.Combo
                .Subscribe(musicGameView.Combo)
                .AddTo(gameObject);
            // スコア
            musicGameModel.Score
                .Subscribe(musicGameView.Score)
                .AddTo(gameObject);

            // ********************************************************************************
            // SetEvents処理（Viewのイベントの監視設定）
            // ********************************************************************************
            // ノーツ判定イベント
            musicGameView.perfectListener = () => musicGameModel.PlusPerfect();
            musicGameView.greatListener = () => musicGameModel.PlusGreat();
            musicGameView.goodListener = () => musicGameModel.PlusGood();
            musicGameView.badListener = () => musicGameModel.PlusBad();
            musicGameView.missListener = () => musicGameModel.PlusMiss();
            // コンボテキスト追加イベント
            musicGameView.comboPlusListener = () => musicGameModel.PlusCombo();
            musicGameView.comboResetListener = () => musicGameModel.ResetCombo();
            // スコアイベント
            musicGameView.normalScoreListener = () => musicGameModel.NormalScore();
            musicGameView.feverScoreListener = () => musicGameModel.FeverScore();
            // SEイベント
            musicGameView.normalNotesSE = () => notesSE.PlayNormalNotesSE();
            // リザルト画面遷移イベント
            musicGameView.resultScene = () =>
            {
                // 結果を保存
                sessionCommon.AddScoreHistory(new ScoreDetail(
                    musicGameModel.Score.Value,
                    ScoreRankUtils.GetScoreRank(musicGameModel.Score.Value, musicGameView.maxScore),
                    musicGameModel.Perfect,
                    musicGameModel.Great,
                    musicGameModel.Good,
                    musicGameModel.Bad,
                    musicGameModel.Miss,
                    musicGameModel.MaxCombo,
                    musicGameModel.MaxCombo == musicGameView.maxCombo,
                    musicGameModel.Perfect == musicGameView.maxCombo));
                // 次画面へ遷移
                screenDataLinkage.Value = new ScreenDataLinkage(ScreenList.ResultScreen, sessionCommon);
            };

            // ********************************************************************************
            // 画面の描画に必要な追加通信
            // ********************************************************************************

            // ********************************************************************************
            // ライブ開始の準備
            // ********************************************************************************
            // 楽曲を取得し設定
            AudioClip music = Resources.Load<AudioClip>(ResourcesPathUtils.Music(sessionCommon));
            Debug.Log(music.samples);
            // ここから基準時間計測開始
            double baseTime = AudioSettings.dspTime + SystemConfig.MUSIC_START_OFFSET;
            // 基準時間を譜面データに反映
            musicGameView.AddOffset(baseTime, music.length, sessionCommon);
            // タイミング設定によって、楽曲再生開始時間を調整
            baseTime += sessionCommon.TimingTime;
            // 楽曲再生予約
            AudioUtils.PlayScheduledLiveMusic(music, sessionCommon.LiveVolumeBGM, baseTime + sessionCommon.NotesMovingTime + sessionCommon.TimingTime);
        }

        // ********************************************************************************
        // OnScreenOepnの直前に呼ばれます
        // ********************************************************************************
        public override void OnScreenOepnBefore()
        {
            // ********************************************************************************
            // InitializeとOnScreenOepnBeforeの間で、前のScreenからパラメータが必要に応じて渡されてきています。
            // ********************************************************************************

            // ********************************************************************************
            // ここでは、初期化後に渡されたパラメータに応じて、画面が表示される前に済ませておきたい処理を行う
            // ********************************************************************************

            // ********************************************************************************
            // 渡されたパラメータをModelに適応
            // ********************************************************************************

            // ********************************************************************************
            // 渡されたパラメータに依存した追加UI生成
            // ********************************************************************************

            // ********************************************************************************
            // 渡されたパラメータに依存した追加通信
            // ********************************************************************************
        }

        // ********************************************************************************
        // 自身に遷移してくる際に呼ばれます
        // ********************************************************************************
        public override void OnScreenOepn()
        {
            // ********************************************************************************
            // UIを表示するアニメーションの再生
            // ********************************************************************************
        }

        // ********************************************************************************
        // OnScreenOepnの直後に呼ばれます
        // ********************************************************************************
        public override void OnScreenOepnAfter()
        {
            // ********************************************************************************
            // 特になし
            // ********************************************************************************
        }

        // ********************************************************************************
        // 次のScreenに遷移する際に呼ばれます
        // ********************************************************************************
        public override void OnScreenChange()
        {
            // ********************************************************************************
            // UIを非表示にするアニメーションの再生
            // ********************************************************************************
        }

        // ********************************************************************************
        // 前のScreenに戻る際に読まれます
        // ********************************************************************************
        public override void OnScreenBackSrc()
        {
            // ********************************************************************************
            // UIを非表示にするアニメーションの再生
            // ********************************************************************************
        }

        // ********************************************************************************
        // OnScreenBackDstの直前に呼ばれます
        // ********************************************************************************
        public override void OnScreenBackDstBefore()
        {
            // ********************************************************************************
            // 特になし
            // ********************************************************************************
        }

        // ********************************************************************************
        // 戻るによって自身に遷移してくる際に呼ばれます
        // ********************************************************************************
        public override void OnScreenBackDst()
        {
            // ********************************************************************************
            // UIを表示するアニメーションの再生
            // ********************************************************************************
        }

        // ********************************************************************************
        // 自身を管理しているWindowが閉じる際に呼ばれます
        // ********************************************************************************
        public override void OnWindowClose()
        {
            // ********************************************************************************
            // 特になし
            // ********************************************************************************
        }

        // ********************************************************************************
        // Androidバックキーが押された際に呼ばれます
        // ********************************************************************************
        public override void AndroidBack()
        {
            // ********************************************************************************
            // 特になし
            // ********************************************************************************
        }
    }
}
