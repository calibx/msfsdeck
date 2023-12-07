namespace Loupedeck.MsfsPlugin.tools
{
    using System;
    using System.Diagnostics;
    using System.Threading;
    using System.Runtime.CompilerServices;

    internal static class DebugTracing
    {
        /// <summary>
        /// Set this field to true when you want to trace what happens.
        /// </summary>
        public const bool tracingEnabled = false;

        public static void Trace(string message, [CallerMemberName] string caller = null) => Tracing(caller, message);

        public static void Trace(Exception ex, [CallerMemberName] string caller = null) => Tracing(caller, ex.ToString());

        static void Tracing(string caller, string message)
        {
            if (tracingEnabled)
            {
                var threadId = Thread.CurrentThread.ManagedThreadId;
                Debug.WriteLine($"--> (thread {threadId}, method {caller}): {message}");
            }
        }
    }
}
