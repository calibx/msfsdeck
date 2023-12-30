namespace Loupedeck.MsfsPlugin.input.nav
{
    internal class WeatherDisplay : DefaultInput
    {
        public WeatherDisplay() : base("Weather", "Display weather parameters", "Misc")
        {
            bindings.Add(oat = MsfsData.Instance.Register(BindingKeys.AIR_TEMP));
            bindings.Add(windDir = MsfsData.Instance.Register(BindingKeys.WIND_DIRECTION));
            bindings.Add(windVel = MsfsData.Instance.Register(BindingKeys.WIND_SPEED));
            bindings.Add(visib = MsfsData.Instance.Register(BindingKeys.VISIBILITY));
            bindings.Add(pressure = MsfsData.Instance.Register(BindingKeys.SEA_LEVEL_PRESSURE));
        }

        protected override string GetValue() => $"{OatText}\n{WindText}\n{VisibilityText}\n{PressureText}";

        string OatText => $"{oat.MsfsValue / 10.0} °C";
        string WindText => $"{windDir.MsfsValue}°/{windVel.MsfsValue} kts";
        string VisibilityText => $"Vis: {VisibilityValueText(visib.MsfsValue)} m";
        string VisibilityValueText(long value) => value > 9999 ? ">9999" : value.ToString();
        string PressureText => $"{pressure.MsfsValue / 10.0} hPa";

        readonly Binding oat;
        readonly Binding windDir;
        readonly Binding windVel;
        readonly Binding visib;
        readonly Binding pressure;
    }
}
