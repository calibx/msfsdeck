namespace Loupedeck.MsfsPlugin
{
    using System;

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
            return (MsfsData.Instance.CurrentBrakes == 0) ? "Enable parking brakes" : "Disable parking brakes";
        }

        protected override void RunCommand(String actionParameter)
        {
            MsfsData.Instance.CurrentBrakes = MsfsData.Instance.CurrentBrakes != 0 ? 0 : 32767;
        }
    }
}

