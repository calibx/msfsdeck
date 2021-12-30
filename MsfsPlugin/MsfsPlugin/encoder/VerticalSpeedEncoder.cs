namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.encoder;
    class VerticalSpeedEncoder : DefaultEncoder
    { 
        public VerticalSpeedEncoder() : base("VS", "Vertical speed of the AP", "AP", true, 0, 10000, 100) {}
        protected override String GetDisplayValue() => "[" + this.GetValue().ToString() + "]\n" + MsfsData.Instance.CurrentVerticalSpeed;
        protected override void RunCommand(String actionParameter) => this.SetValue((Int32)(Math.Round(MsfsData.Instance.CurrentVerticalSpeed / 100d, 0) * 100));
        protected override Int32 GetValue() => MsfsData.Instance.CurrentAPVerticalSpeed;
        protected override Int32 SetValue(Int32 newValue) => MsfsData.Instance.CurrentAPVerticalSpeed = newValue;
    }
}
