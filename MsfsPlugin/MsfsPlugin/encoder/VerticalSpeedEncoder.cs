namespace Loupedeck.MsfsPlugin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class VerticalSpeedEncoder : PluginDynamicAdjustment, Notifiable
    {

        public VerticalSpeedEncoder() : base("VS", "Vertical speed of the AP", "AP", true)
        {
            MsfsData.Instance.register(this);
        }
        protected override void ApplyAdjustment(String actionParameter, Int32 ticks)
        {
            MsfsData.Instance.CurrentAPVerticalSpeed = (Int16)(MsfsData.Instance.CurrentAPVerticalSpeed + ticks * 100);
        }
        protected override void RunCommand(String actionParameter)
        {
            MsfsData.Instance.CurrentAPVerticalSpeed = (Int16)(Math.Round(MsfsData.Instance.CurrentVerticalSpeed / 100d, 0) * 100);
        }

        protected override String GetAdjustmentValue(String actionParameter)
        {
            MsfsData.Instance.ValuesDisplayed = true;
            return "[" + MsfsData.Instance.CurrentAPVerticalSpeed.ToString() + "]\n" + MsfsData.Instance.CurrentVerticalSpeed;
        }


        public void Notify() => this.AdjustmentValueChanged();
    }
}
