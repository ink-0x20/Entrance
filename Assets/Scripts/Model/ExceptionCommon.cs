using UnityEngine;
using UnityEngine.UI;

using Entrance.Common;
using Entrance.Model;
using Entrance.Prezenter;
using Entrance.Utils;

namespace Entrance.Exception
{
    public class ExceptionCommon : MonoBehaviour
    {
        // ********************************************************************************
        // 起動時に例外処理を登録する
        // ********************************************************************************
        void Awake()
        {
            Application.logMessageReceivedThreaded += HandleException;
        }

        // ********************************************************************************
        // 共通例外処理を定義する
        // ********************************************************************************
        void HandleException(string logString, string stackTrace, LogType type)
        {
            if (type == LogType.Exception)
            {
                // データストアにログを書き込む
                string logMessage = "[" + type.ToString() + "]" + logString + "|" + stackTrace;
                DataStoreUtils.WriteLog(logMessage);
                // 通信エラーダイアログ表示
                Button dialogButton = DialogHandler.ShowDialog(
                    "エラー",
                    "エラーが発生しました。\nタイトルに戻ります。",
                    "OK");
                dialogButton.onClick.AddListener(() =>
                {
                    // タイトルへ遷移
                    ScenePrezenter scenePrezenter = GameObject.Find("Scene").GetComponent<ScenePrezenter>();
                    scenePrezenter.ControllWindow(new ScreenDataLinkage(ScreenList.TitleScreen));
                });
            }
        }
    }
}
