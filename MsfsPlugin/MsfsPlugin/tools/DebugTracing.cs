namespace Loupedeck.MsfsPlugin.tools
{
    using System;
    using System.Diagnostics;
    using System.Threading;
    using System.Runtime.CompilerServices;

    internal static class DebugTracing
    {
        public static bool TracingEnabled => MsfsData.Instance.DEBUG;

        public static void Trace(string message, [CallerMemberName] string caller = null) => Tracing(caller, message);

        public static void Trace(Exception ex, [CallerMemberName] string caller = null) => Tracing(caller, ex.ToString());

        static void Tracing(string caller, string message)
        {
            if (TracingEnabled)
            {
                var threadId = Thread.CurrentThread.ManagedThreadId;
                Debug.WriteLine($"--> (thread {threadId}, method {caller}): {message}");
            }
        }
    }
}
