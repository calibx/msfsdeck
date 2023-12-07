namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.encoder;

    class BarometerEncoder : DefaultEncoder
    {
        public BarometerEncoder() : base("Baro", "Barometer encoder", "Nav", true, 2799, 3201, 1) =>
            bindings.Add(MsfsData.Instance.Register(BindingKeys.KOHLSMAN));

        protected override void RunCommand(string actionParameter) => SetValue(2992);

        protected override string GetDisplayValue() => (GetValue() / 100f).ToString() + "\n" + GetValueHpa().ToString();

        protected override long GetValue() => bindings[0].ControllerValue;

        protected override void SetValue(long newValue) => bindings[0].SetControllerValue(newValue);

        private double GetValueHpa()
        {
            var result = bindings[0].ControllerValue * 0.3386389;
            return Math.Round(result, 1);
        }
    }
}
