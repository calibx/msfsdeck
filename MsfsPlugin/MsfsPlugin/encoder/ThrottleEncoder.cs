namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.encoder;
    class ThrottleEncoder : DefaultEncoder
    {
        public ThrottleEncoder() : base("Throttle", "Current throttle", "Misc", true, -100, 100, 1) { }
        protected override void RunCommand(String actionParameter) => this.SetValue(MsfsData.Instance.ThrottleLowerFromMSFS < 0 ? -100 : 0);
        protected override Int32 GetValue()
        {
            this.min = MsfsData.Instance.ThrottleLowerFromMSFS < 0 ? -100 : 0;
            return MsfsData.Instance.CurrentThrottle;
        }
        protected override Int32 SetValue(Int32 newValue) => MsfsData.Instance.CurrentThrottle = newValue;
    }
}
