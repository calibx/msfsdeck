namespace Loupedeck.MsfsPlugin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Loupedeck.MsfsPlugin.encoder;

    class MixtureEncoder : DefaultEncoder
    {
        public MixtureEncoder() : base("Mixture", "Mixture encoder for the 4 engines", "Misc", true, 0, 16383, 1)
        {
        }
        protected override Int32 GetValue() => MsfsData.Instance.CurrentMixture;
        protected override Int32 SetValue(Int32 newValue) => MsfsData.Instance.CurrentMixture = newValue;
    }
}
