namespace Loupedeck.MsfsPlugin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Loupedeck.MsfsPlugin.encoder;

    class RefreshRateEncoder : DefaultEncoder
    {
        public RefreshRateEncoder() : base("Refresh", "Current data refresh rate", "Misc", true, 1, 3000, 1)
        {
        }
        protected override Int32 GetValue() => MsfsData.Instance.CurrentZoom;
        protected override Int32 SetValue(Int32 newValue) => MsfsData.Instance.CurrentZoom = newValue;
    }
}
