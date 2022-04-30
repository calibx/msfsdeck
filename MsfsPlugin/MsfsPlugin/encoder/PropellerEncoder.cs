namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.encoder;
    class PropellerEncoder : DefaultEncoder
    {
        public PropellerEncoder() : base("Propeller", "Current propeller", "Nav", true, 0, 100, 1) => this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.PROPELLER)));
        protected override void RunCommand(String actionParameter) => this.SetValue(0);
        protected override Int64 GetValue() => this._bindings[0].ControllerValue;
        protected override void SetValue(Int64 newValue) => this._bindings[0].SetControllerValue(newValue);

    }
}
