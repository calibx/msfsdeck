namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.encoder;
    class PropellerEncoder : DefaultEncoder
    {
        public PropellerEncoder() : base("Prop", "Current propeller", "Nav", true, 0, 100, 1) =>
            bindings.Add(Register(BindingKeys.PROPELLER));

        protected override void RunCommand(string actionParameter) => SetValue(0);

        protected override long GetValue() => bindings[0].ControllerValue;

        protected override void SetValue(long newValue) => bindings[0].SetControllerValue(newValue);

    }
}
