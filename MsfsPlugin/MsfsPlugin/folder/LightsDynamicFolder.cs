namespace Loupedeck.MsfsPlugin.folder
{
    using System.Collections.Generic;

    using Loupedeck.MsfsPlugin.msfs;
    using Loupedeck.MsfsPlugin.tools;

    public class LightsDynamicFolder : DefaultFolder
    {
        public LightsDynamicFolder() : base("Lights")
        {
            nav = Bind(BindingKeys.LIGHT_NAV);
            beacon = Bind(BindingKeys.LIGHT_BEACON);
            landing = Bind(BindingKeys.LIGHT_LANDING);
            taxi = Bind(BindingKeys.LIGHT_TAXI);
            strobe = Bind(BindingKeys.LIGHT_STROBE);
            instrument = Bind(BindingKeys.LIGHT_INSTRUMENT);
            recognition = Bind(BindingKeys.LIGHT_RECOG);
            wing = Bind(BindingKeys.LIGHT_WING);
            logo = Bind(BindingKeys.LIGHT_LOGO);
            cabin = Bind(BindingKeys.LIGHT_CABIN);
            pedestal = Bind(BindingKeys.LIGHT_PEDESTAL);
            glareshield = Bind(BindingKeys.LIGHT_GLARESHIELD);
            flash = Bind(BindingKeys.FLASHLIGHT);
            allLights = Bind(BindingKeys.LIGHT_ALL_SWITCH);
        }

        public override PluginDynamicFolderNavigation GetNavigationArea(DeviceType _) => PluginDynamicFolderNavigation.None;

        public override IEnumerable<string> GetButtonPressActionNames(DeviceType deviceType)
        {
            return new[]
            {
                CreateCommandName("Navigation"),
                CreateCommandName("Beacon"),
                CreateCommandName("Landing"),
                CreateCommandName("Taxi"),
                CreateCommandName("Strobes"),
                CreateCommandName("Instruments"),
                CreateCommandName("Recognition"),
                CreateCommandName("Wing"),
                CreateCommandName("Logo"),
                CreateCommandName("Cabin"),
                CreateCommandName("Pedestal"),
                CreateCommandName("Glareshield"),
                CreateCommandName("Flashlight"),
                CreateCommandName("All")
            };
        }
        public override BitmapImage GetCommandImage(string actionParameter, PluginImageSize imageSize)
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
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(strobe.ControllerValue));
                        break;
                    case "Instruments":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(instrument.ControllerValue));
                        break;
                    case "Recognition":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(recognition.ControllerValue));
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
                    case "Flashlight":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(flash.ControllerValue));
                        break;
                    case "All":
                        break;
                }
                bitmapBuilder.DrawText(actionParameter);

                return bitmapBuilder.ToImage();
            }
        }

        public override void RunCommand(string actionParameter)
        {
            SimConnectDAO.Instance.setPlugin(Plugin);
            switch (actionParameter)
            {
                case "Navigation":
                    nav.SetControllerValue(1);
                    break;
                case "Beacon":
                    beacon.SetControllerValue(1);
                    break;
                case "Landing":
                    landing.SetControllerValue(1);
                    break;
                case "Taxi":
                    taxi.SetControllerValue(1);
                    break;
                case "Strobes":
                    strobe.SetControllerValue(1);
                    break;
                case "Instruments":
                    instrument.SetControllerValue(1);
                    break;
                case "Recognition":
                    recognition.SetControllerValue(1);
                    break;
                case "Wing":
                    wing.SetControllerValue(1);
                    break;
                case "Logo":
                    logo.SetControllerValue(1);
                    break;
                case "Cabin":
                    cabin.SetControllerValue(1);
                    break;
                case "Pedestal":
                    pedestal.SetControllerValue(1);
                    break;
                case "Glareshield":
                    glareshield.SetControllerValue(1);
                    break;
                case "Flashlight":
                    flash.SetControllerValue(1);
                    break;
                case "All":
                    allLights.SetControllerValue(1);
                    break;

            }
        }

        readonly Binding nav;
        readonly Binding beacon;
        readonly Binding landing;
        readonly Binding taxi;
        readonly Binding strobe;
        readonly Binding instrument;
        readonly Binding recognition;
        readonly Binding wing;
        readonly Binding logo;
        readonly Binding cabin;
        readonly Binding pedestal;
        readonly Binding glareshield;
        readonly Binding flash;
        readonly Binding allLights;
    }
}
