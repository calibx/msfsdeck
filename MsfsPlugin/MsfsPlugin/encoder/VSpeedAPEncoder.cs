namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.encoder;

    class VSpeedAPEncoder : DefaultEncoder
    {
        public VSpeedAPEncoder() : base("VS", "Autopilot VS encoder", "AP", true, -10000, 10000, 100) { }
        protected override void RunCommand(String actionParameter) => MsfsData.Instance.CurrentAPVerticalSpeed = MsfsData.Instance.CurrentAPVerticalSpeedState - MsfsData.Instance.CurrentVerticalSpeed;
        protected override String GetDisplayValue() => "[" + MsfsData.Instance.CurrentAPVerticalSpeedState + "]\n" + MsfsData.Instance.CurrentVerticalSpeed;
        protected override void SetValue(Int64 newValue) => MsfsData.Instance.CurrentAPVerticalSpeed = (Int32)newValue;
    }
}
