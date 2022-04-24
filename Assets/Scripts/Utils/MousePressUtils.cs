using UnityEngine;

namespace Entrance.Utils
{
    public static class MousePressUtils
    {
        public static bool Left()
        {
            return Input.GetMouseButtonDown(0);
        }
        public static bool Right()
        {
            return Input.GetMouseButtonDown(1);
        }
    }
}
