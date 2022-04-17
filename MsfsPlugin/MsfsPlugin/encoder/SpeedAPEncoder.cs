namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.encoder;

    class SpeedAPEncoder : DefaultEncoder
    {
        public SpeedAPEncoder() : base("Speed", "Autopilot speed encoder", "AP", true, 0, 2000, 1) { }
        protected override void RunCommand(String actionParameter) => MsfsData.Instance.CurrentAPSpeed = MsfsData.Instance.CurrentAPSpeedState - MsfsData.Instance.CurrentSpeed;
        protected override String GetDisplayValue() => "[" + MsfsData.Instance.CurrentAPSpeedState + "]\n" + MsfsData.Instance.CurrentSpeed;
        protected override void SetValue(Int32 newValue) => MsfsData.Instance.CurrentAPSpeed = newValue;
    }
}
