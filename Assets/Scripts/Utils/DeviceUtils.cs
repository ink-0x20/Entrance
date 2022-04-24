using UnityEngine;

namespace Entrance.Utils
{
    public static class DeviceUtils
    {
        // **************************************************
        // デバイス情報
        // **************************************************
        private static string deviceToken = null;
        private const string deviceTokenSalt = "KiK-AwERcsAz3Bnm9AYW_9idEa2mn2PUTUumhLL8mfFHVND8Y4";

        // ********************************************************************************
        // デバイストークンを取得する
        // ********************************************************************************
        public static string GetDeviceToken()
        {
            if (string.IsNullOrEmpty(deviceToken))
            {
                // デバイスID + ソルト + デバイス名
                deviceToken = SystemInfo.deviceUniqueIdentifier + deviceTokenSalt + SystemInfo.deviceName;
                // ハッシュ化
                deviceToken = HashUtils.Sha512(deviceToken);
            }
            return deviceToken;
        }
    }
}
