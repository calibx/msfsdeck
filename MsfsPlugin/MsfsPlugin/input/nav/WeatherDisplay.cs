namespace Loupedeck.MsfsPlugin.input.nav
{
    internal class WeatherDisplay : DefaultInput
    {
        public WeatherDisplay() : base("Weather", "Display weather parameters", "Nav")
        {
            bindings.Add(bindingOat = MsfsData.Instance.Register(BindingKeys.AIR_TEMP));
            bindings.Add(bindingWindDir = MsfsData.Instance.Register(BindingKeys.WIND_DIRECTION));
            bindings.Add(bindingWindVel = MsfsData.Instance.Register(BindingKeys.WIND_SPEED));
            bindings.Add(bindingVisib = MsfsData.Instance.Register(BindingKeys.VISIBILITY));
        }

        protected override string GetValue() => $"{bindingOat.MsfsValue / 10.0} °C\n{bindingWindDir.MsfsValue}°/{bindingWindVel.MsfsValue}kts\nVis:{bindingVisib.MsfsValue}m";

        readonly Binding bindingOat;
        readonly Binding bindingWindDir;
        readonly Binding bindingWindVel;
        readonly Binding bindingVisib;
    }
}
