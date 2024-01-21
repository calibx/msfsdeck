namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.encoder;

    class ThrottleEncoder : DefaultEncoder
    {
        public ThrottleEncoder() : base("Throttle", "Current throttle", "Nav", true, -100, 100, 1)
        {
            bindings.Add(Register(BindingKeys.MIN_THROTTLE));
            bindings.Add(Register(BindingKeys.THROTTLE));
        }

        protected override void RunCommand(string actionParameter) => SetValue(0);

        protected override long GetValue() => bindings[1].ControllerValue;

        protected override void SetValue(long newValue) => bindings[1].SetControllerValue(newValue);
    }
}
