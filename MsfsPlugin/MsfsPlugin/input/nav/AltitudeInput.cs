namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.input;

    class AltitudeInput : DefaultInput
    {
        public AltitudeInput() : base("Altitude", "Display current and AP altitude", "Nav")
        {
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_ALT_INPUT)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.ALT_INPUT)));
        }
        protected override String GetValue() => "Alt\n[" + this._bindings[0].ControllerValue + "]\n" + this._bindings[1].ControllerValue;
        protected override void ChangeValue() => this._bindings[0].SetControllerValue(this._bindings[1].ControllerValue);
    }
}