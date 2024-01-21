namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.encoder;

    class MixtureEncoder : DefaultEncoder
    {
        public MixtureEncoder() : base("Mixture", "Mixture encoder for the 4 engines", "Nav", true, 0, 100, 1) =>
            bindings.Add(Register(BindingKeys.MIXTURE));

        protected override void RunCommand(string actionParameter) => SetValue(0);

        protected override long GetValue() => bindings[0].ControllerValue;

        protected override void SetValue(long newValue) => bindings[0].SetControllerValue(newValue);
    }
}
