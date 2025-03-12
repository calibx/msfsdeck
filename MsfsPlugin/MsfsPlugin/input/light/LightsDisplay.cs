namespace Loupedeck.MsfsPlugin.input.light
{
    using Loupedeck;
    using Loupedeck.MsfsPlugin.msfs;
    using Loupedeck.MsfsPlugin.tools;

    internal class LightsDisplay : DefaultInput
    {
        public LightsDisplay() : base("External lights", "Display external lights on/off", "Lights")
        {
            nav = Bind(BindingKeys.LIGHT_NAV);
            beacon = Bind(BindingKeys.LIGHT_BEACON);
            taxi = Bind(BindingKeys.LIGHT_TAXI);
            strobe = Bind(BindingKeys.LIGHT_STROBE);
            landing = Bind(BindingKeys.LIGHT_LANDING);
            wing = Bind(BindingKeys.LIGHT_WING);
            logo = Bind(BindingKeys.LIGHT_LOGO);
            recognition = Bind(BindingKeys.LIGHT_RECOG);
            connection = Bind(BindingKeys.CONNECTION);
        }

        protected override BitmapImage GetCommandImage(string actionParameter, PluginImageSize imageSize)
        {
            using (var bitmapBuilder = new BitmapBuilder(imageSize))
            {
                void Draw(string text, int x, int y, bool emphasize) =>
                    bitmapBuilder.DrawText(text, x, y, text.Length * 7, fontSize, GetTextColor(emphasize), fontSize);

                Draw("NAV", 4, line1, nav.GetBool());
                Draw("BEACON", 32, line1, beacon.GetBool());
                Draw("TAXI", 0, line2, taxi.GetBool());
                Draw("STROBE", 34, line2, strobe.GetBool());
                Draw("LANDING", 2, line3, landing.GetBool());
                Draw("RECOGNITION", 2, line4, recognition.GetBool());
                Draw("LOGO", 4, line5, logo.GetBool());
                Draw("WING", 42, line5, wing.GetBool());

                return bitmapBuilder.ToImage();
            }
        }

        protected override void RunCommand(string actionParameter)
        {
            var curValue = connection.MsfsValue;
            if (curValue == 1 || curValue == 2)
                SimConnectDAO.Instance.Disconnect();
            else
                SimConnectDAO.Instance.Connect();
        }

        readonly Binding nav;
        readonly Binding beacon;
        readonly Binding taxi;
        readonly Binding strobe;
        readonly Binding landing;
        readonly Binding wing;
        readonly Binding logo;
        readonly Binding recognition;
        readonly Binding connection;

        BitmapColor GetTextColor(bool emphasize) => emphasize ? ImageTool.Green : ImageTool.LightGrey;

        const int fontSize = 13;
        const int line1 = 0;
        const int line2 = line1 + fontSize + 2;
        const int line3 = line2 + fontSize + 2;
        const int line4 = line3 + fontSize + 2;
        const int line5 = line4 + fontSize + 2;
    }
}
