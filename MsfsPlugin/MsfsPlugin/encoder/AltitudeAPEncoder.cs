namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.encoder;

    class AltitudeAPEncoder : DefaultEncoder
    {
        public AltitudeAPEncoder() : base("Alt", "Autopilot altitude encoder", "AP", true, -10000, 99900, 100) { }
        protected override void RunCommand(String actionParameter) => MsfsData.Instance.CurrentAPAltitude = MsfsData.Instance.CurrentAltitude;
        protected override String GetDisplayValue() => "[" + MsfsData.Instance.CurrentAPAltitude + "]\n" + MsfsData.Instance.CurrentAltitude;
        protected override void SetValue(Int32 newValue) => MsfsData.Instance.CurrentAPAltitude = newValue;
        protected override Int32 GetValue() => MsfsData.Instance.CurrentAPAltitude;
    }
}
