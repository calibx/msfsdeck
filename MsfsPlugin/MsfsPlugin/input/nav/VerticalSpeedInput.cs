namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.input;

    class VerticalSpeedInput : DefaultInput
    {
        public VerticalSpeedInput() : base("VS", "Display current and AP vertical speed", "Nav")
        {
            apVspeed = Bind(BindingKeys.AP_VSPEED);
            vspeed = Bind(BindingKeys.VSPEED);
        }

        protected override string GetValue() => "VSpeed\n[" + apVspeed.ControllerValue + "]\n" + vspeed.ControllerValue;

        protected override void ChangeValue() => apVspeed.SetControllerValue(vspeed.ControllerValue);

        readonly Binding apVspeed;
        readonly Binding vspeed;
    }
}
