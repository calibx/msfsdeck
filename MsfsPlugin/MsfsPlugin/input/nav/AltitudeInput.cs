namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.input;

    class AltitudeInput : DefaultInput
    {
        public AltitudeInput() : base("Altitude", "Display current and AP altitude", "Nav")
        {
            this.bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_ALT_INPUT)));
            this.bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.ALT_INPUT)));
        }
        protected override String GetValue() => "Alt\n[" + this.bindings[0].ControllerValue + "]\n" + this.bindings[1].ControllerValue;
        protected override void ChangeValue() => this.bindings[0].SetControllerValue(this.bindings[1].ControllerValue);
    }
}