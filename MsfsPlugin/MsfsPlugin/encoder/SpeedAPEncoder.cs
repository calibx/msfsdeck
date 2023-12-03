namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.encoder;

    class SpeedAPEncoder : DefaultEncoder
    {
        public SpeedAPEncoder() : base("Speed", "Autopilot speed encoder", "AP", true, 0, 2000, 1)
        {
            bindings.Add(MsfsData.Instance.Register(BindingKeys.AP_SPEED));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.SPEED));
        }

        protected override void RunCommand(string actionParameter) => SetValue(bindings[1].ControllerValue);

        protected override string GetDisplayValue() => "[" + bindings[0].ControllerValue + "]\n" + bindings[1].ControllerValue;

        protected override long GetValue() => bindings[0].ControllerValue;

        protected override void SetValue(long newValue) => bindings[0].SetControllerValue(newValue);
    }
}
