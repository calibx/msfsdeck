namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.input;
    class FuelDisplay : DefaultInput
    {
        public FuelDisplay() : base("Fuel", "Display fuel left, flow and time before empty", "Misc") { }
        protected override String GetValue() => "Fuel\n" + MsfsData.Instance.fuelPercent + " %\n" + MsfsData.Instance.fuelFlow + " pph\n" + TimeSpan.FromSeconds(MsfsData.Instance.fuelTimeLeft).ToString();
    }
}

