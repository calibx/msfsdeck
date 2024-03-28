namespace Loupedeck.MsfsPlugin
{
    class SpeedAPEncoder : DefaultEncoder
    {
        public SpeedAPEncoder() : base("Speed", "Autopilot speed encoder", "AP", true, 0, 2000, 1)
        {
            apSpeed = Bind(BindingKeys.AP_SPEED);
            speed = Bind(BindingKeys.SPEED);
        }

        protected override void RunCommand(string actionParameter) => SetValue(speed.ControllerValue);

        protected override string GetDisplayValue() => "[" + apSpeed.ControllerValue + "]\n" + speed.ControllerValue;

        protected override long GetValue() => apSpeed.ControllerValue;

        protected override void SetValue(long newValue) => apSpeed.SetControllerValue(newValue);

        readonly Binding apSpeed;
        readonly Binding speed;
    }
}
