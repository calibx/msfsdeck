namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.encoder;

    class HeadignAPEncoder : DefaultEncoder
    {
        public HeadignAPEncoder() : base("Head", "Autopilot heading encoder", "AP", true, 0, 360, 1)
        {
            var bind = new Binding(BindingKeys.AP_HEADING);
            this._bindings.Add(bind);
            MsfsData.Instance.Register(bind);
            bind = new Binding(BindingKeys.HEADING);
            this._bindings.Add(bind);
            MsfsData.Instance.Register(bind);
        }
        protected override void RunCommand(String actionParameter) => this.SetValue(this._bindings[1].ControllerValue);
        protected override String GetDisplayValue() => "[" + this._bindings[0].ControllerValue + "]\n" + this._bindings[1].ControllerValue;
        protected override Int64 GetValue() => this._bindings[0].ControllerValue;
        protected override void SetValue(Int64 newValue) => this._bindings[0].SetControllerValue(newValue);
    }
}
