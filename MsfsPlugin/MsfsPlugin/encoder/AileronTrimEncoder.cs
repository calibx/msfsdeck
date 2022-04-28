namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.encoder;

    class AileronTrimEncoder : DefaultEncoder
    {
        public AileronTrimEncoder() : base("Aileron Trim", "Aileron trim encoder", "Nav", true, -100, 100, 1)
        {
            var bind = new Binding(BindingKeys.AILERON_TRIM);
            this._bindings.Add(bind);
            MsfsData.Instance.Register(bind);
        }
        protected override void RunCommand(String actionParameter) => this.SetValue(0);
        protected override Int64 GetValue() => this._bindings[0].ControllerValue;
        protected override String GetDisplayValue() => (this._bindings[0].ControllerValue / 10f).ToString();
        protected override void SetValue(Int64 newValue) => this._bindings[0].SetControllerValue(newValue);
    }
}
