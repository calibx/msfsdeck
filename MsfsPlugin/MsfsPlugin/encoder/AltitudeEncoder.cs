namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.encoder;

    class AltitudeEncoder : DefaultEncoder
    {
        public AltitudeEncoder() : base("Alt", "Altitude of the AP", "AP", true, 0, 20000, 100) { }
        protected override String GetDisplayValue() => "[" + this.GetValue().ToString() + "]\n" + MsfsData.Instance.CurrentAltitude;
        protected override void RunCommand(String actionParameter) => this.SetValue((Int32)(Math.Round(MsfsData.Instance.CurrentAltitude / 100d, 0) * 100));
        protected override Int32 GetValue() => MsfsData.Instance.CurrentAPAltitude;
        protected override Int32 SetValue(Int32 newValue) => MsfsData.Instance.CurrentAPAltitude = newValue;
    }
}
