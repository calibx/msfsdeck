namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.input;

    class VerticalSpeedInput : DefaultInput
    {
        public VerticalSpeedInput() : base("VS", "Display current and AP vertical speed", "Nav") {
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_VSPEED_INPUT)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.VSPEED_INPUT)));
        }
        protected override String GetValue() => "VSpeed\n[" + this._bindings[0].ControllerValue + "]\n" + this._bindings[1].ControllerValue;
        protected override void ChangeValue() => this._bindings[0].SetControllerValue(this._bindings[1].ControllerValue);
    }
}

