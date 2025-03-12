namespace Loupedeck.MsfsPlugin
{
    class VSpeedAPEncoder : DefaultEncoder
    {
        public VSpeedAPEncoder() : base("VS", "Autopilot VS encoder", "AP", true, -10000, 10000, 100)
        {
            apVspeed = Bind(BindingKeys.AP_VSPEED);
            vSpeed = Bind(BindingKeys.VSPEED);
        }

        protected override void RunCommand(string actionParameter) => SetValue(vSpeed.ControllerValue);

        protected override string GetDisplayValue() => "[" + apVspeed.ControllerValue + "]\n" + vSpeed.ControllerValue;

        protected override long GetValue() => apVspeed.ControllerValue;

        protected override void SetValue(long newValue) => apVspeed.SetControllerValue(newValue);

        readonly Binding apVspeed;
        readonly Binding vSpeed;
    }
}
