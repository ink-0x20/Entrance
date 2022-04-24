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
        // �N�����ɗ�O������o�^����
        // ********************************************************************************
        void Awake()
        {
            Application.logMessageReceivedThreaded += HandleException;
        }

        // ********************************************************************************
        // ���ʗ�O�������`����
        // ********************************************************************************
        void HandleException(string logString, string stackTrace, LogType type)
        {
            if (type == LogType.Exception)
            {
                // �f�[�^�X�g�A�Ƀ��O����������
                string logMessage = "[" + type.ToString() + "]" + logString + "|" + stackTrace;
                DataStoreUtils.WriteLog(logMessage);
                // �ʐM�G���[�_�C�A���O�\��
                Button dialogButton = DialogHandler.ShowDialog(
                    "�G���[",
                    "�G���[���������܂����B\n�^�C�g���ɖ߂�܂��B",
                    "OK");
                dialogButton.onClick.AddListener(() =>
                {
                    // �^�C�g���֑J��
                    ScenePrezenter scenePrezenter = GameObject.Find("Scene").GetComponent<ScenePrezenter>();
                    scenePrezenter.ControllWindow(new ScreenDataLinkage(ScreenList.TitleScreen));
                });
            }
        }
    }
}
