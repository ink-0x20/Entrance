using UnityEngine;

using Entrance.Common;
using Entrance.Model;

using NCMB;

namespace Entrance.Utils
{
    public static class DataStoreUtils
    {
        // ********************************************************************************
        // �Z�b�V�������f�[�^�X�g�A�ɕۑ�����
        // ********************************************************************************
        public static void SaveSession(SessionCommon sessionCommon)
        {
            if (!sessionCommon.DefaultLoadFlg)
            {
                // ********************************************************************************
                // �Z�b�V�����e�[�u�������`
                // ********************************************************************************
                NCMBObject sessionInfo = new NCMBObject(ConstApp.SESSION_INFO_TABLE);

                // ********************************************************************************
                // �V�K/�X�V����
                // ********************************************************************************
                if (string.IsNullOrEmpty(sessionCommon.ObjectId))
                {
                    // ********************************************************************************
                    // �V�K�ǉ�����
                    // ********************************************************************************
                    // �f�o�C�XID��ۑ�
                    sessionInfo[ConstApp.DEVICE_TOKEN_COLUMN] = DeviceUtils.GetDeviceToken();
                }
                else
                {
                    // ********************************************************************************
                    // �X�V����
                    // ********************************************************************************
                    // �I�u�W�F�N�gID��ݒ�
                    sessionInfo.ObjectId = sessionCommon.ObjectId;
                }
                // �Z�b�V������ۑ�
                sessionInfo[ConstApp.SESSION_INFO_TABLE] = JsonUtility.ToJson(sessionCommon);
                // �f�[�^�X�g�A�ւ̓o�^
                sessionInfo.SaveAsync((NCMBException e) =>
                {
                    if (e == null)
                    {
                        // �I�u�W�F�N�gID�ۑ�
                        sessionCommon.ObjectId = sessionInfo.ObjectId;
                    }
                    else
                    {
                        // �ʐM�G���[�_�C�A���O�\��
                        DialogHandler.ShowDialog(
                            "�ʐM�G���[",
                            "�ڑ����ł��܂���ł����B\n�d�g�󋵂̗ǂ��Ƃ���ōēx���������������B",
                            "OK");
                    }
                });
            }
        }

        // ********************************************************************************
        // ���O���f�[�^�X�g�A�ɏ�������
        // ********************************************************************************
        public static void WriteLog(string logMessage)
        {
            // ********************************************************************************
            // ���O�e�[�u�������`
            // ********************************************************************************
            NCMBObject logInfo = new NCMBObject(ConstApp.LOG_INFO_TABLE);
            // �f�o�C�XID��ۑ�
            logInfo[ConstApp.DEVICE_TOKEN_COLUMN] = DeviceUtils.GetDeviceToken();
            // ���O��ۑ�
            logInfo[ConstApp.LOG_INFO_TABLE] = logMessage;
            // �f�[�^�X�g�A�ւ̓o�^
            logInfo.SaveAsync();
        }
    }
}
