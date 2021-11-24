namespace Loupedeck.MsfsPlugin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class HeadingEncoder : PluginDynamicAdjustment, Notifiable
    {

        public HeadingEncoder() : base("Head", "heading of the AP", "AP", true)
        {
            MsfsData.Instance.register(this);
        }
        protected override void ApplyAdjustment(String actionParameter, Int32 ticks)
        {
            var newHeading = (MsfsData.Instance.currentAPHeading + ticks) % 360;
            if (newHeading < 0)
            {
                newHeading += 360;
            }
            MsfsData.Instance.currentAPHeading = newHeading;
            MsfsData.Instance.changed();
        }
        protected override void RunCommand(String actionParameter)
        {
            MsfsData.Instance.currentAPHeading = MsfsData.Instance.currentHeading;
            MsfsData.Instance.changed();
        }

        protected override String GetAdjustmentValue(String actionParameter) => MsfsData.Instance.currentAPHeading.ToString();


        public void Notify() => this.AdjustmentValueChanged();
    }
}
