namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.encoder;

    class AileronTrimEncoder : DefaultEncoder
    {
        public AileronTrimEncoder() : base("Aileron Trim", "Aileron trim encoder", "Nav", true, -100, 100, 1) {
            this._binding = new Binding(BindingKeys.AILERON_TRIM);
            MsfsData.Instance.Register(this._binding);
        }
        protected override void RunCommand(String actionParameter) => this.SetValue(0);
        protected override Int64 GetValue() => this._binding.ControllerValue;
        protected override String GetDisplayValue() => (this._binding.ControllerValue /10f).ToString();
        protected override void SetValue(Int64 newValue) => this._binding.SetControllerValue(newValue);
    }
}
