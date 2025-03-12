namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.input;

    class HeadingInput : DefaultInput
    {
        public HeadingInput() : base("Heading", "Display current and AP heading", "Nav")
        {
            apHeading = Bind(BindingKeys.AP_HEADING);
            heading = Bind(BindingKeys.HEADING);
        }

        protected override string GetValue() => "Head\n[" + apHeading.ControllerValue + "]\n" + heading.ControllerValue;

        protected override void ChangeValue() => apHeading.SetControllerValue(heading.ControllerValue);

        readonly Binding apHeading;
        readonly Binding heading;
    }
}
