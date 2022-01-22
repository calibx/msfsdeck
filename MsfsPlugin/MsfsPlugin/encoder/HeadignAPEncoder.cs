namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.encoder;

    class HeadignAPEncoder : DefaultEncoder
    {
        public HeadignAPEncoder() : base("Head", "Autopilot heading encoder", "AP", true, 0, 360, 1) { }
        protected override void RunCommand(String actionParameter) => MsfsData.Instance.CurrentAPHeading = MsfsData.Instance.CurrentHeading;
        protected override String GetDisplayValue() => "[" + MsfsData.Instance.CurrentAPHeading + "]\n" + MsfsData.Instance.CurrentHeading;
        protected override void SetValue(Int32 newValue) => MsfsData.Instance.CurrentAPHeading = newValue;
        protected override Int32 GetValue() => MsfsData.Instance.CurrentAPHeading;
    }
}
