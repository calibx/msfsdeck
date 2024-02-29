namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.encoder;

    class AileronTrimEncoder : DefaultEncoder
    {
        public AileronTrimEncoder() : base("Aileron Trim", "Aileron trim encoder", "Nav", true, -100, 100, 1) =>
            bindings.Add(Register(BindingKeys.AILERON_TRIM));

        protected override void RunCommand(string actionParameter) => SetValue(0);

        protected override long GetValue() => bindings[0].ControllerValue;

        protected override string GetDisplayValue() => bindings[0].ControllerValue.ToString();

        protected override void SetValue(long newValue) => bindings[0].SetControllerValue(newValue);
    }
}
