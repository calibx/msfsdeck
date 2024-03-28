namespace Loupedeck.MsfsPlugin
{
    class SimRateEncoder : DefaultEncoder
    {
        public SimRateEncoder() : base("Sim Rate", "Simulation rate encoder", "Misc", true, 0, 12800, 100)
        {
            simRate = Bind(BindingKeys.SIM_RATE);
        }

        protected override void RunCommand(string actionParameter) => SetValue(0);

        protected override long GetValue() => simRate.ControllerValue;

        protected override string GetDisplayValue() => (simRate.ControllerValue / 100f).ToString();

        protected override void SetValue(long newValue) => simRate.SetControllerValue(newValue);

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

        readonly Binding simRate;
    }
}
