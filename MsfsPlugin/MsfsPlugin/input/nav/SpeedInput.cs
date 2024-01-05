namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.input;

    class SpeedInput : DefaultInput
    {
        public SpeedInput() : base("Speed", "Display current and AP speed", "Nav")
        {
            bindings.Add(MsfsData.Instance.Register(BindingKeys.AP_SPEED));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.SPEED));
        }

        protected override string GetValue() => "Speed\n[" + bindings[0].ControllerValue + "]\n" + bindings[1].ControllerValue;

        protected override void ChangeValue() => bindings[0].SetControllerValue(bindings[1].ControllerValue);
    }
}

