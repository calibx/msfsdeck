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
            MsfsData.Instance.CurrentAPVerticalSpeed = MsfsData.Instance.CurrentAPVerticalSpeed + ticks * 10;
            MsfsData.Instance.dirtyAP = true;
            MsfsData.Instance.changed();
        }
        protected override void RunCommand(String actionParameter)
        {
            MsfsData.Instance.CurrentAPVerticalSpeed = MsfsData.Instance.CurrentVerticalSpeed;
            MsfsData.Instance.dirtyAP = true;
            MsfsData.Instance.changed();
        }

        protected override String GetAdjustmentValue(String actionParameter) => MsfsData.Instance.CurrentAPVerticalSpeed.ToString();


        public void Notify() => this.AdjustmentValueChanged();
    }
}
