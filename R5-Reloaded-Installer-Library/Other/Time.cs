using System.Diagnostics;

namespace R5_Reloaded_Installer_Library.Other
{
    public static class Time
    {
        private static Stopwatch stopWatch = Stopwatch.StartNew();
        private static float timeBuffer = nowTime;
        private static float nowTime => stopWatch.ElapsedTicks * 0.0001f * 0.001f;
        private static float getDelta()
        {
            var oldTime = timeBuffer;
            timeBuffer = nowTime;
            return timeBuffer - oldTime;
        }
        public static float deltaTime => getDelta();
    }
}
