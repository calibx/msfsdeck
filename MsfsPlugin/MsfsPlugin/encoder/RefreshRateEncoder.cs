namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.encoder;

    class RefreshRateEncoder : DefaultEncoder
    {
        public RefreshRateEncoder() : base("Refresh", "Current data refresh rate", "Misc", true, 100, 1000, 100)
        {
        }
        protected override void RunCommand(String actionParameter) => MsfsData.Instance.RefreshRate = 500;
        protected override Int32 GetValue() => MsfsData.Instance.RefreshRate;
        protected override Int32 SetValue(Int32 newValue) => MsfsData.Instance.RefreshRate = newValue;
    }
}
