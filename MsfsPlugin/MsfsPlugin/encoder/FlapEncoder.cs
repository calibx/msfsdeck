namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.encoder;

    class FlapEncoder : DefaultEncoder
    {
        public FlapEncoder() : base("Flap", "Current flap level", "Nav", true, 0, 100, 1)
        {
            var bind = new Binding(BindingKeys.MAX_FLAP);
            this._bindings.Add(bind);
            MsfsData.Instance.Register(bind);
            bind = new Binding(BindingKeys.FLAP);
            this._bindings.Add(bind);
            MsfsData.Instance.Register(bind);
        }
        protected override Int64 GetValue()
        {
            this.max = (Int32)this._bindings[0].ControllerValue;
            return this._bindings[1].ControllerValue;
        }
        protected override void RunCommand(String actionParameter) => this.SetValue(0);
        protected override void SetValue(Int64 newValue) => this._bindings[1].SetControllerValue(newValue);
    }
}
