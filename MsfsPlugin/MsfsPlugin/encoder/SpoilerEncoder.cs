namespace Loupedeck.MsfsPlugin
{
    class SpoilerEncoder : DefaultEncoder
    {
        public SpoilerEncoder() : base("Spoiler", "Spoiler position", "Nav", true, 0, 100, 1)
        {
            spoiler = Bind(BindingKeys.SPOILER);
            spoilersArm = Bind(BindingKeys.SPOILERS_ARM);
        }

        protected override void RunCommand(string actionParameter) => spoilersArm.SetControllerValue((1 + 1) % 2);

        protected override long GetValue() => spoiler.ControllerValue;

        protected override void SetValue(long newValue) => spoiler.SetControllerValue(newValue);

        protected override string GetDisplayValue() => spoilersArm.ControllerValue == 1 ? "[" + GetValue().ToString() + "]" : GetValue().ToString();

        readonly Binding spoiler;
        readonly Binding spoilersArm;
    }
}
