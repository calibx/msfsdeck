namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.encoder;

    class AltitudeAPEncoder : DefaultEncoder
    {
        public AltitudeAPEncoder() : base("Alt", "Autopilot altitude encoder", "AP", true, -10000, 99900, 100)
        {
            bindings.Add(Register(BindingKeys.AP_ALT));
            bindings.Add(Register(BindingKeys.ALT));
        }

        protected override void RunCommand(string actionParameter) => SetValue(bindings[1].ControllerValue);

        protected override string GetDisplayValue() => "[" + bindings[0].ControllerValue + "]\n" + bindings[1].ControllerValue;

        protected override long GetValue() => bindings[0].ControllerValue;

        protected override void SetValue(long newValue) => bindings[0].SetControllerValue(newValue);
    }
}
