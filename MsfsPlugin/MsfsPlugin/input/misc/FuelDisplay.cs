namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.input;
    class FuelDisplay : DefaultInput
    {
        public FuelDisplay() : base("Fuel", "Display fuel left, flow and time before empty", "Misc")
        {
            bindings.Add(fuelPercent = Register(BindingKeys.FUEL_PERCENT));
            bindings.Add(fuelFlow = Register(BindingKeys.FUEL_FLOW));
            bindings.Add(fuelTimeLeft = Register(BindingKeys.FUEL_TIME_LEFT));
        }

        protected override string GetValue() =>
            "Fuel\n" + fuelPercent.MsfsValue + " %\n" +
            fuelFlow.MsfsValue + " gph\n" +
            (fuelFlow.MsfsValue == 0 ? "" : TimeSpan.FromSeconds(fuelTimeLeft.MsfsValue).ToString());

        readonly Binding fuelPercent;
        readonly Binding fuelFlow;
        readonly Binding fuelTimeLeft;
    }
}

