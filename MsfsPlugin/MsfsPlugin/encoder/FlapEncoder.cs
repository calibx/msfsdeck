namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.encoder;

    class FlapEncoder : DefaultEncoder
    {
        public FlapEncoder() : base("Flap", "Current flap level", "Nav", true, 0, 100, 1) => this.max = MsfsData.Instance.MaxFlap - 1;
        protected override void RunCommand(String actionParameter) => this.SetValue(0);
        protected override Int64 GetValue()
        {
            this.max = MsfsData.Instance.MaxFlap - 1;
            return MsfsData.Instance.CurrentFlap;
        }
        protected override void SetValue(Int64 newValue) => MsfsData.Instance.CurrentFlap = (Int32)newValue;
    }
}
