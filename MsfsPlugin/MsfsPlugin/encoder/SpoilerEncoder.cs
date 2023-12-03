namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.encoder;

    class SpoilerEncoder : DefaultEncoder
    {
        public SpoilerEncoder() : base("Spoiler", "Spoiler position", "Nav", true, 0, 100, 1)
        {
            bindings.Add(MsfsData.Instance.Register(BindingKeys.SPOILER));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.SPOILERS_ARM));
        }

        protected override void RunCommand(string actionParameter) => bindings[1].SetControllerValue((1+1)%2);

        protected override long GetValue() => bindings[0].ControllerValue;

        protected override void SetValue(long newValue) => bindings[0].SetControllerValue(newValue);

        protected override string GetDisplayValue() => bindings[1].ControllerValue == 1 ? "[" + GetValue().ToString()  + "]" : GetValue().ToString();
    }
}
