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
            MsfsData.Instance.currentAPAltitude = MsfsData.Instance.currentAPAltitude + ticks*100;
            MsfsData.Instance.dirtyAP = true;
            MsfsData.Instance.changed();
        }
        protected override void RunCommand(String actionParameter)
        {
            MsfsData.Instance.currentAPAltitude = MsfsData.Instance.currentAltitude;
            MsfsData.Instance.dirtyAP = true;
            MsfsData.Instance.changed();
        }

        protected override String GetAdjustmentValue(String actionParameter) => MsfsData.Instance.currentAPAltitude.ToString();


        public void Notify() => this.AdjustmentValueChanged();
    }
}
