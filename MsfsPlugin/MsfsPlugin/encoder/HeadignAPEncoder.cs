namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.encoder;
    using Loupedeck.MsfsPlugin.tools;

    class HeadignAPEncoder : DefaultEncoder
    {
        public HeadignAPEncoder() : base("Head", "Autopilot heading encoder", "AP", true, 1, 360, 1)
        {
            _bindings.Add(MsfsData.Instance.Register(BindingKeys.AP_HEADING));
            _bindings.Add(MsfsData.Instance.Register(BindingKeys.HEADING));
        }
        protected override void RunCommand(string actionParameter) => SetValue(_bindings[1].ControllerValue);
        protected override string GetDisplayValue() => "[" + _bindings[0].ControllerValue + "]\n" + _bindings[1].ControllerValue;
        protected override long GetValue() => _bindings[0].ControllerValue;
        protected override void SetValue(long newValue) => _bindings[0].SetControllerValue(newValue);
        protected override void ApplyAdjustment(string actionParameter, int ticks)
        {
            SetValue(ConvertTool.ApplyAdjustment(GetValue(), ticks, min, max, step, true));
            ActionImageChanged();
        }

    }
}
