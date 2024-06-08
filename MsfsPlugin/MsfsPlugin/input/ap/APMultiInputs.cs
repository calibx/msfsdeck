namespace Loupedeck.MsfsPlugin.folder
{
    using Loupedeck.MsfsPlugin.input;
    using Loupedeck.MsfsPlugin.tools;

    public class APMultiInputs : DefaultInput
    {
        public APMultiInputs()
        {
            altSwitch = Bind(BindingKeys.AP_ALT_SWITCH);
            headSwitch = Bind(BindingKeys.AP_HEAD_SWITCH);
            navSwitch = Bind(BindingKeys.AP_NAV_SWITCH);
            speedSwitch = Bind(BindingKeys.AP_SPEED_SWITCH);
            masterSwitch = Bind(BindingKeys.AP_MASTER_SWITCH);
            throttleSwitch = Bind(BindingKeys.AP_THROTTLE_SWITCH);
            vspeedSwitch = Bind(BindingKeys.AP_VSPEED_SWITCH);
            fdSwitch = Bind(BindingKeys.AP_FD_SWITCH);
            flcSwitch = Bind(BindingKeys.AP_FLC_SWITCH);
            appSwitch = Bind(BindingKeys.AP_APP_SWITCH);
            locSwitch = Bind(BindingKeys.AP_LOC_SWITCH);
            yawDamperSwitch = Bind(BindingKeys.AP_YAW_DAMPER_SWITCH);
            bcSwitch = Bind(BindingKeys.AP_BC_SWITCH);

            AddParameter("AP Alt", "Autopilot Altitude Switch", "AP");
            AddParameter("AP Head", "Autopilot Heading Switch", "AP");
            AddParameter("AP Nav", "Autopilot Nav Switch", "AP");
            AddParameter("AP Speed", "Autopilot Speed Switch", "AP");
            AddParameter("AP Master", "Autopilot Master Switch", "AP");
            AddParameter("AP Throttle", "Autopilot ThrottleSwitch", "AP");
            AddParameter("AP VSpeed", "Autopilot VSpeed Switch", "AP");
            AddParameter("AP FD", "Autopilot FD Switch", "AP");
            AddParameter("AP FLC", "Autopilot FLC Switch", "AP");
            AddParameter("AP APP", "Autopilot APP Switch", "AP");
            AddParameter("AP LOC", "Autopilot LOC Switch", "AP");
            AddParameter("AP YD", "Autopilot Yaw Damper Switch", "AP");
            AddParameter("AP BC", "Autopilot Back Course Switch", "AP");
        }

        protected override BitmapImage GetCommandImage(string actionParameter, PluginImageSize imageSize)
        {
            using (var bitmapBuilder = new BitmapBuilder(imageSize))
            {
                switch (actionParameter)
                {
                    case "AP Alt":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(altSwitch.ControllerValue));
                        break;
                    case "AP Head":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(headSwitch.ControllerValue));
                        break;
                    case "AP Nav":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(navSwitch.ControllerValue));
                        break;
                    case "AP Speed":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(speedSwitch.ControllerValue));
                        break;
                    case "AP Master":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(masterSwitch.ControllerValue));
                        break;
                    case "AP Throttle":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(throttleSwitch.ControllerValue));
                        break;
                    case "AP VSpeed":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(vspeedSwitch.ControllerValue));
                        break;
                    case "AP FD":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(fdSwitch.ControllerValue));
                        break;
                    case "AP FLC":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(flcSwitch.ControllerValue));
                        break;
                    case "AP APP":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(appSwitch.ControllerValue));
                        break;
                    case "AP LOC":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(locSwitch.ControllerValue));
                        break;
                    case "AP YD":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(yawDamperSwitch.ControllerValue));
                        break;
                    case "AP BC":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(bcSwitch.ControllerValue));
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
                case "AP Alt":
                    altSwitch.SetControllerValue(1);
                    break;
                case "AP Head":
                    headSwitch.SetControllerValue(1);
                    break;
                case "AP Nav":
                    navSwitch.SetControllerValue(1);
                    break;
                case "AP Speed":
                    speedSwitch.SetControllerValue(1);
                    break;
                case "AP Master":
                    masterSwitch.SetControllerValue(1);
                    break;
                case "AP Throttle":
                    throttleSwitch.SetControllerValue(1);
                    break;
                case "AP VSpeed":
                    vspeedSwitch.SetControllerValue(1);
                    break;
                case "AP FD":
                    fdSwitch.SetControllerValue(1);
                    break;
                case "AP FLC":
                    flcSwitch.SetControllerValue(1);
                    break;
                case "AP APP":
                    appSwitch.SetControllerValue(1);
                    break;
                case "AP LOC":
                    locSwitch.SetControllerValue(1);
                    break;
                case "AP YD":
                    yawDamperSwitch.SetControllerValue(1);
                    break;
                case "AP BC":
                    bcSwitch.SetControllerValue(1);
                    break;
            }
        }

        readonly Binding altSwitch;
        readonly Binding headSwitch;
        readonly Binding navSwitch;
        readonly Binding speedSwitch;
        readonly Binding masterSwitch;
        readonly Binding throttleSwitch;
        readonly Binding vspeedSwitch;
        readonly Binding fdSwitch;
        readonly Binding flcSwitch;
        readonly Binding appSwitch;
        readonly Binding locSwitch;
        readonly Binding yawDamperSwitch;
        readonly Binding bcSwitch;
    }
}
