namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.encoder;
    using Loupedeck.MsfsPlugin.tools;

    class HeadingAPEncoder : DefaultEncoder
    {
        public HeadingAPEncoder() : base("Head", "Autopilot heading encoder", "AP", true, 1, 360, 1)
        {
            ApHeading = Bind(BindingKeys.AP_HEADING);
            Heading = Bind(BindingKeys.HEADING);
        }

        protected override void RunCommand(string actionParameter) => SetValue(Heading.ControllerValue);
        protected override string GetDisplayValue() => "[" + ApHeading.ControllerValue + "]\n" + Heading.ControllerValue;
        protected override long GetValue() => ApHeading.ControllerValue;
        protected override void SetValue(long newValue) => ApHeading.SetControllerValue(newValue);
        protected override void ApplyAdjustment(string actionParameter, int ticks)
        {
            SetValue(ConvertTool.ApplyAdjustment(ApHeading.ControllerValue, ticks, min, max, step, true));
            ActionImageChanged();
        }

        readonly Binding ApHeading;
        readonly Binding Heading;
    }
}
