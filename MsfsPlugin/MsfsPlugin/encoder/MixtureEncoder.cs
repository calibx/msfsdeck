namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.encoder;

    class MixtureEncoder : DefaultEncoder
    {
        public MixtureEncoder() : base("Mixture", "Mixture encoder for the 4 engines", "Misc", true, 0, 100, 1)
        {
        }
        protected override void RunCommand(String actionParameter) => MsfsData.Instance.CurrentMixture = 0;
        protected override Int32 GetValue() => MsfsData.Instance.CurrentMixture;
        protected override Int32 SetValue(Int32 newValue) => MsfsData.Instance.CurrentMixture = newValue;
    }
}
