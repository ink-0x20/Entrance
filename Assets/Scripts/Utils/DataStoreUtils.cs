using UnityEngine;

using Entrance.Common;
using Entrance.Model;

using NCMB;

namespace Entrance.Utils
{
    public static class DataStoreUtils
    {
        // ********************************************************************************
        // セッションをデータストアに保存する
        // ********************************************************************************
        public static void SaveSession(SessionCommon sessionCommon)
        {
            if (!sessionCommon.DefaultLoadFlg)
            {
                // ********************************************************************************
                // セッションテーブル情報を定義
                // ********************************************************************************
                NCMBObject sessionInfo = new NCMBObject(ConstApp.SESSION_INFO_TABLE);

                // ********************************************************************************
                // 新規/更新判定
                // ********************************************************************************
                if (string.IsNullOrEmpty(sessionCommon.ObjectId))
                {
                    // ********************************************************************************
                    // 新規追加処理
                    // ********************************************************************************
                    // デバイスIDを保存
                    sessionInfo[ConstApp.DEVICE_TOKEN_COLUMN] = DeviceUtils.GetDeviceToken();
                }
                else
                {
                    // ********************************************************************************
                    // 更新処理
                    // ********************************************************************************
                    // オブジェクトIDを設定
                    sessionInfo.ObjectId = sessionCommon.ObjectId;
                }
                // セッションを保存
                sessionInfo[ConstApp.SESSION_INFO_TABLE] = JsonUtility.ToJson(sessionCommon);
                // データストアへの登録
                sessionInfo.SaveAsync((NCMBException e) =>
                {
                    if (e == null)
                    {
                        // オブジェクトID保存
                        sessionCommon.ObjectId = sessionInfo.ObjectId;
                    }
                    else
                    {
                        // 通信エラーダイアログ表示
                        DialogHandler.ShowDialog(
                            "通信エラー",
                            "接続ができませんでした。\n電波状況の良いところで再度お試しください。",
                            "OK");
                    }
                });
            }
        }

        // ********************************************************************************
        // ログをデータストアに書き込む
        // ********************************************************************************
        public static void WriteLog(string logMessage)
        {
            // ********************************************************************************
            // ログテーブル情報を定義
            // ********************************************************************************
            NCMBObject logInfo = new NCMBObject(ConstApp.LOG_INFO_TABLE);
            // デバイスIDを保存
            logInfo[ConstApp.DEVICE_TOKEN_COLUMN] = DeviceUtils.GetDeviceToken();
            // ログを保存
            logInfo[ConstApp.LOG_INFO_TABLE] = logMessage;
            // データストアへの登録
            logInfo.SaveAsync();
        }
    }
}
