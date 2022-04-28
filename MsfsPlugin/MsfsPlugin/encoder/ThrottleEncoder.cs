namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.encoder;
    class ThrottleEncoder : DefaultEncoder
    {
        public ThrottleEncoder() : base("Throttle", "Current throttle", "Nav", true, 0, 100, 1) {
            var bind = new Binding(BindingKeys.MIN_THROTTLE);
            this._bindings.Add(bind);
            MsfsData.Instance.Register(bind);
            bind = new Binding(BindingKeys.THROTTLE);
            this._bindings.Add(bind);
            MsfsData.Instance.Register(bind);
        }
        protected override void RunCommand(String actionParameter) => this.SetValue(0);
        protected override Int64 GetValue() => this._bindings[1].ControllerValue;
        protected override void SetValue(Int64 newValue) => this._bindings[1].SetControllerValue(newValue);
    }
}
