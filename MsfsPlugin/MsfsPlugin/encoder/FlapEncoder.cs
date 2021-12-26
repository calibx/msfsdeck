namespace Loupedeck.MsfsPlugin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Loupedeck.MsfsPlugin.encoder;

    class FlapEncoder : DefaultEncoder
    {
        public FlapEncoder() : base("Flap", "Current flap level", "Misc", true, 0, 100, 1)
        {
            this.max = MsfsData.Instance.MaxFlap;
        }
        protected override Int32 GetValue() => MsfsData.Instance.CurrentFlap;
        protected override Int32 SetValue(Int32 newValue) => MsfsData.Instance.CurrentFlap = newValue;
    }
}
