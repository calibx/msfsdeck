namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.encoder;

    class SpeedAPEncoder : DefaultEncoder
    {
        public SpeedAPEncoder() : base("Speed", "Autopilot speed encoder", "AP", true, 0, 2000, 1)
        {
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_SPEED)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.SPEED)));
        }
        protected override void RunCommand(String actionParameter) => this.SetValue(this._bindings[1].ControllerValue);
        protected override String GetDisplayValue() => "[" + this._bindings[0].ControllerValue + "]\n" + this._bindings[1].ControllerValue;
        protected override Int64 GetValue() => this._bindings[0].ControllerValue;
        protected override void SetValue(Int64 newValue) => this._bindings[0].SetControllerValue(newValue);
    }
}
