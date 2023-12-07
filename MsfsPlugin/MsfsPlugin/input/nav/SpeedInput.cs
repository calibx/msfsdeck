namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.input;

    class SpeedInput : DefaultInput
    {
        public SpeedInput() : base("Speed", "Display current and AP speed", "Nav")
        {
            this.bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_SPEED_INPUT)));
            this.bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.SPEED_INPUT)));
        }
        protected override String GetValue() => "Speed\n[" + this.bindings[0].ControllerValue + "]\n" + this.bindings[1].ControllerValue;
        protected override void ChangeValue() => this.bindings[0].SetControllerValue(this.bindings[1].ControllerValue);
    }
}

