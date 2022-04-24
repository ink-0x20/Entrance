using UnityEngine;

namespace Entrance.Utils
{
    public static class KeyboardPressUtils
    {
        public static void ExitApplication()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
                Application.Quit();
#endif
            }
        }

        public static bool Enter()
        {
            return
                   Input.GetKeyDown(KeyCode.Return)
                || Input.GetKeyDown(KeyCode.KeypadEnter);
        }
        public static bool Space()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }
        public static bool Num0()
        {
            return Input.GetKeyDown(KeyCode.Keypad0);
        }
        public static bool Num1()
        {
            return Input.GetKeyDown(KeyCode.Keypad1);
        }
        public static bool Num2()
        {
            return Input.GetKeyDown(KeyCode.Keypad2);
        }
        public static bool Num3()
        {
            return Input.GetKeyDown(KeyCode.Keypad3);
        }
        public static bool Num4()
        {
            return Input.GetKeyDown(KeyCode.Keypad4);
        }
        public static bool Num5()
        {
            return Input.GetKeyDown(KeyCode.Keypad5);
        }
        public static bool Num6()
        {
            return Input.GetKeyDown(KeyCode.Keypad6);
        }
        public static bool Num7()
        {
            return Input.GetKeyDown(KeyCode.Keypad7);
        }
        public static bool Num8()
        {
            return Input.GetKeyDown(KeyCode.Keypad8);
        }
        public static bool Num9()
        {
            return Input.GetKeyDown(KeyCode.Keypad9);
        }
        public static bool All0()
        {
            return Input.GetKeyDown(KeyCode.Alpha0)
                || Input.GetKeyDown(KeyCode.Keypad0);
        }
        public static bool All1()
        {
            return Input.GetKeyDown(KeyCode.Alpha1)
                || Input.GetKeyDown(KeyCode.Keypad1);
        }
        public static bool All2()
        {
            return Input.GetKeyDown(KeyCode.Alpha2)
                || Input.GetKeyDown(KeyCode.Keypad2);
        }
        public static bool All3()
        {
            return Input.GetKeyDown(KeyCode.Alpha3)
                || Input.GetKeyDown(KeyCode.Keypad3);
        }
        public static bool All4()
        {
            return Input.GetKeyDown(KeyCode.Alpha4)
                || Input.GetKeyDown(KeyCode.Keypad4);
        }
        public static bool All5()
        {
            return Input.GetKeyDown(KeyCode.Alpha5)
                || Input.GetKeyDown(KeyCode.Keypad5);
        }
        public static bool All6()
        {
            return Input.GetKeyDown(KeyCode.Alpha6)
                || Input.GetKeyDown(KeyCode.Keypad6);
        }
        public static bool All7()
        {
            return Input.GetKeyDown(KeyCode.Alpha7)
                || Input.GetKeyDown(KeyCode.Keypad7);
        }
        public static bool All8()
        {
            return Input.GetKeyDown(KeyCode.Alpha8)
                || Input.GetKeyDown(KeyCode.Keypad8);
        }
        public static bool All9()
        {
            return Input.GetKeyDown(KeyCode.Alpha9)
                || Input.GetKeyDown(KeyCode.Keypad9);
        }
    }
}
