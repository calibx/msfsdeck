namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.input;

    class VerticalSpeedInput : DefaultInput
    {
        public VerticalSpeedInput() : base("VS", "Display current and AP vertical speed", "Nav")
        {
            bindings.Add(Register(BindingKeys.AP_VSPEED));
            bindings.Add(Register(BindingKeys.VSPEED));
        }

        protected override string GetValue() => "VSpeed\n[" + bindings[0].ControllerValue + "]\n" + bindings[1].ControllerValue;

        protected override void ChangeValue() => bindings[0].SetControllerValue(bindings[1].ControllerValue);
    }
}
