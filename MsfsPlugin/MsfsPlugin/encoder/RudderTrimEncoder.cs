namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.encoder;

    class RudderTrimEncoder : DefaultEncoder
    {
        public RudderTrimEncoder() : base("Rudder Trim", "Rudder trim encoder", "Nav", true, -100, 100, 1) =>
            bindings.Add(Register(BindingKeys.RUDDER_TRIM));

        protected override void RunCommand(string actionParameter) => SetValue(0);

        protected override long GetValue() => bindings[0].ControllerValue;

        protected override void SetValue(long newValue) => bindings[0].SetControllerValue(newValue);
    }
}
