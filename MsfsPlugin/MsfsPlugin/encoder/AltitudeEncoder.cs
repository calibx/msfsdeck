namespace Loupedeck.MsfsPlugin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class AltitudeEncoder : PluginDynamicAdjustment, Notifiable
    {

        public AltitudeEncoder() : base("Alt", "Altitude of the AP", "AP", true)
        {
            MsfsData.Instance.register(this);
        }
        protected override void ApplyAdjustment(String actionParameter, Int32 ticks)
        {
            MsfsData.Instance.CurrentAPAltitude = MsfsData.Instance.CurrentAPAltitude + ticks * 100;
        }
        protected override void RunCommand(String actionParameter)
        {
            MsfsData.Instance.CurrentAPAltitude = (Int32)(Math.Round(MsfsData.Instance.CurrentAltitude / 100d, 0) * 100);
        }

        protected override String GetAdjustmentValue(String actionParameter)
        {
            MsfsData.Instance.ValuesDisplayed = true;
            return "[" + MsfsData.Instance.CurrentAPAltitude.ToString() + "]\n" + MsfsData.Instance.CurrentAltitude;
        }


        public void Notify() => this.AdjustmentValueChanged();
    }
}
