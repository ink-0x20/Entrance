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
        // [UniRx�Ď��C�x���g]�Z�b�V�������
        // **************************************************
        private readonly ReactiveProperty<SessionCommon> sessionCommon = new ReactiveProperty<SessionCommon>();
        public IReadOnlyReactiveProperty<SessionCommon> SessionCommon => sessionCommon;

        // **************************************************
        // [UniRx�Ď��C�x���g]�ʐM���ی���
        // **************************************************
        private readonly ReactiveProperty<bool> isCommunicationFailure = new ReactiveProperty<bool>(false);
        public IReadOnlyReactiveProperty<bool> IsCommunicationFailure => isCommunicationFailure;

        // ********************************************************************************
        // �Z�b�V�������擾����
        // ********************************************************************************
        public void LoadSession()
        {
            // �ʐM�󋵃��Z�b�g
            isCommunicationFailure.Value = false;
            // �Z�b�V������������
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
                    // �Z�b�V������񂪂Ȃ���Ύ�������
                    if (sessionCommon.Value == null)
                    {
                        sessionCommon.Value = new SessionCommon();
                    }
                }
                else
                {
                    // �ʐM���s
                    isCommunicationFailure.Value = true;
                }
            });
        }

    }
}
