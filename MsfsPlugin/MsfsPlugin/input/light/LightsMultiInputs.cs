namespace Loupedeck.MsfsPlugin.folder
{
    using Loupedeck.MsfsPlugin.input;
    using Loupedeck.MsfsPlugin.msfs;
    using Loupedeck.MsfsPlugin.tools;

    public class LightsMultiInputs : DefaultInput
    {
        public LightsMultiInputs()
        {
            nav = Bind(BindingKeys.LIGHT_NAV);
            beacon = Bind(BindingKeys.LIGHT_BEACON);
            landing = Bind(BindingKeys.LIGHT_LANDING);
            taxi = Bind(BindingKeys.LIGHT_TAXI);
            strobes = Bind(BindingKeys.LIGHT_STROBE);
            instrument = Bind(BindingKeys.LIGHT_INSTRUMENT);
            recog = Bind(BindingKeys.LIGHT_RECOG);
            wing = Bind(BindingKeys.LIGHT_WING);
            logo = Bind(BindingKeys.LIGHT_LOGO);
            cabin = Bind(BindingKeys.LIGHT_CABIN);
            pedestal = Bind(BindingKeys.LIGHT_PEDESTAL);
            glareshield = Bind(BindingKeys.LIGHT_GLARESHIELD);
            all = Bind(BindingKeys.LIGHT_ALL_SWITCH);
            flashlight = Bind(BindingKeys.FLASHLIGHT);

            AddParameter("Navigation", "Navigation light", "Lights");
            AddParameter("Beacon", "Beacon light", "Lights");
            AddParameter("Landing", "Landing light", "Lights");
            AddParameter("Taxi", "Taxi light", "Lights");
            AddParameter("Strobes", "Strobes light", "Lights");
            AddParameter("Instruments", "Instruments light", "Lights");
            AddParameter("Recognition", "Recognition light", "Lights");
            AddParameter("Wing", "Wing light", "Lights");
            AddParameter("Logo", "Logo light", "Lights");
            AddParameter("Cabin", "Cabin light", "Lights");
            AddParameter("Pedestal", "Pedestal light", "Lights");
            AddParameter("Glareshield", "Glareshield light", "Lights");
            AddParameter("All lights", "All lights", "Lights");
            AddParameter("Flashlight", "Flashlight", "Lights");
        }

        protected override BitmapImage GetCommandImage(string actionParameter, PluginImageSize imageSize)
        {
            using (var bitmapBuilder = new BitmapBuilder(imageSize))
            {
                switch (actionParameter)
                {
                    case "Navigation":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(nav.ControllerValue));
                        break;
                    case "Beacon":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(beacon.ControllerValue));
                        break;
                    case "Landing":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(landing.ControllerValue));
                        break;
                    case "Taxi":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(taxi.ControllerValue));
                        break;
                    case "Strobes":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(strobes.ControllerValue));
                        break;
                    case "Instruments":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(instrument.ControllerValue));
                        break;
                    case "Recognition":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(recog.ControllerValue));
                        break;
                    case "Wing":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(wing.ControllerValue));
                        break;
                    case "Logo":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(logo.ControllerValue));
                        break;
                    case "Cabin":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(cabin.ControllerValue));
                        break;
                    case "Pedestal":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(pedestal.ControllerValue));
                        break;
                    case "Glareshield":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(glareshield.ControllerValue));
                        break;
                    case "All lights":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(all.ControllerValue));
                        break;
                    case "Flashlight":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(flashlight.ControllerValue));
                        break;
                }
                bitmapBuilder.DrawText(actionParameter);
                return bitmapBuilder.ToImage();
            }
        }

        protected override void RunCommand(string actionParameter)
        {
            switch (actionParameter)
            {
                case "Navigation":
                    nav.ToggleControllerValue();
                    break;
                case "Beacon":
                    beacon.ToggleControllerValue();
                    break;
                case "Landing":
                    landing.ToggleControllerValue();
                    break;
                case "Taxi":
                    taxi.ToggleControllerValue();
                    break;
                case "Strobes":
                    strobes.ToggleControllerValue();
                    break;
                case "Instruments":
                    instrument.ToggleControllerValue();
                    break;
                case "Recognition":
                    recog.ToggleControllerValue();
                    break;
                case "Wing":
                    wing.ToggleControllerValue();
                    break;
                case "Logo":
                    logo.ToggleControllerValue();
                    break;
                case "Cabin":
                    cabin.ToggleControllerValue();
                    break;
                case "Pedestal":
                    pedestal.ToggleControllerValue();
                    break;
                case "Glareshield":
                    glareshield.ToggleControllerValue();
                    break;
                case "All lights":
                    all.ToggleControllerValue();
                    break;
                case "Flashlight":
                    flashlight.ToggleControllerValue();
                    break;
            }
        }

        readonly Binding nav;
        readonly Binding beacon;
        readonly Binding landing;
        readonly Binding taxi;
        readonly Binding strobes;
        readonly Binding instrument;
        readonly Binding recog;
        readonly Binding wing;
        readonly Binding logo;
        readonly Binding cabin;
        readonly Binding pedestal;
        readonly Binding glareshield;
        readonly Binding all;
        readonly Binding flashlight;
    }
}
