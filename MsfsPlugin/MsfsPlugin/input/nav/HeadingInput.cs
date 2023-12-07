namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.input;

    class HeadingInput : DefaultInput
    {
        public HeadingInput() : base("Heading", "Display current and AP heading", "Nav")
        {
            this.bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_HEADING_INPUT)));
            this.bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.HEADING_INPUT)));
        }
        protected override String GetValue() => "Head\n[" + this.bindings[0].ControllerValue + "]\n" + this.bindings[1].ControllerValue;

        protected override void ChangeValue() => this.bindings[0].SetControllerValue(this.bindings[1].ControllerValue);
    }
}

