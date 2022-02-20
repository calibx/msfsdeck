namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.encoder;

    class BarometerEncoder : DefaultEncoder
    {
        public BarometerEncoder() : base("Baro", "Barometer encoder", "Nav", true, 948, 1084, 1) { }
        protected override void RunCommand(String actionParameter) => this.SetValue(0);
        protected override String GetDisplayValue() => Math.Round(MsfsData.Instance.Barometer * 0.02952998751, 2).ToString();
        protected override Int32 GetValue() => MsfsData.Instance.Barometer;
        protected override void SetValue(Int32 newValue) => MsfsData.Instance.Barometer = (Int16)newValue;
    }
}
