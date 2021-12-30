namespace Loupedeck.MsfsPlugin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class SpeedEncoder : PluginDynamicAdjustment, Notifiable
    {

        public SpeedEncoder() : base("Speed", "Speed of the AP", "AP", true)
        {
            MsfsData.Instance.register(this);
        }
        protected override void ApplyAdjustment(String actionParameter, Int32 ticks)
        {
            MsfsData.Instance.CurrentAPSpeed = MsfsData.Instance.CurrentAPSpeed + ticks;
        }
        protected override void RunCommand(String actionParameter)
        {
            MsfsData.Instance.CurrentAPSpeed = MsfsData.Instance.CurrentSpeed;
        }

        protected override String GetAdjustmentValue(String actionParameter)
        {
            MsfsData.Instance.ValuesDisplayed = true;
            return "[" + MsfsData.Instance.CurrentAPSpeed.ToString() + "]\n" + MsfsData.Instance.CurrentSpeed;
        }


        public void Notify() => this.AdjustmentValueChanged();
    }
}
