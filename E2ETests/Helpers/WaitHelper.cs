using System;
using System.Threading;

namespace E2ETests.Helpers
{
    public static class WaitHelper
    {
        public static void ShortWait() => Thread.Sleep(1000);
        public static void MediumWait() => Thread.Sleep(5000);
        public static void LongWait() => Thread.Sleep(10000);

        public static void CustomWait(int milliseconds) => Thread.Sleep(milliseconds);
    }
}
