namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.encoder;

    class RudderTrimEncoder : DefaultEncoder
    {
        public RudderTrimEncoder() : base("Rudder Trim", "Rudder trim encoder", "Nav", true, -100, 100, 1) { }
        protected override void RunCommand(String actionParameter) => this.SetValue(0);
        protected override Int64 GetValue() => MsfsData.Instance.CurrentRudderTrim;
        protected override void SetValue(Int64 newValue) => MsfsData.Instance.CurrentRudderTrim = (Int32)newValue;
    }
}
