using UnityEngine;

namespace Entrance.Utils
{
    public static class KeyboardReleaseUtils
    {
        public static bool Enter()
        {
            return Input.GetKeyUp(KeyCode.Return)
                || Input.GetKeyUp(KeyCode.KeypadEnter);
        }
        public static bool Space()
        {
            return Input.GetKeyUp(KeyCode.Space);
        }
        public static bool Num0()
        {
            return Input.GetKeyUp(KeyCode.Keypad0);
        }
        public static bool Num1()
        {
            return Input.GetKeyUp(KeyCode.Keypad1);
        }
        public static bool Num2()
        {
            return Input.GetKeyUp(KeyCode.Keypad2);
        }
        public static bool Num3()
        {
            return Input.GetKeyUp(KeyCode.Keypad3);
        }
        public static bool Num4()
        {
            return Input.GetKeyUp(KeyCode.Keypad4);
        }
        public static bool Num5()
        {
            return Input.GetKeyUp(KeyCode.Keypad5);
        }
        public static bool Num6()
        {
            return Input.GetKeyUp(KeyCode.Keypad6);
        }
        public static bool Num7()
        {
            return Input.GetKeyUp(KeyCode.Keypad7);
        }
        public static bool Num8()
        {
            return Input.GetKeyUp(KeyCode.Keypad8);
        }
        public static bool Num9()
        {
            return Input.GetKeyUp(KeyCode.Keypad9);
        }
        public static bool All0()
        {
            return Input.GetKeyUp(KeyCode.Alpha0)
                || Input.GetKeyUp(KeyCode.Keypad0);
        }
        public static bool All1()
        {
            return Input.GetKeyUp(KeyCode.Alpha1)
                || Input.GetKeyUp(KeyCode.Keypad1);
        }
        public static bool All2()
        {
            return Input.GetKeyUp(KeyCode.Alpha2)
                || Input.GetKeyUp(KeyCode.Keypad2);
        }
        public static bool All3()
        {
            return Input.GetKeyUp(KeyCode.Alpha3)
                || Input.GetKeyUp(KeyCode.Keypad3);
        }
        public static bool All4()
        {
            return Input.GetKeyUp(KeyCode.Alpha4)
                || Input.GetKeyUp(KeyCode.Keypad4);
        }
        public static bool All5()
        {
            return Input.GetKeyUp(KeyCode.Alpha5)
                || Input.GetKeyUp(KeyCode.Keypad5);
        }
        public static bool All6()
        {
            return Input.GetKeyUp(KeyCode.Alpha6)
                || Input.GetKeyUp(KeyCode.Keypad6);
        }
        public static bool All7()
        {
            return Input.GetKeyUp(KeyCode.Alpha7)
                || Input.GetKeyUp(KeyCode.Keypad7);
        }
        public static bool All8()
        {
            return Input.GetKeyUp(KeyCode.Alpha8)
                || Input.GetKeyUp(KeyCode.Keypad8);
        }
        public static bool All9()
        {
            return Input.GetKeyUp(KeyCode.Alpha9)
                || Input.GetKeyUp(KeyCode.Keypad9);
        }
    }
}
