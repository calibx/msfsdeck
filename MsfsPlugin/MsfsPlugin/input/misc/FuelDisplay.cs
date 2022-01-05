namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.input;
    class FuelDisplay : DefaultInput
    {
        public FuelDisplay() : base("Fuel", "Display fuel left, flow and time before empty", "Misc") { }
        protected override String GetValue() => "Fuel\n" + MsfsData.Instance.FuelPercent + " %\n" + MsfsData.Instance.FuelFlow + " pph\n" + TimeSpan.FromSeconds(MsfsData.Instance.FuelTimeLeft).ToString();
    }
}

