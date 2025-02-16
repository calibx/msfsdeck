namespace Loupedeck.MsfsPlugin.input.nav
{
    using Loupedeck.MsfsPlugin.tools;

    internal class WeatherDisplay : DefaultInput
    {
        public WeatherDisplay() : base("Weather", "Display weather parameters", "Misc")
        {
            oat = Bind(BindingKeys.AIR_TEMP);
            windDir = Bind(BindingKeys.WIND_DIRECTION);
            windVel = Bind(BindingKeys.WIND_SPEED);
            visib = Bind(BindingKeys.VISIBILITY);
            pressure = Bind(BindingKeys.SEA_LEVEL_PRESSURE);
        }

        protected override string GetValue() => $"{OatText}\n{WindText}\n{VisibilityText}\n{PressureText}";

        protected override void ChangeValue()
        {
            showInHg = !showInHg;
        }

        string OatText => $"{oat.MsfsValue / 10.0} °C";
        string WindText => $"{windDir.MsfsValue}°/{windVel.MsfsValue} kts";
        string VisibilityText => $"Vis: {VisibilityValueText(visib.MsfsValue)} m";
        string VisibilityValueText(long value) => value > 9999 ? ">9999" : value.ToString();
        string PressureText => showInHg ? $"{ConvertTool.Round(InHgValue, 2)} inHg" : $"{HPaValue} hPa";

        double InHgValue => HPaValue / ConvertTool.inHg2mbar;
        double HPaValue => pressure.MsfsValue / 10.0;

        bool showInHg = false;   // If false hPa is shown

        readonly Binding oat;
        readonly Binding windDir;
        readonly Binding windVel;
        readonly Binding visib;
        readonly Binding pressure;
    }
}
