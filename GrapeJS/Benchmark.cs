using System;
using System.Collections.Generic;

namespace GrapeJS
{
    public static class SBenchmark
    {
        public static List<TimeSpan> scores = new List<TimeSpan>();
        private static bool isRunning = false;

        private static DateTime start;

        public static void Start()
        {
            if (isRunning)
                return;
            isRunning = true;

            start = DateTime.Now;
        }

        public static void End()
        {
            if (!isRunning)
                return;

            scores.Add(DateTime.Now - start);
            isRunning = false;
        }
    }
}
