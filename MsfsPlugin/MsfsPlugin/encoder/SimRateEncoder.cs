namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.encoder;

    class SimRateEncoder : DefaultEncoder
    {
        public SimRateEncoder() : base("Sim Rate", "Simulation rate encoder", "Misc", true, 0, 12800, 100) =>
            bindings.Add(Register(BindingKeys.SIM_RATE));

        protected override void RunCommand(string actionParameter) => SetValue(0);

        protected override long GetValue() => bindings[0].ControllerValue;

        protected override string GetDisplayValue() => (bindings[0].ControllerValue / 100f).ToString();

        protected override void SetValue(long newValue) => bindings[0].SetControllerValue(newValue);

        protected override void ApplyAdjustment(string actionParameter, int ticks)
        {
            if (ticks > 0)
            {
                var newValue = GetValue() * 2;
                SetValue(newValue > 12800 ? 12800 : newValue);
            }
            else
            {
                var newValue = GetValue() / 2;
                SetValue(newValue < 25 ? 25 : newValue);
            }
            ActionImageChanged();
        }
    }
}
