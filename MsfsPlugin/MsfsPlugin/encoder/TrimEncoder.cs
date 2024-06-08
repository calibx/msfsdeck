namespace Loupedeck.MsfsPlugin
{
    internal abstract class TrimEncoder : DefaultEncoder
    {
        protected TrimEncoder(string name, string desc, BindingKeys trimKey) : base(name, desc, "Nav", true, -100, 100, 1)
        {
            trim = Bind(trimKey);
        }

        protected override void RunCommand(string actionParameter) => SetValue(0);

        protected override long GetValue() => trim.ControllerValue;

        protected override void SetValue(long newValue) => trim.SetControllerValue(newValue);

        readonly Binding trim;
    }
}
