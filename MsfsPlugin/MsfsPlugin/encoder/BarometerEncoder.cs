namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.encoder;

    class BarometerEncoder : DefaultEncoder
    {
        public BarometerEncoder() : base("Baro", "Barometer encoder", "Nav", true, 2799, 3201, 1)
        {
            bindings.Add(MsfsData.Instance.Register(BindingKeys.KOHLSMAN));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.SEA_LEVEL_PRESSURE));
        }

        protected override void RunCommand(string actionParameter) => SetValue((long)Math.Round(bindings[1].MsfsValue / inHg2mbar / 10));

        protected override string GetDisplayValue() => (GetValue() / 100f).ToString() + "\n" + GetValueHpa().ToString();

        protected override long GetValue() => bindings[0].ControllerValue;

        protected override void SetValue(long newValue) => bindings[0].SetControllerValue(newValue);

        private double GetValueHpa()
        {
            var result = bindings[0].ControllerValue * inHg2mbar;
            return Math.Round(result, 1);
        }

        private const double inHg2mbar = 0.3386389;
    }
}
