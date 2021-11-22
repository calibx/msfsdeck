namespace Loupedeck.MsfsPlugin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class HeadingEncoder : PluginDynamicAdjustment
    {

        public HeadingEncoder() : base("Head", "heading of the AP", "AP", true)
        {
        }
        protected override void ApplyAdjustment(String actionParameter, Int32 ticks)
        {
            MsfsData.Instance.Heading += ticks % 360;
            this.AdjustmentValueChanged();
        }
        protected override void RunCommand(String actionParameter)
        {
            // set le heading actuel en cible
            this.AdjustmentValueChanged();
            
        }

        protected override String GetAdjustmentValue(String actionParameter) => MsfsData.Instance.Heading.ToString();
    }
}
