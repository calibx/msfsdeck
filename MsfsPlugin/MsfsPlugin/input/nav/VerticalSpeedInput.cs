namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.input;

    class VerticalSpeedInput : DefaultInput
    {
        public VerticalSpeedInput() : base("VS", "Display current and AP vertical speed", "Nav")
        {
            this.bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_VSPEED_INPUT)));
            this.bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.VSPEED_INPUT)));
        }
        protected override String GetValue() => "VSpeed\n[" + this.bindings[0].ControllerValue + "]\n" + this.bindings[1].ControllerValue;
        protected override void ChangeValue() => this.bindings[0].SetControllerValue(this.bindings[1].ControllerValue);
    }
}

