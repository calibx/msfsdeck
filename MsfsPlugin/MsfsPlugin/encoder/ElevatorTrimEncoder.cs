namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.encoder;

    class ElevatorTrimEncoder : DefaultEncoder
    {
        public ElevatorTrimEncoder() : base("Elevator Trim", "Elevator trim encoder", "Nav", true, -100, 100, 1) => this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.ELEVATOR_TRIM)));
        protected override void RunCommand(String actionParameter) => this.SetValue(0);
        protected override Int64 GetValue() => this._bindings[0].ControllerValue;
        protected override void SetValue(Int64 newValue) => this._bindings[0].SetControllerValue(newValue);
    }
}