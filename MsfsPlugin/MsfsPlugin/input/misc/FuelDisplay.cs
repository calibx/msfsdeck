namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.input;
    class FuelDisplay : DefaultInput
    {
        public FuelDisplay() : base("Fuel", "Display fuel left, flow and time before empty", "Misc")
        {
            bindings.Add(MsfsData.Instance.Register(BindingKeys.FUEL_PERCENT));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.FUEL_FLOW));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.FUEL_TIME_LEFT));
        }
        protected override string GetValue() => "Fuel\n" + bindings[0].MsfsValue + " %\n" + (bindings[1].MsfsValue != 0 ? bindings[1].MsfsValue + " gph\n" + TimeSpan.FromSeconds(bindings[2].MsfsValue).ToString() : "0 gph\n 0 sec");
    }
}

