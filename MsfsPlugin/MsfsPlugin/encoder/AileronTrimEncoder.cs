namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.encoder;

    class AileronTrimEncoder : DefaultEncoder
    {
        public AileronTrimEncoder() : base("Aileron Trim", "Aileron trim encoder", "Nav", true, -100, 100, 1) =>
            _bindings.Add(MsfsData.Instance.Register(BindingKeys.AILERON_TRIM));

        protected override void RunCommand(string actionParameter) => SetValue(0);

        protected override long GetValue() => _bindings[0].ControllerValue;

        protected override string GetDisplayValue() => _bindings[0].ControllerValue.ToString();

        protected override void SetValue(long newValue) => _bindings[0].SetControllerValue(newValue);
    }
}
