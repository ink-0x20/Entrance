using UnityEngine;
using System.Collections.Generic;
using System.IO;

using Entrance.Common;
using Entrance.Model;
using Entrance.Utils;

using UniRx;

namespace Entrance.Prezenter
{
    public class ScenePrezenter : MonoBehaviour
    {
        // **************************************************
        // インスペクタオブジェクト
        // **************************************************
        // Windowプレハブ
        [SerializeField]
        private GameObject titleWindowPrefab = default;
        [SerializeField]
        private GameObject selectWindowPrefab = default;
        [SerializeField]
        private GameObject gameMusicWindowPrefab = default;
        [SerializeField]
        private GameObject resultWindowPrefab = default;
        // NCMBプレハブ
        [SerializeField]
        private GameObject NCMBSettingswPrefab = default;
        // ロード画面
        [SerializeField]
        private GameObject nowLoading = default;

        // **************************************************
        // ウィンドウ管理リスト
        // **************************************************
        private static Stack<WindowManage> windowManageStack = new Stack<WindowManage>();

        // ********************************************************************************
        // プログラムの起動
        // ********************************************************************************
        private void Start()
        {
            var application = Resources.Load<TextAsset>("ApplicationKey");
            var client = Resources.Load<TextAsset>("ClientKey");
            if (application == null || client == null)
            {
                // ********************************************************************************
                // サーバアクセスキーなしの場合はデフォルト設定で起動
                // ********************************************************************************
                // 初期セッションを生成
                SessionCommon sessionCommon = new SessionCommon();
                sessionCommon.DefaultLoadFlg = true;
                sessionCommon.OnLoaded();
                // タイトルスクリーンへ遷移
                ControllWindow(new ScreenDataLinkage(ScreenList.TitleScreen, sessionCommon));
            }
            else
            {
                // ********************************************************************************
                // サーバアクセスキーありの場合はサーバから設定取得し起動
                // ********************************************************************************
                // NCMBオブジェクト配置
                GameObject ncmb = Instantiate(NCMBSettingswPrefab);
                ncmb.name = "NCMBSettings";
                // タイトルスクリーンへ遷移
                ControllWindow(new ScreenDataLinkage(ScreenList.TitleScreen));
            }
        }

        // ********************************************************************************
        // 遷移先スクリーンからウィンドウを特定し管理する
        // ********************************************************************************
        public void ControllWindow(ScreenDataLinkage ScreenDataLinkage)
        {
            // 変数宣言
            WindowList nextWindow = WindowList.TitleWindow;
            bool destroyFlg = false;
            GameObject windowObject = null;
            BaseWindowPrezenter baseWindowPrezenter = null;

            // ロードテキスト表示
            nowLoading.SetActive(true);

            // 遷移先スクリーンから、対応するウィンドウを特定
            nextWindow = SceneUtils.GetWindowName(ScreenDataLinkage.NextScreen);
            destroyFlg = SceneUtils.IsWindowDestroy(nextWindow);

            if (ScreenDataLinkage.NextScreen == ScreenList.TitleScreen)
            {
                // タイトル画面の場合、全ウィンドウ・スクリーンをリセット
                while (windowManageStack.Count != 0)
                {
                    WindowManage windowInfo = windowManageStack.Pop();
                    // 破棄前処理
                    windowInfo.BaseWindowPrezenter.OnWindowClose();
                    // 全スクリーン破棄
                    windowInfo.BaseWindowPrezenter.AllScreenUnload();
                    // ウィンドウ破棄
                    Destroy(windowInfo.WindowObject);
                }
            }
            else
            {
                // ウィンドウ存在判定
                foreach (WindowManage windowInfo in windowManageStack)
                {
                    if (windowInfo.WindowName.Equals(nextWindow))
                    {
                        windowObject = windowInfo.WindowObject;
                        baseWindowPrezenter = windowInfo.BaseWindowPrezenter;
                        break;
                    }
                }
            }

            if (baseWindowPrezenter == null)
            {
                // ********************************************************************************
                // 前のウィンドウを調整する
                // ********************************************************************************
                if (windowManageStack.Count != 0)
                {
                    if (destroyFlg)
                    {
                        WindowManage windowInfo = windowManageStack.Pop();
                        // 破棄前処理
                        windowInfo.BaseWindowPrezenter.OnWindowClose();
                        // 全スクリーン破棄
                        windowInfo.BaseWindowPrezenter.AllScreenUnload();
                        // ウィンドウ破棄
                        Destroy(windowInfo.WindowObject);
                    }
                    else
                    {
                        WindowManage windowInfo = windowManageStack.Peek();
                        // ウィンドウ非表示前処理
                        windowInfo.BaseWindowPrezenter.OnWindowHide();
                        // ウィンドウ非表示
                        windowInfo.WindowObject.SetActive(false);
                    }
                }

                // ********************************************************************************
                // ウィンドウを生成する
                // ********************************************************************************
                switch (nextWindow)
                {
                    case WindowList.SelectWindow:
                        windowObject = Instantiate(selectWindowPrefab, Vector3.zero, Quaternion.identity);
                        break;
                    case WindowList.MusicGameWindow:
                        windowObject = Instantiate(gameMusicWindowPrefab, Vector3.zero, Quaternion.identity);
                        break;
                    case WindowList.ResultWindow:
                        windowObject = Instantiate(resultWindowPrefab, Vector3.zero, Quaternion.identity);
                        break;
                    default:
                        windowObject = Instantiate(titleWindowPrefab, Vector3.zero, Quaternion.identity);
                        break;
                }

                // ********************************************************************************
                // 新しいウィンドウへ遷移する
                // ********************************************************************************
                // ウィンドウの初期処理
                baseWindowPrezenter = windowObject.GetComponent<BaseWindowPrezenter>();
                baseWindowPrezenter.ScreenDataLinkage
                    .Skip(1)
                    .Subscribe(ControllWindow)
                    .AddTo(gameObject);
                // キャッシュ
                windowManageStack.Push(new WindowManage(nextWindow, windowObject, baseWindowPrezenter));
                // 遷移
                baseWindowPrezenter.WindowOpenSync(nextWindow, ScreenDataLinkage);
            }
            else
            {
                // ********************************************************************************
                // 既存ウィンドウへ遷移する
                // ********************************************************************************
                baseWindowPrezenter.SameWindowSync(ScreenDataLinkage);
            }
        }
    }
}
