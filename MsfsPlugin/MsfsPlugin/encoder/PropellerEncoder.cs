namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.encoder;
    class PropellerEncoder : DefaultEncoder
    {
        public PropellerEncoder() : base("Propeller", "Current propeller", "Nav", true, -100, 100, 1) { }
        protected override void RunCommand(String actionParameter) => this.SetValue(0);
        protected override Int32 GetValue()
        {
            return MsfsData.Instance.CurrentPropeller;
        }
        protected override Int32 SetValue(Int32 newValue) => MsfsData.Instance.CurrentPropeller = newValue;
    }
}
