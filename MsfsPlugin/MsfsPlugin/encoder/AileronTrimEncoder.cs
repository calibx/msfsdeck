namespace Loupedeck.MsfsPlugin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class AileronTrimEncoder : PluginDynamicAdjustment, Notifiable
    {
        public AileronTrimEncoder() : base("Aileron Trim", "Aileron trim encoder", "Misc", true)
        {
            MsfsData.Instance.register(this);
        }
        protected override void ApplyAdjustment(String actionParameter, Int32 ticks)
        {
            MsfsData.Instance.CurrentAileronTrim += (Int16)ticks;
            if (MsfsData.Instance.CurrentAileronTrim < -16383)
            { MsfsData.Instance.CurrentAileronTrim = -16383; }
            else if (MsfsData.Instance.CurrentAileronTrim > 16383)
            { MsfsData.Instance.CurrentAileronTrim = 16383; }
        }
        protected override void RunCommand(String actionParameter)
        {
            MsfsData.Instance.CurrentAileronTrim = 0;
        }

        protected override String GetAdjustmentValue(String actionParameter)
        {

            return MsfsData.Instance.CurrentAileronTrim.ToString();

        }
        public void Notify() => this.AdjustmentValueChanged();
    }
}
