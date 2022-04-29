namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.input;

    class AltitudeInput : DefaultInput
    {
        public AltitudeInput() : base("Altitude", "Display current and AP altitude", "Nav") {
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_ALT)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.ALT)));
        }
        protected override String GetValue() => "[" + this._bindings[0].ControllerValue + "]\n" + this._bindings[1].ControllerValue;
        protected override void ChangeValue() => this._bindings[1].SetControllerValue(1);
    }
}