namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.input;
    class FuelDisplay : DefaultInput
    {
        public FuelDisplay() : base("Fuel", "Display fuel left, flow and time before empty", "Misc")
        {
            var bind = new Binding(BindingKeys.FUEL_PERCENT);
            this._bindings.Add(bind);
            MsfsData.Instance.Register(bind);
            bind = new Binding(BindingKeys.FUEL_FLOW);
            this._bindings.Add(bind);
            MsfsData.Instance.Register(bind);
            bind = new Binding(BindingKeys.FUEL_TIME_LEFT);
            this._bindings.Add(bind);
            MsfsData.Instance.Register(bind);
        }
        protected override String GetValue() => "Fuel\n" + this._bindings[0].MsfsValue + " %\n" + (this._bindings[1].MsfsValue != 0 ? this._bindings[1].MsfsValue + " gph\n" + TimeSpan.FromSeconds(this._bindings[2].MsfsValue).ToString() : "0 gph\n 0 sec");
    }
}

