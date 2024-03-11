namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.encoder;
    using Loupedeck.MsfsPlugin.tools;

    class BarometerEncoder : DefaultEncoder
    {
        public BarometerEncoder() : base("Baro", "Barometer encoder", "Nav", true, 2799, 3201, 1)
        {
            kohlsman = Bind(BindingKeys.KOHLSMAN);                  // Value is 10000 times the actual value (rounded to long)
            pressure = Bind(BindingKeys.SEA_LEVEL_PRESSURE);
        }

        protected override void RunCommand(string actionParameter) => SetValue(ConvertTool.RoundToLong(pressure.MsfsValue / ConvertTool.inHg2mbar * 1000d));

        protected override string GetDisplayValue() => ConvertTool.Round(GetValue() / 10000d, 2).ToString() + "\n" + GetValueHpa().ToString();

        protected override long GetValue() => kohlsman.ControllerValue;

        protected override void SetValue(long newValue) => kohlsman.SetControllerValue(newValue);

        protected override void ApplyAdjustment(string actionParameter, int ticks)
        {
            SetValue(ConvertTool.ApplyAdjustment(ConvertTool.RoundToLong(GetValue() / 100d), ticks, min, max, step) * 100);
            ActionImageChanged();
        }

        private double GetValueHpa()
        {
            var result = kohlsman.ControllerValue * ConvertTool.inHg2mbar / 10000d;
            return ConvertTool.Round(result, 1);
        }

        readonly Binding kohlsman;
        readonly Binding pressure;
    }
}
