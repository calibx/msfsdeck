namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.encoder;

    class FlapEncoder : DefaultEncoder
    {
        public FlapEncoder() : base("Flaps", "Current flap level", "Nav", true, 0, 100, 1)
        {
            _bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.MAX_FLAP)));
            _bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.FLAP)));
        }

        protected override string GetDisplayValue()
        {
            return _bindings[1].ControllerValue + " / " + _bindings[0].ControllerValue;
        }

        protected override long GetValue()
        {
            max = (int)_bindings[0].ControllerValue;
            return _bindings[1].ControllerValue;
        }

        protected override void RunCommand(string actionParameter) => SetValue(0);

        protected override void SetValue(long newValue) => _bindings[1].SetControllerValue(newValue);
    }
}
