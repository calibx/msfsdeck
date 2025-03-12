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
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(allLights.ControllerValue));
                        break;
                }
                bitmapBuilder.DrawText(actionParameter);

                return bitmapBuilder.ToImage();
            }
        }

        public override void RunCommand(string actionParameter)
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
                    strobe.ToggleControllerValue();
                    break;
                case "Instruments":
                    instrument.ToggleControllerValue();
                    break;
                case "Recognition":
                    recognition.ToggleControllerValue();
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
                case "Flashlight":
                    flash.ToggleControllerValue();
                    break;
                case "All":
                    allLights.ToggleControllerValue();
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
