namespace Loupedeck.MsfsPlugin.folder
{
    using System;
    using System.Collections.Generic;

    using Loupedeck.MsfsPlugin.tools;

    public class AirlinerDynamicFolder : DefaultFolder
    {
        public AirlinerDynamicFolder() : base("Autopilot")
        {
            bindings.Add(ApAltSetting = Register(BindingKeys.AP_ALT));
            bindings.Add(PlaneAltitude = Register(BindingKeys.ALT));
            bindings.Add(ApHdgSetting = Register(BindingKeys.AP_HEADING));
            bindings.Add(PlaneHeading = Register(BindingKeys.HEADING));
            bindings.Add(ApSpeedSetting = Register(BindingKeys.AP_SPEED));
            bindings.Add(PlaneSpeed = Register(BindingKeys.SPEED));
            bindings.Add(ApVspeedSetting = Register(BindingKeys.AP_VSPEED));
            bindings.Add(PlaneVspeed = Register(BindingKeys.VSPEED));

            bindings.Add(FdSwitch = Register(BindingKeys.AP_FD_SWITCH));
            bindings.Add(AltSwitch = Register(BindingKeys.AP_ALT_SWITCH));
            bindings.Add(MasterSwitch = Register(BindingKeys.AP_MASTER_SWITCH));
            bindings.Add(NavSwitch = Register(BindingKeys.AP_NAV_SWITCH));
            bindings.Add(FlcSwitch = Register(BindingKeys.AP_FLC_SWITCH));
            bindings.Add(AprSwitch = Register(BindingKeys.AP_APP_SWITCH));
            bindings.Add(LocSwitch = Register(BindingKeys.AP_LOC_SWITCH));
            bindings.Add(SpeedSwitch = Register(BindingKeys.AP_SPEED_SWITCH));
            bindings.Add(HeadingSwitch = Register(BindingKeys.AP_HEAD_SWITCH));
            bindings.Add(ThrottleSwitch = Register(BindingKeys.AP_THROTTLE_SWITCH));
            bindings.Add(VspeedSwitch = Register(BindingKeys.AP_VSPEED_SWITCH));
            bindings.Add(YdSwitch = Register(BindingKeys.AP_YAW_DAMPER_SWITCH));
            bindings.Add(BcSwitch = Register(BindingKeys.AP_BC_SWITCH));
        }

        public override PluginDynamicFolderNavigation GetNavigationArea(DeviceType _) => PluginDynamicFolderNavigation.None;

        public override IEnumerable<string> GetEncoderRotateActionNames(DeviceType deviceType)
        {
            return new[]
            {
                CreateAdjustmentName ("Altitude Encoder"),
                CreateAdjustmentName ("Heading Encoder"),
                CreateAdjustmentName (""),
                CreateAdjustmentName ("Speed Encoder"),
                CreateAdjustmentName ("VS Speed Encoder"),
            };
        }

        public override IEnumerable<string> GetEncoderPressActionNames(DeviceType deviceType)
        {
            return new[]
            {
                CreateCommandName("Altitude Reset"),
                CreateCommandName("Heading Reset"),
                NavigateLeftActionName,
                CreateCommandName("Speed Reset"),
                CreateCommandName("VS Speed Reset"),
                NavigateRightActionName
            };
        }
        public override IEnumerable<string> GetButtonPressActionNames(DeviceType deviceType)
        {
            return new[]
            {
                NavigateUpActionName,
                CreateCommandName("FD"),
                CreateCommandName("AP"),
                CreateCommandName("Yaw Damp"),

                CreateCommandName("Altitude"),
                CreateCommandName("FLC"),
                CreateCommandName("Heading"),
                CreateCommandName("NAV"),

                CreateCommandName("LOC"),
                CreateCommandName("APR"),
                CreateCommandName("Speed"),
                CreateCommandName("VSpeed"),

                CreateCommandName("Throttle"),
                CreateCommandName("Back Crs"),
            };
        }

        public override string GetAdjustmentDisplayName(string actionParameter, PluginImageSize imageSize)
        {
            var ret = "";
            switch (actionParameter)
            {
                case "Altitude Encoder":
                    ret = "Alt\n[" + ApAltSetting.ControllerValue + "]\n" + PlaneAltitude.ControllerValue;
                    break;
                case "Heading Encoder":
                    ret = "Head\n[" + ApHdgSetting.ControllerValue + "]\n" + PlaneHeading.ControllerValue;
                    break;
                case "Speed Encoder":
                    ret = "Speed\n[" + ApSpeedSetting.ControllerValue + "]\n" + PlaneSpeed.ControllerValue;
                    break;
                case "VS Speed Encoder":
                    ret = "VS\n[" + ApVspeedSetting.ControllerValue + "]\n" + PlaneVspeed.ControllerValue;
                    break;
            }
            return ret;
        }
        public override BitmapImage GetCommandImage(string actionParameter, PluginImageSize imageSize)
        {
            using (var bitmapBuilder = new BitmapBuilder(imageSize))
            {
                switch (actionParameter)
                {
                    case "LOC":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(LocSwitch.ControllerValue));
                        break;
                    case "FD":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(FdSwitch.ControllerValue));
                        break;
                    case "FLC":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(FlcSwitch.ControllerValue));
                        break;
                    case "APR":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(AprSwitch.ControllerValue));
                        break;
                    case "Altitude":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(AltSwitch.ControllerValue));
                        break;
                    case "Heading":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(HeadingSwitch.ControllerValue));
                        break;
                    case "NAV":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(NavSwitch.ControllerValue));
                        break;
                    case "Speed":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(SpeedSwitch.ControllerValue));
                        break;
                    case "AP":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(MasterSwitch.ControllerValue));
                        break;
                    case "Throttle":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(ThrottleSwitch.ControllerValue));
                        break;
                    case "VSpeed":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(VspeedSwitch.ControllerValue));
                        break;
                    case "Yaw Damp":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(YdSwitch.ControllerValue));
                        break;
                    case "Back Crs":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(BcSwitch.ControllerValue));
                        break;
                }
                bitmapBuilder.DrawText(actionParameter);
                return bitmapBuilder.ToImage();
            }
        }

        public override void ApplyAdjustment(string actionParameter, int ticks)
        {
            switch (actionParameter)
            {
                case "Altitude Encoder":
                    ApAltSetting.SetControllerValue(ConvertTool.ApplyAdjustment(ApAltSetting.ControllerValue, ticks, -10000, 99900, 100));
                    break;
                case "Heading Encoder":
                    ApHdgSetting.SetControllerValue(ConvertTool.ApplyAdjustment(ApHdgSetting.ControllerValue, ticks, 1, 360, 1, true));
                    break;
                case "Speed Encoder":
                    ApSpeedSetting.SetControllerValue(ConvertTool.ApplyAdjustment(ApSpeedSetting.ControllerValue, ticks, 0, 2000, 1));
                    break;
                case "VS Speed Encoder":
                    ApVspeedSetting.SetControllerValue(ConvertTool.ApplyAdjustment(ApVspeedSetting.ControllerValue, ticks, -10000, 10000, 100));
                    break;
            }
            EncoderActionNamesChanged();
        }
        public override void RunCommand(string actionParameter)
        {
            switch (actionParameter)
            {
                case "LOC":
                    LocSwitch.SetControllerValue(1);
                    break;
                case "FD":
                    FdSwitch.SetControllerValue(1);
                    break;
                case "FLC":
                    FlcSwitch.SetControllerValue(1);
                    break;
                case "APR":
                    AprSwitch.SetControllerValue(1);
                    break;
                case "Altitude":
                    AltSwitch.SetControllerValue(1);
                    break;
                case "Heading":
                    HeadingSwitch.SetControllerValue(1);
                    break;
                case "NAV":
                    NavSwitch.SetControllerValue(1);
                    break;
                case "Speed":
                    SpeedSwitch.SetControllerValue(1);
                    break;
                case "AP":
                    MasterSwitch.SetControllerValue(1);
                    break;
                case "Throttle":
                    ThrottleSwitch.SetControllerValue(1);
                    break;
                case "VSpeed":
                    VspeedSwitch.SetControllerValue(1);
                    break;
                case "Yaw Damp":
                    YdSwitch.SetControllerValue(1);
                    break;
                case "Back Crs":
                    BcSwitch.SetControllerValue(1);
                    break;
                case "Altitude Reset":
                    ApAltSetting.SetControllerValue((long)(Math.Round(PlaneAltitude.ControllerValue / 100d, 0) * 100));
                    break;
                case "Heading Reset":
                    ApHdgSetting.SetControllerValue(PlaneHeading.ControllerValue);
                    break;
                case "Speed Reset":
                    ApSpeedSetting.SetControllerValue((long)(Math.Round(PlaneSpeed.ControllerValue / 100d, 0) * 100));
                    break;
                case "VS Speed Reset":
                    ApVspeedSetting.SetControllerValue((long)(Math.Round(PlaneVspeed.ControllerValue / 100d, 0) * 100));
                    break;
            }
        }

        readonly Binding ApAltSetting;
        readonly Binding PlaneAltitude;
        readonly Binding ApHdgSetting;
        readonly Binding PlaneHeading;
        readonly Binding ApSpeedSetting;
        readonly Binding PlaneSpeed;
        readonly Binding ApVspeedSetting;
        readonly Binding PlaneVspeed;
        readonly Binding FdSwitch;
        readonly Binding AltSwitch;
        readonly Binding MasterSwitch;
        readonly Binding NavSwitch;
        readonly Binding FlcSwitch;
        readonly Binding AprSwitch;
        readonly Binding LocSwitch;
        readonly Binding SpeedSwitch;
        readonly Binding HeadingSwitch;
        readonly Binding ThrottleSwitch;
        readonly Binding VspeedSwitch;
        readonly Binding YdSwitch;
        readonly Binding BcSwitch;
    }
}
