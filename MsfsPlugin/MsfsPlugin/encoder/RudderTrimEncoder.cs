namespace Loupedeck.MsfsPlugin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class RudderTrimEncoder : PluginDynamicAdjustment, Notifiable
    {
        public RudderTrimEncoder() : base("Rudder Trim", "Rudder trim encoder", "Misc", true)
        {
            MsfsData.Instance.register(this);
        }
        protected override void ApplyAdjustment(String actionParameter, Int32 ticks)
        {
            MsfsData.Instance.CurrentRudderTrim += (Int16)ticks;
            if (MsfsData.Instance.CurrentRudderTrim < -16383)
            { MsfsData.Instance.CurrentRudderTrim = -16383; }
            else if (MsfsData.Instance.CurrentRudderTrim > 16383)
            { MsfsData.Instance.CurrentRudderTrim = 16383; }
        }
        protected override void RunCommand(String actionParameter)
        {
            MsfsData.Instance.CurrentRudderTrim = 0;
        }

        protected override String GetAdjustmentValue(String actionParameter)
        {

            return MsfsData.Instance.CurrentRudderTrim.ToString();

        }
        public void Notify() => this.AdjustmentValueChanged();
    }
}
