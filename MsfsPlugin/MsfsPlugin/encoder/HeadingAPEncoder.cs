namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.tools;

    class HeadingAPEncoder : DefaultEncoder
    {
        public HeadingAPEncoder() : base("Head", "Autopilot heading encoder", "AP", true, 1, 360, 1)
        {
            apHeading = Bind(BindingKeys.AP_HEADING);
            heading = Bind(BindingKeys.HEADING);
        }

        protected override void RunCommand(string actionParameter) => SetValue(heading.ControllerValue);
        protected override string GetDisplayValue() => "[" + apHeading.ControllerValue + "]\n" + heading.ControllerValue;
        protected override long GetValue() => apHeading.ControllerValue;
        protected override void SetValue(long newValue) => apHeading.SetControllerValue(newValue);
        protected override void ApplyAdjustment(string actionParameter, int ticks)
        {
            SetValue(ConvertTool.ApplyAdjustment(apHeading.ControllerValue, ticks, min, max, step, true));
            ActionImageChanged();
        }

        readonly Binding apHeading;
        readonly Binding heading;
    }
}
