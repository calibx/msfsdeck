namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.input;

    class AltitudeInput : DefaultInput
    {
        public AltitudeInput() : base("Altitude", "Display current and AP altitude", "Nav")
        {
            bindings.Add(Register(BindingKeys.AP_ALT));
            bindings.Add(Register(BindingKeys.ALT));
        }

        protected override string GetValue() => "Alt\n[" + bindings[0].ControllerValue + "]\n" + bindings[1].ControllerValue;

        protected override void ChangeValue() => bindings[0].SetControllerValue(bindings[1].ControllerValue);
    }
}