namespace Loupedeck.MsfsPlugin
{
    class PropellerEncoder : DefaultEncoder
    {
        public PropellerEncoder() : base("Prop", "Current propeller", "Nav", true, 0, 100, 1)
        {
            propeller = Bind(BindingKeys.PROPELLER);
        }

        protected override void RunCommand(string actionParameter) => SetValue(0);

        protected override long GetValue() => propeller.ControllerValue;

        protected override void SetValue(long newValue) => propeller.SetControllerValue(newValue);

        readonly Binding propeller;
    }
}
