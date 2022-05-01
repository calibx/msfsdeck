namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.encoder;

    class FlapEncoder : DefaultEncoder
    {
        public FlapEncoder() : base("Flap", "Current flap level", "Nav", true, 0, 100, 1)
        {
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.MAX_FLAP)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.FLAP)));
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
