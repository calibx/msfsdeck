namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.encoder;

    class FlapEncoder : DefaultEncoder
    {
        public FlapEncoder() : base("Flaps", "Current flap level", "Nav", true, 0, 100, 1)
        {
            bindings.Add(Register(BindingKeys.MAX_FLAP));
            bindings.Add(Register(BindingKeys.FLAP));
        }

        protected override string GetDisplayValue() => bindings[1].ControllerValue + " / " + bindings[0].ControllerValue;

        protected override long GetValue()
        {
            max = (int)bindings[0].ControllerValue;
            return bindings[1].ControllerValue;
        }

        protected override void RunCommand(string actionParameter) => SetValue(0);

        protected override void SetValue(long newValue) => bindings[1].SetControllerValue(newValue);
    }
}
