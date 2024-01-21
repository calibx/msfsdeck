namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.input;
    class FuelDisplay : DefaultInput
    {
        public FuelDisplay() : base("Fuel", "Display fuel left, flow and time before empty", "Misc")
        {
            bindings.Add(fuelPercent = Register(BindingKeys.FUEL_PERCENT));
            bindings.Add(fuelFlowGph = Register(BindingKeys.FUEL_FLOW_GPH));
            bindings.Add(fuelFlowPph = Register(BindingKeys.FUEL_FLOW_PPH));
            bindings.Add(fuelTimeLeft = Register(BindingKeys.FUEL_TIME_LEFT));
        }

        protected override string GetValue() =>
            "Fuel " + fuelPercent.MsfsValue + "%\n" +
            fuelFlowGph.MsfsValue + " gph\n" +
            fuelFlowPph.MsfsValue + " pph\n" +
            (fuelFlowGph.MsfsValue == 0 ? "" : TimeSpan.FromSeconds(fuelTimeLeft.MsfsValue).ToString());

        readonly Binding fuelPercent;
        readonly Binding fuelFlowGph;
        readonly Binding fuelFlowPph;
        readonly Binding fuelTimeLeft;
    }
}

