namespace Loupedeck.MsfsPlugin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class ParkingBrakeInput : PluginDynamicCommand, Notifiable
    {
        public ParkingBrakeInput() : base("Parking brake", "Display parking brakes state", "Misc")

        {
            MsfsData.Instance.register(this);
        }

        public void Notify() => this.AdjustmentValueChanged();

        protected override String GetCommandDisplayName(String actionParameter, PluginImageSize imageSize)
        {
            MsfsData.Instance.ValuesDisplayed = true;
            return (MsfsData.Instance.CurrentBrakes != 0) ? "Brakes On" : "Brakes Off";
        }

        protected override void RunCommand(String actionParameter)
        {
            if (MsfsData.Instance.CurrentBrakes != 0)
            {
                MsfsData.Instance.CurrentBrakes = 0;
            } else
            {
                MsfsData.Instance.CurrentBrakes = 32767;
            }
        }
    }
}

