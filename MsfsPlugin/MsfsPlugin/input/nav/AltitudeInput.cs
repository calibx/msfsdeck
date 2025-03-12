namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.input;

    class AltitudeInput : DefaultInput
    {
        public AltitudeInput() : base("Altitude", "Display current and AP altitude", "Nav")
        {
            apAlt = Bind(BindingKeys.AP_ALT);
            alt = Bind(BindingKeys.ALT);
        }

        protected override string GetValue() => "Alt\n[" + apAlt.ControllerValue + "]\n" + alt.ControllerValue;

        protected override void ChangeValue() => apAlt.SetControllerValue(alt.ControllerValue);

        readonly Binding apAlt;
        readonly Binding alt;
    }
}