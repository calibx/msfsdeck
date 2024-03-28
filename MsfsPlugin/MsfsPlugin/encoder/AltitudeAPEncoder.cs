namespace Loupedeck.MsfsPlugin
{
    class AltitudeAPEncoder : DefaultEncoder
    {
        public AltitudeAPEncoder() : base("Alt", "Autopilot altitude encoder", "AP", true, -10000, 99900, 100)
        {
            apAlt = Bind(BindingKeys.AP_ALT);
            alt = Bind(BindingKeys.ALT);
        }

        protected override void RunCommand(string actionParameter) => SetValue(alt.ControllerValue);

        protected override string GetDisplayValue() => "[" + apAlt.ControllerValue + "]\n" + alt.ControllerValue;

        protected override long GetValue() => apAlt.ControllerValue;

        protected override void SetValue(long newValue) => apAlt.SetControllerValue(newValue);

        readonly Binding apAlt;
        readonly Binding alt;
    }
}
