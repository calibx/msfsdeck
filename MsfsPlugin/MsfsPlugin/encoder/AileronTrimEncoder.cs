namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.encoder;

    class AileronTrimEncoder : DefaultEncoder
    {
        public AileronTrimEncoder() : base("Aileron Trim", "Aileron trim encoder", "Nav", true, -100, 100, 1) { }
        protected override void RunCommand(String actionParameter) => this.SetValue(0);
        protected override Int32 GetValue() => MsfsData.Instance.CurrentAileronTrim;
        protected override void SetValue(Int32 newValue) => MsfsData.Instance.CurrentAileronTrim = newValue;
    }
}
