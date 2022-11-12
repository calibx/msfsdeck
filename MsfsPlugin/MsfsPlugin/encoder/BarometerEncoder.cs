namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.encoder;

    class BarometerEncoder : DefaultEncoder
    {
        public BarometerEncoder() : base("Baro", "Barometer encoder", "Nav", true, 2799, 3201, 1) => this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.KOHLSMAN)));
        protected override void RunCommand(String actionParameter) => this.SetValue(2992);
        protected override String GetDisplayValue() => (this._bindings[0].ControllerValue / 100f).ToString();
        protected override Int64 GetValue() => this._bindings[0].ControllerValue;
        protected override void SetValue(Int64 newValue) => this._bindings[0].SetControllerValue(newValue);


    }
}
