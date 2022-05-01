namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.encoder;
    class SpoilerEncoder : DefaultEncoder
    {
        public SpoilerEncoder() : base("Spoiler", "Spoiler position", "Nav", true, -1, 100, 1) => this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.SPOILER)));
        protected override void RunCommand(String actionParameter) => this.SetValue(0);
        protected override Int64 GetValue() => this._bindings[0].ControllerValue;
        protected override void SetValue(Int64 newValue) => this._bindings[0].SetControllerValue(newValue);
        protected override String GetDisplayValue() => this.GetValue() == -1 ? "Arm" : this.GetValue().ToString();
    }
}
