namespace Loupedeck.MsfsPlugin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Loupedeck.MsfsPlugin.encoder;

    class ZoomEncoder : DefaultEncoder
    {
        public ZoomEncoder() : base("Zoom", "Current Zoom level", "Misc", true, -16383, 16383, 1)
        {
        }

        protected override void RunCommand(String actionParameter) => MsfsData.Instance.CurrentZoom = 0;
        protected override Int32 GetValue() => MsfsData.Instance.CurrentZoom;
        protected override Int32 SetValue(Int32 newValue) => MsfsData.Instance.CurrentZoom = newValue;
    }
}
