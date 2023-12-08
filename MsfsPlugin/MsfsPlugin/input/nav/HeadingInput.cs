namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.input;

    class HeadingInput : DefaultInput
    {
        public HeadingInput() : base("Heading", "Display current and AP heading", "Nav")
        {
            bindings.Add(MsfsData.Instance.Register(BindingKeys.AP_HEADING_INPUT));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.HEADING_INPUT));
        }

        protected override string GetValue() => "Head\n[" + bindings[0].ControllerValue + "]\n" + bindings[1].ControllerValue;

        protected override void ChangeValue() => bindings[0].SetControllerValue(bindings[1].ControllerValue);
    }
}

