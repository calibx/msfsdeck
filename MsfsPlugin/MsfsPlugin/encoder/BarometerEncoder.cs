namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.encoder;

    class BarometerEncoder : DefaultEncoder
    {
        public BarometerEncoder() : base("Baro", "Barometer encoder", "Nav", true, 2799, 3201, 1) =>
            _bindings.Add(MsfsData.Instance.Register(BindingKeys.KOHLSMAN));

        protected override void RunCommand(string actionParameter) => SetValue(2992);

        protected override string GetDisplayValue() => (_bindings[0].ControllerValue / 100f).ToString();

        protected override long GetValue() => _bindings[0].ControllerValue;

        protected override void SetValue(long newValue) => _bindings[0].SetControllerValue(newValue);
    }
}
