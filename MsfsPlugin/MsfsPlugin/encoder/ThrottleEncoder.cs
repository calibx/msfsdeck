namespace Loupedeck.MsfsPlugin
{
    class ThrottleEncoder : DefaultEncoder
    {
        public ThrottleEncoder() : base("Throttle", "Current throttle", "Nav", true, -100, 100, 1)
        {
            throttle = Bind(BindingKeys.THROTTLE);
        }

        protected override void RunCommand(string actionParameter) => SetValue(0);

        protected override long GetValue() => throttle.ControllerValue;

        protected override void SetValue(long newValue) => throttle.SetControllerValue(newValue);

        readonly Binding throttle;
    }
}
