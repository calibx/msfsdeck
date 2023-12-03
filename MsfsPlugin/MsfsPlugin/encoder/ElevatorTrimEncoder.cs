namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.encoder;

    class ElevatorTrimEncoder : DefaultEncoder
    {
        public ElevatorTrimEncoder() : base("Elevator Trim", "Elevator trim encoder", "Nav", true, -100, 100, 1) =>
            bindings.Add(MsfsData.Instance.Register(BindingKeys.ELEVATOR_TRIM));

        protected override void RunCommand(string actionParameter) => SetValue(0);

        protected override long GetValue() => bindings[0].ControllerValue;

        protected override void SetValue(long newValue) => bindings[0].SetControllerValue(newValue);
    }
}