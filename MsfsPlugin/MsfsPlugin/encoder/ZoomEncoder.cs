namespace Loupedeck.MsfsPlugin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class ZoomEncoder : PluginDynamicAdjustment, Notifiable
    {
        public ZoomEncoder() : base("Zoom", "Current Zoom level", "Misc", true)
        {
            MsfsData.Instance.register(this);
        }
        protected override void ApplyAdjustment(String actionParameter, Int32 ticks)
        {
            MsfsData.Instance.CurrentZoom += (Int16)ticks;
            if (MsfsData.Instance.CurrentZoom < -16383)
            { MsfsData.Instance.CurrentZoom = -16383; }
            else if (MsfsData.Instance.CurrentZoom > 16383)
            { MsfsData.Instance.CurrentZoom = 16383; }
        }
        protected override void RunCommand(String actionParameter)
        {
            MsfsData.Instance.CurrentZoom = 0;
        }

        protected override String GetAdjustmentValue(String actionParameter)
        {

            return MsfsData.Instance.CurrentZoom.ToString();

        }
        public void Notify() => this.AdjustmentValueChanged();
    }
}
