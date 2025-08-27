// File: MsfsPlugin/MsfsPlugin/tools/MobiSend.cs
// Reflection bridge to call SimConnectWrapper.SendMobiFlightPreset(string) if present.

namespace Loupedeck.MsfsPlugin.tools
{
    using System.Reflection;
    using Loupedeck.MsfsPlugin.msfs;

    public static class MobiSend
    {
        private static MethodInfo _sendPreset;
        private static object _instance;

        private static void Ensure()
        {
            if (_sendPreset != null) return;
            _instance = SimConnectWrapper.Instance;
            if (_instance == null) return;
            // Look for a method you (or the wrapper) may expose:
            //   public void SendMobiFlightPreset(string presetName)
            _sendPreset = _instance.GetType().GetMethod(
                "SendMobiFlightPreset",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                binder: null,
                types: new[] { typeof(string) },
                modifiers: null
            );
        }

        /// <summary>
        /// Try to send a MobiFlight preset by name. Returns true if invoked, false otherwise.
        /// </summary>
        public static bool Send(string presetName)
        {
            Ensure();
            if (_sendPreset == null || _instance == null) return false;
            try
            {
                _sendPreset.Invoke(_instance, new object[] { presetName });
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
