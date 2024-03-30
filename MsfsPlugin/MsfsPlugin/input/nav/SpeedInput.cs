namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.input;

    class SpeedInput : DefaultInput
    {
        public SpeedInput() : base("Speed", "Display current and AP speed", "Nav")
        {
            apSpeed = Bind(BindingKeys.AP_SPEED);
            speed = Bind(BindingKeys.SPEED);
        }

        protected override string GetValue() => "Speed\n[" + apSpeed.ControllerValue + "]\n" + speed.ControllerValue;

        protected override void ChangeValue() => apSpeed.SetControllerValue(speed.ControllerValue);

        readonly Binding apSpeed;
        readonly Binding speed;
    }
}
