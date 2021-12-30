namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.input;
    class ParkingBrakeInput : DefaultInput
    {
        public ParkingBrakeInput() : base("Parking brake", "Display parking brakes state", "Misc") {}
        protected override String GetValue() => MsfsData.Instance.CurrentBrakes == 0 ? "Enable parking brakes" : "Disable parking brakes";
        protected override void ChangeValue() => MsfsData.Instance.CurrentBrakes = MsfsData.Instance.CurrentBrakes != 0 ? 0 : 32767;
    }
}

