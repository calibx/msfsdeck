namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.encoder;

    class RudderTrimEncoder : DefaultEncoder
    {
        public RudderTrimEncoder() : base("Rudder Trim", "Rudder trim encoder", "Misc", true, -100, 100, 1) { }
        protected override void RunCommand(String actionParameter) => this.SetValue(0);
        protected override Int32 GetValue() => MsfsData.Instance.CurrentRudderTrim;
        protected override Int32 SetValue(Int32 newValue) => MsfsData.Instance.CurrentRudderTrim = newValue;
    }
}
