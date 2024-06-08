namespace Loupedeck.MsfsPlugin
{
    class FlapEncoder : DefaultEncoder
    {
        public FlapEncoder() : base("Flaps", "Current flap level", "Nav", true, 0, 100, 1)
        {
            maxFlap = Bind(BindingKeys.MAX_FLAP);
            flap = Bind(BindingKeys.FLAP);
        }

        protected override string GetDisplayValue() => flap.ControllerValue + " / " + maxFlap.ControllerValue;

        protected override long GetValue()
        {
            max = (int)maxFlap.ControllerValue;
            return flap.ControllerValue;
        }

        protected override void RunCommand(string actionParameter) => SetValue(0);

        protected override void SetValue(long newValue) => flap.SetControllerValue(newValue);

        readonly Binding maxFlap;
        readonly Binding flap;
    }
}
