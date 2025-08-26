// File: MsfsPlugin/MsfsPlugin/tools/MobiSend.cs
namespace Loupedeck.MsfsPlugin.tools
{
    using Loupedeck.MsfsPlugin.msfs;

    internal static class MobiSend
    {
        // Fire a HubHop/MobiFlight preset: "MobiFlight.<PresetName>"
        public static void Preset(string presetName)
            => SimConnectWrapper.Instance.SendClientEvent($"MobiFlight.{presetName}", 0u);
    }
}
