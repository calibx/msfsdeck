namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.encoder;

    class FlapEncoder : DefaultEncoder
    {
        public FlapEncoder() : base("Flap", "Current flap level", "Nav", true, 0, 100, 1) => this.max = MsfsData.Instance.MaxFlap;
        protected override void RunCommand(String actionParameter) => this.SetValue(0);
        protected override Int32 GetValue()
        {
            this.max = MsfsData.Instance.MaxFlap;
            return MsfsData.Instance.CurrentFlap;
        }
        protected override Int32 SetValue(Int32 newValue) => MsfsData.Instance.CurrentFlap = newValue;
    }
}
