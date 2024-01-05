namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.encoder;
    using Loupedeck.MsfsPlugin.tools;

    class HeadignAPEncoder : DefaultEncoder
    {
        public HeadignAPEncoder() : base("Head", "Autopilot heading encoder", "AP", true, 1, 360, 1)
        {
            bindings.Add(Register(BindingKeys.AP_HEADING));
            bindings.Add(Register(BindingKeys.HEADING));
        }
        protected override void RunCommand(string actionParameter) => SetValue(bindings[1].ControllerValue);
        protected override string GetDisplayValue() => "[" + bindings[0].ControllerValue + "]\n" + bindings[1].ControllerValue;
        protected override long GetValue() => bindings[0].ControllerValue;
        protected override void SetValue(long newValue) => bindings[0].SetControllerValue(newValue);
        protected override void ApplyAdjustment(string actionParameter, int ticks)
        {
            SetValue(ConvertTool.ApplyAdjustment(GetValue(), ticks, min, max, step, true));
            ActionImageChanged();
        }

    }
}
