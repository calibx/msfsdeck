namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.input;

    class AltitudeInput : DefaultInput
    {
        public AltitudeInput() : base("Altitude", "Display current and AP altitude", "Nav"){}
        protected override String GetValue() => "Alt\n" + MsfsData.Instance.CurrentAltitude + "\n[" + MsfsData.Instance.CurrentAPAltitude + "]";
        protected override void ChangeValue() => MsfsData.Instance.CurrentAPAltitude = MsfsData.Instance.CurrentAltitude;
    }
}