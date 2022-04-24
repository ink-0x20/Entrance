using UnityEngine;

namespace Entrance.Utils
{
    public static class DeviceUtils
    {
        // **************************************************
        // �f�o�C�X���
        // **************************************************
        private static string deviceToken = null;
        private const string deviceTokenSalt = "KiK-AwERcsAz3Bnm9AYW_9idEa2mn2PUTUumhLL8mfFHVND8Y4";

        // ********************************************************************************
        // �f�o�C�X�g�[�N�����擾����
        // ********************************************************************************
        public static string GetDeviceToken()
        {
            if (string.IsNullOrEmpty(deviceToken))
            {
                // �f�o�C�XID + �\���g + �f�o�C�X��
                deviceToken = SystemInfo.deviceUniqueIdentifier + deviceTokenSalt + SystemInfo.deviceName;
                // �n�b�V����
                deviceToken = HashUtils.Sha512(deviceToken);
            }
            return deviceToken;
        }
    }
}
