using UnityEngine;
using System.Collections.Generic;

using Entrance.Common;
using Entrance.Utils;

using UniRx;
using NCMB;

namespace Entrance.Model
{
    public class TitleModel
    {
        // **************************************************
        // [UniRx監視イベント]セッション情報
        // **************************************************
        private readonly ReactiveProperty<SessionCommon> sessionCommon = new ReactiveProperty<SessionCommon>();
        public IReadOnlyReactiveProperty<SessionCommon> SessionCommon => sessionCommon;

        // **************************************************
        // [UniRx監視イベント]通信成否結果
        // **************************************************
        private readonly ReactiveProperty<bool> isCommunicationFailure = new ReactiveProperty<bool>(false);
        public IReadOnlyReactiveProperty<bool> IsCommunicationFailure => isCommunicationFailure;

        // ********************************************************************************
        // セッションを取得する
        // ********************************************************************************
        public void LoadSession()
        {
            // 通信状況リセット
            isCommunicationFailure.Value = false;
            // セッション情報を検索
            NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>(ConstApp.SESSION_INFO_TABLE);
            query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
            {
                if (e == null)
                {
                    string deviceToken = DeviceUtils.GetDeviceToken();
                    int length = objList.Count;
                    for (int i = 0; i < length; i++)
                    {
                        NCMBObject sessionInfo = objList[i];
                        if (deviceToken.Equals(sessionInfo[ConstApp.DEVICE_TOKEN_COLUMN]))
                        {
                            SessionCommon session = JsonUtility.FromJson<SessionCommon>(sessionInfo[ConstApp.SESSION_INFO_TABLE].ToString());
                            session.ObjectId = sessionInfo.ObjectId;
                            sessionCommon.Value = session;
                            break;
                        }
                    }
                    // セッション情報がなければ自動生成
                    if (sessionCommon.Value == null)
                    {
                        sessionCommon.Value = new SessionCommon();
                    }
                }
                else
                {
                    // 通信失敗
                    isCommunicationFailure.Value = true;
                }
            });
        }

    }
}
