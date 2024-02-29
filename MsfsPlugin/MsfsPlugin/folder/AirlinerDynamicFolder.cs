﻿namespace Loupedeck.MsfsPlugin.folder
{
    using System;
    using System.Collections.Generic;

    using Loupedeck.MsfsPlugin.tools;

    public class AirlinerDynamicFolder : DefaultFolder
    {
        public AirlinerDynamicFolder() : base("Airliner")
        {
            bindings.Add(Register(BindingKeys.AP_ALT));
            bindings.Add(Register(BindingKeys.ALT));
            bindings.Add(Register(BindingKeys.AP_HEADING));
            bindings.Add(Register(BindingKeys.HEADING));
            bindings.Add(Register(BindingKeys.AP_SPEED));
            bindings.Add(Register(BindingKeys.SPEED));
            bindings.Add(Register(BindingKeys.AP_VSPEED));
            bindings.Add(Register(BindingKeys.VSPEED));

            bindings.Add(Register(BindingKeys.AP_FD_SWITCH_AL_FOLDER));
            bindings.Add(Register(BindingKeys.AP_ALT_SWITCH));
            bindings.Add(Register(BindingKeys.AP_MASTER_SWITCH));
            bindings.Add(Register(BindingKeys.AP_NAV_SWITCH));
            bindings.Add(Register(BindingKeys.AP_FLC_SWITCH_AL_FOLDER));
            bindings.Add(Register(BindingKeys.AP_APP_SWITCH_AL_FOLDER));
            bindings.Add(Register(BindingKeys.AP_LOC_SWITCH_AL_FOLDER));
            bindings.Add(Register(BindingKeys.AP_SPEED_SWITCH));
            bindings.Add(Register(BindingKeys.AP_HEAD_SWITCH));
            bindings.Add(Register(BindingKeys.AP_THROTTLE_SWITCH));
            bindings.Add(Register(BindingKeys.AP_VSPEED_SWITCH));
            bindings.Add(Register(BindingKeys.AP_YAW_DAMPER_SWITCH));
            bindings.Add(Register(BindingKeys.AP_BC_SWITCH));
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
                CreateCommandName("Altitude"),
                CreateCommandName("AP"),
                CreateCommandName("GPS"),
                CreateCommandName("FLC"),
                CreateCommandName("APP"),
                CreateCommandName("LOC"),
                CreateCommandName("Speed"),
                CreateCommandName("Heading"),
                CreateCommandName("Throttle"),
                CreateCommandName("VS Speed"),
                CreateCommandName("Yaw Damper"),
                CreateCommandName("Back Course"),
            };
        }

        public override string GetAdjustmentDisplayName(string actionParameter, PluginImageSize imageSize)
        {
            var ret = "";
            switch (actionParameter)
            {
                case "Altitude Encoder":
                    ret = "Alt\n[" + bindings[0].ControllerValue + "]\n" + bindings[1].ControllerValue;
                    break;
                case "Heading Encoder":
                    ret = "Head\n[" + bindings[2].ControllerValue + "]\n" + bindings[3].ControllerValue;
                    break;
                case "Speed Encoder":
                    ret = "Speed\n[" + bindings[4].ControllerValue + "]\n" + bindings[5].ControllerValue;
                    break;
                case "VS Speed Encoder":
                    ret = "VS\n[" + bindings[6].ControllerValue + "]\n" + bindings[7].ControllerValue;
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
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(bindings[14].ControllerValue));
                        break;
                    case "FD":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(bindings[8].ControllerValue));
                        break;
                    case "FLC":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(bindings[12].ControllerValue));
                        break;
                    case "APP":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(bindings[13].ControllerValue));
                        break;
                    case "Altitude":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(bindings[9].ControllerValue));
                        break;
                    case "Heading":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(bindings[16].ControllerValue));
                        break;
                    case "GPS":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(bindings[11].ControllerValue));
                        break;
                    case "Speed":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(bindings[15].ControllerValue));
                        break;
                    case "AP":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(bindings[10].ControllerValue));
                        break;
                    case "Throttle":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(bindings[17].ControllerValue));
                        break;
                    case "VS Speed":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(bindings[18].ControllerValue));
                        break;
                    case "Yaw Damper":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(bindings[19].ControllerValue));
                        break;
                    case "Back Course":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(bindings[20].ControllerValue));
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
                    bindings[0].SetControllerValue(ConvertTool.ApplyAdjustment(bindings[0].ControllerValue, ticks, -10000, 99900, 100));
                    break;
                case "Heading Encoder":
                    bindings[2].SetControllerValue(ConvertTool.ApplyAdjustment(bindings[2].ControllerValue, ticks, 1, 360, 1, true));
                    break;
                case "Speed Encoder":
                    bindings[4].SetControllerValue(ConvertTool.ApplyAdjustment(bindings[4].ControllerValue, ticks, 0, 2000, 1));
                    break;
                case "VS Speed Encoder":
                    bindings[6].SetControllerValue(ConvertTool.ApplyAdjustment(bindings[6].ControllerValue, ticks, -10000, 10000, 100));
                    break;
            }
            EncoderActionNamesChanged();
        }
        public override void RunCommand(string actionParameter)
        {
            switch (actionParameter)
            {
                case "LOC":
                    bindings[14].SetControllerValue(1);
                    break;
                case "FD":
                    bindings[8].SetControllerValue(1);
                    break;
                case "FLC":
                    bindings[12].SetControllerValue(1);
                    break;
                case "APP":
                    bindings[13].SetControllerValue(1);
                    break;
                case "Altitude":
                    bindings[9].SetControllerValue(1);
                    break;
                case "Heading":
                    bindings[16].SetControllerValue(1);
                    break;
                case "GPS":
                    bindings[11].SetControllerValue(1);
                    break;
                case "Speed":
                    bindings[15].SetControllerValue(1);
                    break;
                case "AP":
                    bindings[10].SetControllerValue(1);
                    break;
                case "Throttle":
                    bindings[17].SetControllerValue(1);
                    break;
                case "VS Speed":
                    bindings[18].SetControllerValue(1);
                    break;
                case "Yaw Damper":
                    bindings[19].SetControllerValue(1);
                    break;
                case "Back Course":
                    bindings[20].SetControllerValue(1);
                    break;
                case "Altitude Reset":
                    bindings[0].SetControllerValue((long)(Math.Round(bindings[1].ControllerValue / 100d, 0) * 100));
                    break;
                case "Heading Reset":
                    bindings[2].SetControllerValue(bindings[3].ControllerValue);
                    break;
                case "Speed Reset":
                    bindings[4].SetControllerValue((long)(Math.Round(bindings[5].ControllerValue / 100d, 0) * 100));
                    break;
                case "VS Speed Reset":
                    bindings[6].SetControllerValue((long)(Math.Round(bindings[7].ControllerValue / 100d, 0) * 100));
                    break;
            }
        }
    }
}
