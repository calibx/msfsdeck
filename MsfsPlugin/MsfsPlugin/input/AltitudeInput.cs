namespace Loupedeck.MsfsPlugin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class AltitudeInput : PluginDynamicCommand, Notifiable
    {
        public AltitudeInput() : base("Altitude", "Display current and AP altitude", "Nav")

        {
            MsfsData.Instance.register(this);
        }

        public void Notify() => this.AdjustmentValueChanged();

        protected override String GetCommandDisplayName(String actionParameter, PluginImageSize imageSize)
        {
            return MsfsData.Instance.CurrentAltitude + "\n" + MsfsData.Instance.CurrentAPAltitude;
        }

        protected override void RunCommand(String actionParameter)
        {
            MsfsData.Instance.CurrentAPAltitude = MsfsData.Instance.CurrentAltitude;
            MsfsData.Instance.changed();
        }
    }
}

