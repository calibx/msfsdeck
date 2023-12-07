namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.input;
    class FuelDisplay : DefaultInput
    {
        public FuelDisplay() : base("Fuel", "Display fuel left, flow and time before empty", "Misc")
        {
            this.bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.FUEL_PERCENT)));
            this.bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.FUEL_FLOW)));
            this.bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.FUEL_TIME_LEFT)));
        }
        protected override String GetValue() => "Fuel\n" + this.bindings[0].MsfsValue + " %\n" + (this.bindings[1].MsfsValue != 0 ? this.bindings[1].MsfsValue + " gph\n" + TimeSpan.FromSeconds(this.bindings[2].MsfsValue).ToString() : "0 gph\n 0 sec");
    }
}

