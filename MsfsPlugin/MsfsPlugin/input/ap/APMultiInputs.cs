namespace Loupedeck.MsfsPlugin.folder
{
    using Loupedeck.MsfsPlugin.input;
    using Loupedeck.MsfsPlugin.tools;

    public class APMultiInputs : DefaultInput
    {
        public APMultiInputs()
        {
            bindings.Add(Register(BindingKeys.AP_ALT_SWITCH));
            bindings.Add(Register(BindingKeys.AP_HEAD_SWITCH));
            bindings.Add(Register(BindingKeys.AP_NAV_SWITCH));
            bindings.Add(Register(BindingKeys.AP_SPEED_SWITCH));
            bindings.Add(Register(BindingKeys.AP_MASTER_SWITCH));
            bindings.Add(Register(BindingKeys.AP_THROTTLE_SWITCH));
            bindings.Add(Register(BindingKeys.AP_VSPEED_SWITCH));
            bindings.Add(Register(BindingKeys.AP_FD_SWITCH));
            bindings.Add(Register(BindingKeys.AP_FLC_SWITCH));
            bindings.Add(Register(BindingKeys.AP_APP_SWITCH));
            bindings.Add(Register(BindingKeys.AP_LOC_SWITCH));
            bindings.Add(Register(BindingKeys.AP_YAW_DAMPER_SWITCH));
            bindings.Add(Register(BindingKeys.AP_BC_SWITCH));

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
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(bindings[0].ControllerValue));
                        break;
                    case "AP Head":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(bindings[1].ControllerValue));
                        break;
                    case "AP Nav":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(bindings[2].ControllerValue));
                        break;
                    case "AP Speed":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(bindings[3].ControllerValue));
                        break;
                    case "AP Master":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(bindings[4].ControllerValue));
                        break;
                    case "AP Throttle":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(bindings[5].ControllerValue));
                        break;
                    case "AP VSpeed":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(bindings[6].ControllerValue));
                        break;
                    case "AP FD":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(bindings[7].ControllerValue));
                        break;
                    case "AP FLC":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(bindings[8].ControllerValue));
                        break;
                    case "AP APP":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(bindings[9].ControllerValue));
                        break;
                    case "AP LOC":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(bindings[10].ControllerValue));
                        break;
                    case "AP YD":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(bindings[11].ControllerValue));
                        break;
                    case "AP BC":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(bindings[12].ControllerValue));
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
                    bindings[0].SetControllerValue(1);
                    break;
                case "AP Head":
                    bindings[1].SetControllerValue(1);
                    break;
                case "AP Nav":
                    bindings[2].SetControllerValue(1);
                    break;
                case "AP Speed":
                    bindings[3].SetControllerValue(1);
                    break;
                case "AP Master":
                    bindings[4].SetControllerValue(1);
                    break;
                case "AP Throttle":
                    bindings[5].SetControllerValue(1);
                    break;
                case "AP VSpeed":
                    bindings[6].SetControllerValue(1);
                    break;
                case "AP FD":
                    bindings[7].SetControllerValue(1);
                    break;
                case "AP FLC":
                    bindings[8].SetControllerValue(1);
                    break;
                case "AP APP":
                    bindings[9].SetControllerValue(1);
                    break;
                case "AP LOC":
                    bindings[10].SetControllerValue(1);
                    break;
                case "AP YD":
                    bindings[11].SetControllerValue(1);
                    break;
                case "AP BC":
                    bindings[12].SetControllerValue(1);
                    break;
            }
        }
    }
}
