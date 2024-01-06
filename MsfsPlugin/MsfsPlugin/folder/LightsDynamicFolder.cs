namespace Loupedeck.MsfsPlugin.folder
{
    using System.Collections.Generic;

    using Loupedeck.MsfsPlugin.msfs;
    using Loupedeck.MsfsPlugin.tools;

    public class LightsDynamicFolder : DefaultFolder
    {
        public LightsDynamicFolder() : base("Lights")
        {
            bindings.Add(Register(BindingKeys.LIGHT_NAV));
            bindings.Add(Register(BindingKeys.LIGHT_BEACON));
            bindings.Add(Register(BindingKeys.LIGHT_LANDING));
            bindings.Add(Register(BindingKeys.LIGHT_TAXI));
            bindings.Add(Register(BindingKeys.LIGHT_STROBE));
            bindings.Add(Register(BindingKeys.LIGHT_INSTRUMENT));
            bindings.Add(Register(BindingKeys.LIGHT_RECOG));
            bindings.Add(Register(BindingKeys.LIGHT_WING));
            bindings.Add(Register(BindingKeys.LIGHT_LOGO));
            bindings.Add(Register(BindingKeys.LIGHT_CABIN));
            bindings.Add(Register(BindingKeys.LIGHT_PEDESTAL));
            bindings.Add(Register(BindingKeys.LIGHT_GLARESHIELD));
            bindings.Add(Register(BindingKeys.FLASHLIGHT));
            bindings.Add(Register(BindingKeys.LIGHT_ALL_SWITCH));
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
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(bindings[0].ControllerValue));
                        break;
                    case "Beacon":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(bindings[1].ControllerValue));
                        break;
                    case "Landing":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(bindings[2].ControllerValue));
                        break;
                    case "Taxi":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(bindings[3].ControllerValue));
                        break;
                    case "Strobes":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(bindings[4].ControllerValue));
                        break;
                    case "Instruments":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(bindings[5].ControllerValue));
                        break;
                    case "Recognition":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(bindings[6].ControllerValue));
                        break;
                    case "Wing":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(bindings[7].ControllerValue));
                        break;
                    case "Logo":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(bindings[8].ControllerValue));
                        break;
                    case "Cabin":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(bindings[9].ControllerValue));
                        break;
                    case "Pedestal":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(bindings[10].ControllerValue));
                        break;
                    case "Glareshield":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(bindings[11].ControllerValue));
                        break;
                    case "Flashlight":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(bindings[12].ControllerValue));
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
                    bindings[0].SetControllerValue(1);
                    break;
                case "Beacon":
                    bindings[1].SetControllerValue(1);
                    break;
                case "Landing":
                    bindings[2].SetControllerValue(1);
                    break;
                case "Taxi":
                    bindings[3].SetControllerValue(1);
                    break;
                case "Strobes":
                    bindings[4].SetControllerValue(1);
                    break;
                case "Instruments":
                    bindings[5].SetControllerValue(1);
                    break;
                case "Recognition":
                    bindings[6].SetControllerValue(1);
                    break;
                case "Wing":
                    bindings[7].SetControllerValue(1);
                    break;
                case "Logo":
                    bindings[8].SetControllerValue(1);
                    break;
                case "Cabin":
                    bindings[9].SetControllerValue(1);
                    break;
                case "Pedestal":
                    bindings[10].SetControllerValue(1);
                    break;
                case "Glareshield":
                    bindings[11].SetControllerValue(1);
                    break;
                case "Flashlight":
                    bindings[12].SetControllerValue(1);
                    break;
                case "All":
                    bindings[13].SetControllerValue(1);
                    break;

            }
        }
    }
}
