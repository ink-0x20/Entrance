using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

using Entrance.Common;
using Entrance.Model;
using Entrance.Utils;

using UniRx;

namespace Entrance.Prezenter
{
    public abstract class BaseWindowPrezenter : MonoBehaviour
    {
        // **************************************************
        // 共通セッション
        // **************************************************
        protected SessionCommon sessionCommon = null;

        // **************************************************
        // スクリーン管理
        // **************************************************
        private Stack<ScreenList> screenManageStack = new Stack<ScreenList>();
        private Dictionary<ScreenList, BaseScreenPrezenter> inactiveScreenManage = new Dictionary<ScreenList, BaseScreenPrezenter>();

        // **************************************************
        // [UniRx監視イベント]スクリーン遷移時のデータ連携イベント
        // **************************************************
        protected readonly ReactiveProperty<ScreenDataLinkage> screenDataLinkage = new ReactiveProperty<ScreenDataLinkage>();
        public IReadOnlyReactiveProperty<ScreenDataLinkage> ScreenDataLinkage => screenDataLinkage;

        // **************************************************
        // abstractメソッド定義
        // **************************************************
        // 初期化時に呼ばれます
        public abstract void Initialize();
        // OnWindowOpenの直前に呼ばれます
        public abstract void OnWindowOpenBefore();
        // 自身が開かれる際に呼ばれます
        public abstract void OnWindowOpen();

        // OnWindowBackの直前に呼ばれます
        public abstract void OnWindowBackBefore();
        // 他のWindowが閉じることによって、自身が画面に表示される時に呼ばれます
        public abstract void OnWindowBack();

        // 他のWindowが開かれることによって、自身が画面から消える時に呼ばれます
        public abstract void OnWindowHide();
        // 自身を閉じることによって、自身が画面から消える時に呼ばれます
        public abstract void OnWindowClose();

        // 自身が管理しているスクリーンが遷移し始めた時に呼ばれます
        public abstract void OnScreenChangeBefore();
        // 自身が管理しているスクリーンが遷移し終わった時に呼ばれます
        public abstract void OnScreenChangeAfter();

        // ********************************************************************************
        // ウィンドウ初期起動時にイベントを順番に処理する
        // ********************************************************************************
        public void WindowOpenSync(WindowList window, ScreenDataLinkage screenDataLinkage)
        {
            // スクリーンキャッシュ
            screenManageStack.Push(screenDataLinkage.NextScreen);
            // 初期処理
            Initialize();
            // ウィンドウ生成前処理
            OnWindowOpenBefore();
            // ウィンドウ生成
            OnWindowOpen();
            // 遷移前処理
            OnScreenChangeBefore();
            // セッション保持
            this.sessionCommon = screenDataLinkage.SessionCommon;
            // シーン遷移
            SceneManager.sceneLoaded += ScreenLoaded;
            SceneManager.LoadScene(screenDataLinkage.NextScreen.ToString(), LoadSceneMode.Additive);
        }

        // ********************************************************************************
        // ウィンドウ内遷移時にイベントを順番に処理する
        // ********************************************************************************
        public void SameWindowSync(ScreenDataLinkage screenDataLinkage)
        {
            // 遷移前処理
            OnScreenChangeBefore();
            // セッション保持
            this.sessionCommon = screenDataLinkage.SessionCommon;
            // スクリーン存在判定
            if (screenManageStack.Peek() == screenDataLinkage.NextScreen)
            {
                // 再表示
                BaseScreenPrezenter baseScreenPrezenter = inactiveScreenManage[screenDataLinkage.NextScreen];
                baseScreenPrezenter.SetActive(true);
                // ロード画面非表示
                GameObject.Find("NowLoading").SetActive(false);
                // 戻るによって自身に遷移する前の処理
                baseScreenPrezenter.OnScreenBackDstBefore();
                // 戻るによって自身に遷移する時の処理
                baseScreenPrezenter.OnScreenBackDst();
                // プレゼンターキャッシュ削除
                inactiveScreenManage.Remove(screenDataLinkage.NextScreen);
            }
            else
            {
                // スクリーンキャッシュ
                screenManageStack.Push(screenDataLinkage.NextScreen);
                // シーン遷移
                SceneManager.sceneLoaded += ScreenLoaded;
                SceneManager.LoadScene(screenDataLinkage.NextScreen.ToString(), LoadSceneMode.Additive);
            }
        }

        // ********************************************************************************
        // スクリーンを起動時にイベントを順番に処理する
        // ********************************************************************************
        private void ScreenLoaded(Scene scene, LoadSceneMode mode)
        {
            BaseScreenPrezenter baseScreenPrezenter = scene.GetRootGameObjects()[0].GetComponent<BaseScreenPrezenter>();
            baseScreenPrezenter.ScreenDataLinkage
                .Skip(1)
                .Subscribe((screenDataLinkage) =>
                {
                    if (screenDataLinkage.ActiveFlg)
                    {
                        // ********************************************************************************
                        // 遷移イベント登録
                        // ********************************************************************************
                        if (screenManageStack.Count != 0)
                        {
                            // 前のスクリーンの遷移前処理
                            baseScreenPrezenter.OnScreenChange();
                            // 遷移情報取得
                            ScreenList previousScreen = screenManageStack.Peek();
                            WindowList previousWindow = SceneUtils.GetWindowName(previousScreen);
                            WindowList nextWindow = SceneUtils.GetWindowName(screenDataLinkage.NextScreen);
                            if (previousWindow == nextWindow)
                            {
                                if (screenManageStack.Contains(screenDataLinkage.NextScreen))
                                {
                                    // ********************************************************************************
                                    // 同じウィンドウ・既存スクリーン
                                    // ********************************************************************************
                                    // 遷移順に戻していく
                                    ScreenList backScreen = screenManageStack.Pop();
                                    while (screenManageStack.Count != 0)
                                    {
                                        // 同じウィンドウで、既存スクリーンのため、スクリーンのみ破棄
                                        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(backScreen.ToString()), UnloadSceneOptions.None);
                                        if (screenManageStack.Peek() == screenDataLinkage.NextScreen)
                                        {
                                            // 遷移先まで来たら終了
                                            break;
                                        }
                                        backScreen = screenManageStack.Pop();
                                    }
                                }
                                else
                                {
                                    // ********************************************************************************
                                    // 同じウィンドウ・新規スクリーン
                                    // ********************************************************************************
                                    // プレゼンターキャッシュ
                                    inactiveScreenManage.Add(previousScreen, baseScreenPrezenter);
                                    // 同じウィンドウで、新しいスクリーンのため非表示
                                    baseScreenPrezenter.SetActive(false);
                                }
                            }
                            else if (SceneUtils.IsWindowDestroy(nextWindow))
                            {
                                // ********************************************************************************
                                // 違うウィンドウ・破棄
                                // ********************************************************************************
                                // 違うウィンドウのため破棄前処理
                                baseScreenPrezenter.OnWindowClose();
                                // 破棄
                                SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(screenManageStack.Pop().ToString()), UnloadSceneOptions.None);
                            }
                            else
                            {
                                // ********************************************************************************
                                // 違うウィンドウ・非表示
                                // ********************************************************************************
                                // プレゼンターキャッシュ
                                inactiveScreenManage.Add(previousScreen, baseScreenPrezenter);
                                // 違うウィンドウだが破棄しないため非表示
                                baseScreenPrezenter.SetActive(false);
                            }
                            // シーンにデータ連携
                            this.screenDataLinkage.Value = screenDataLinkage;
                        }
                    }
                })
                .AddTo(baseScreenPrezenter.gameObject);
            // 遷移
            baseScreenPrezenter.ScreenOpenSync(sessionCommon);
            // 遷移後処理
            OnScreenChangeAfter();
            // ロード画面非表示
            GameObject.Find("NowLoading").SetActive(false);
            // 1回のみの実行のため削除
            SceneManager.sceneLoaded -= ScreenLoaded;
        }

        // ********************************************************************************
        // 現在生成中のスクリーンをすべて破棄する
        // ********************************************************************************
        public void AllScreenUnload()
        {
            while (screenManageStack.Count != 0)
            {
                SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(screenManageStack.Pop().ToString()), UnloadSceneOptions.None);
            }
        }
    }
}
