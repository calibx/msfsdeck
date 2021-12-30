namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.encoder;
    class SpeedEncoder : DefaultEncoder
    {
        public SpeedEncoder() : base("Speed", "Speed of the AP", "AP", true, 0, 2000, 1) { }
        protected override String GetDisplayValue() => "[" + this.GetValue().ToString() + "]\n" + MsfsData.Instance.CurrentSpeed;
        protected override void RunCommand(String actionParameter) => this.SetValue(MsfsData.Instance.CurrentSpeed);
        protected override Int32 GetValue() => MsfsData.Instance.CurrentAPSpeed;
        protected override Int32 SetValue(Int32 newValue) => MsfsData.Instance.CurrentAPSpeed = newValue;
    }
}
