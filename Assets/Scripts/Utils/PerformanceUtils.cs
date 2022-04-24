using System;
using UnityEngine;

namespace Entrance.Utils
{
    public static class PerformanceUtils
    {
        public const int LOOP_1MAN = 10000;
        public const int LOOP_10MAN = 100000;
        public const int LOOP_100MAN = 1000000;
        public const int LOOP_1000MAN = 10000000;
        public const int LOOP_1OKU = 100000000;
        public const int LOOP_10OKU = 1000000000;

        private static System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

        public static void Run(int loopCount, Action action)
        {
            sw.Reset();
            sw.Start();
            for (int i = 0; i < loopCount; i++)
            {
                action();
            }
            sw.Stop();
            Debug.Log(sw.ElapsedMilliseconds + "ms");
        }
    }
}
