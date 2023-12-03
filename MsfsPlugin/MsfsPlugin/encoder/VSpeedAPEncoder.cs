namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.encoder;

    class VSpeedAPEncoder : DefaultEncoder
    {
        public VSpeedAPEncoder() : base("VS", "Autopilot VS encoder", "AP", true, -10000, 10000, 100)
        {
            bindings.Add(MsfsData.Instance.Register(BindingKeys.AP_VSPEED));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.VSPEED));
        }

        protected override void RunCommand(string actionParameter) => SetValue(bindings[1].ControllerValue);

        protected override string GetDisplayValue() => "[" + bindings[0].ControllerValue + "]\n" + bindings[1].ControllerValue;

        protected override long GetValue() => bindings[0].ControllerValue;

        protected override void SetValue(long newValue) => bindings[0].SetControllerValue(newValue);
    }
}
