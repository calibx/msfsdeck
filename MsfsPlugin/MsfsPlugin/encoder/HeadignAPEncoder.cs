namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.encoder;
    using Loupedeck.MsfsPlugin.tools;

    class HeadignAPEncoder : DefaultEncoder
    {
        public HeadignAPEncoder() : base("Head", "Autopilot heading encoder", "AP", true, 0, 360, 1)
        {
            _bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_HEADING)));
            _bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.HEADING)));
        }
        protected override void RunCommand(String actionParameter) => SetValue(_bindings[1].ControllerValue);
        protected override String GetDisplayValue() => "[" + _bindings[0].ControllerValue + "]\n" + _bindings[1].ControllerValue;
        protected override Int64 GetValue() => _bindings[0].ControllerValue;
        protected override void SetValue(Int64 newValue) => _bindings[0].SetControllerValue(newValue);
        protected override void ApplyAdjustment(String actionParameter, Int32 ticks)
        {
            SetValue(ConvertTool.ApplyAdjustment(GetValue(), ticks, min, max, step, true));
            ActionImageChanged();
        }

    }
}
