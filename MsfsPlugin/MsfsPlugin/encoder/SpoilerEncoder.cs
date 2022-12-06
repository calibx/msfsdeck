namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.encoder;
    class SpoilerEncoder : DefaultEncoder
    {
        public SpoilerEncoder() : base("Spoiler", "Spoiler position", "Nav", true, 0, 100, 1)
        {
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.SPOILER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.SPOILERS_ARM)));
        }
        protected override void RunCommand(String actionParameter) => this._bindings[1].SetControllerValue((1+1)%2);
        protected override Int64 GetValue() => this._bindings[0].ControllerValue;
        protected override void SetValue(Int64 newValue) => this._bindings[0].SetControllerValue(newValue);
        protected override String GetDisplayValue() => this._bindings[1].ControllerValue == 1 ? "[" + this.GetValue().ToString()  + "]" : this.GetValue().ToString();
    }
}
