using System;
using UnityEngine;

using Entrance.Common;
using Entrance.Model;

using UniRx;

namespace Entrance.Prezenter
{
    public abstract class BaseScreenPrezenter : MonoBehaviour
    {
        // **************************************************
        // スクリーン遷移イベント
        // **************************************************
        public Action screenChangeListener;

        // **************************************************
        // [UniRx監視イベント]スクリーン遷移時のデータ連携イベント
        // **************************************************
        protected readonly ReactiveProperty<ScreenDataLinkage> screenDataLinkage = new ReactiveProperty<ScreenDataLinkage>();
        public IReadOnlyReactiveProperty<ScreenDataLinkage> ScreenDataLinkage => screenDataLinkage;

        // **************************************************
        // 共通セッション
        // **************************************************
        protected SessionCommon sessionCommon = null;

        // ********************************************************************************
        // abstractメソッド定義
        // ********************************************************************************
        // 初期化時に呼ばれます
        public abstract void Initialize();

        // OnScreenOepnの直前に呼ばれます
        public abstract void OnScreenOepnBefore();
        // 自身に遷移してくる際に呼ばれます
        public abstract void OnScreenOepn();
        // OnScreenOepnの直後に呼ばれます
        public abstract void OnScreenOepnAfter();

        // 次のScreenに遷移する際に呼ばれます
        public abstract void OnScreenChange();

        // 前のScreenに戻る際に読まれます
        public abstract void OnScreenBackSrc();

        // OnScreenBackDstの直前に呼ばれます
        public abstract void OnScreenBackDstBefore();
        // 戻るによって自身に遷移してくる際に呼ばれます
        public abstract void OnScreenBackDst();

        // 自身を管理しているWindowが閉じる際に呼ばれます
        public abstract void OnWindowClose();
        // Androidバックキーが押された際に呼ばれます
        public abstract void AndroidBack();

        // ********************************************************************************
        // ウィンドウ初期起動時にイベントを順番に処理する
        // ********************************************************************************
        public void ScreenOpenSync(SessionCommon sessionCommon)
        {
            // セッションを保存
            this.sessionCommon = sessionCommon;
            // 初期処理
            Initialize();
            // スクリーン生成前処理
            OnScreenOepnBefore();
            // スクリーン生成
            OnScreenOepn();
            // スクリーン生成後処理
            OnScreenOepnAfter();
        }

        // ********************************************************************************
        // スクリーン表示を切り替える
        // ********************************************************************************
        public void SetActive(bool activeFlg)
        {
            gameObject.SetActive(activeFlg);
            if (!activeFlg)
            {
                screenDataLinkage.Value = new ScreenDataLinkage(activeFlg);
            }
        }
    }
}
