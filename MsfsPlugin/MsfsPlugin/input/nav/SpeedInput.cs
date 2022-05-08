namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.input;

    class SpeedInput : DefaultInput
    {
        public SpeedInput() : base("Speed", "Display current and AP speed", "Nav")
        {
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_SPEED_INPUT)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.SPEED_INPUT)));
        }
        protected override String GetValue() => "Speed\n[" + this._bindings[0].ControllerValue + "]\n" + this._bindings[1].ControllerValue;
        protected override void ChangeValue() => this._bindings[0].SetControllerValue(this._bindings[1].ControllerValue);
    }
}

