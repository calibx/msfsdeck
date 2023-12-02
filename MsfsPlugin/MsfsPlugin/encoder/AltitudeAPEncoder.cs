namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.encoder;

    class AltitudeAPEncoder : DefaultEncoder
    {
        public AltitudeAPEncoder() : base("Alt", "Autopilot altitude encoder", "AP", true, -10000, 99900, 100)
        {
            _bindings.Add(MsfsData.Instance.Register(BindingKeys.AP_ALT));
            _bindings.Add(MsfsData.Instance.Register(BindingKeys.ALT));
        }

        protected override void RunCommand(string actionParameter) => SetValue(_bindings[1].ControllerValue);

        protected override string GetDisplayValue() => "[" + _bindings[0].ControllerValue + "]\n" + _bindings[1].ControllerValue;

        protected override long GetValue() => _bindings[0].ControllerValue;

        protected override void SetValue(long newValue) => _bindings[0].SetControllerValue(newValue);
    }
}
