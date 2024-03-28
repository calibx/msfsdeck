namespace Loupedeck.MsfsPlugin
{
    class MixtureEncoder : DefaultEncoder
    {
        public MixtureEncoder() : base("Mixture", "Mixture encoder for the 4 engines", "Nav", true, 0, 100, 1)
        {
            mixture = Bind(BindingKeys.MIXTURE);
        }

        protected override void RunCommand(string actionParameter) => SetValue(0);

        protected override long GetValue() => mixture.ControllerValue;

        protected override void SetValue(long newValue) => mixture.SetControllerValue(newValue);

        readonly Binding mixture;
    }
}
