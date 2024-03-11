namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.input;

    class FuelDisplay : DefaultInput
    {
        public FuelDisplay() : base("Fuel", "Display fuel left, flow and time before empty", "Misc")
        {
            fuelPercent = Bind(BindingKeys.FUEL_PERCENT);
            fuelFlowGph = Bind(BindingKeys.FUEL_FLOW_GPH);
            fuelFlowPph = Bind(BindingKeys.FUEL_FLOW_PPH);
            fuelTimeLeft = Bind(BindingKeys.FUEL_TIME_LEFT);
        }

        protected override string GetValue() =>
            $"Fuel {fuelPercent.MsfsValue} %\n{fuelFlowGph.MsfsValue} gph\n{AlternativeValueText}\n {TimeLeftText}";

        protected override void ChangeValue()
        {
            showPph = !showPph;
        }

        string AlternativeValueText => showPph ? $"{PphValue} pph" : $"{KgphValue} kgph";

        string TimeLeftText => fuelFlowGph.MsfsValue == 0 ? "" : TimeSpan.FromSeconds(fuelTimeLeft.MsfsValue).ToString();

        long KgphValue => (long)Math.Round(PphValue * 0.45359237);
        long PphValue => (long)Math.Round(fuelFlowPph.MsfsValue / 100.0);

        bool showPph = false;   // If false Kgph is shown

        readonly Binding fuelPercent;
        readonly Binding fuelFlowGph;
        readonly Binding fuelFlowPph;
        readonly Binding fuelTimeLeft;
    }
}
