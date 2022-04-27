namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.encoder;

    class ElevatorTrimEncoder : DefaultEncoder
    {
        public ElevatorTrimEncoder() : base("Elevator Trim", "Elevator trim encoder", "Nav", true, -100, 100, 1) { }
        protected override void RunCommand(String actionParameter) => this.SetValue(0);
        protected override Int64 GetValue() => MsfsData.Instance.CurrentElevatorTrim;
        protected override void SetValue(Int64 newValue) => MsfsData.Instance.CurrentElevatorTrim = (Int32)newValue;
    }
}