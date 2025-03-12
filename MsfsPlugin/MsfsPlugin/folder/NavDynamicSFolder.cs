namespace Loupedeck.MsfsPlugin.folder
{
    using System;
    using System.Collections.Generic;

    using Loupedeck.MsfsPlugin.tools;

    public class NavDynamicSFolder : DefaultFolder
    {
        public NavDynamicSFolder() : base("NAV (for S)")
        {
            Nav1ActiveFreq = Bind(BindingKeys.NAV1_ACTIVE_FREQUENCY);
            Nav2ActiveFreq = Bind(BindingKeys.NAV2_ACTIVE_FREQUENCY);
            Nav1StandbyFreq = Bind(BindingKeys.NAV1_STBY_FREQUENCY);
            Nav2StandbyFreq = Bind(BindingKeys.NAV2_STBY_FREQUENCY);
            Nav1Available = Bind(BindingKeys.NAV1_AVAILABLE);
            Nav2Available = Bind(BindingKeys.NAV2_AVAILABLE);
            Nav1Swap = Bind(BindingKeys.NAV1_RADIO_SWAP);
            Nav2Swap = Bind(BindingKeys.NAV2_RADIO_SWAP);
        }

        public override PluginDynamicFolderNavigation GetNavigationArea(DeviceType _) => PluginDynamicFolderNavigation.EncoderArea;

        public override IEnumerable<string> GetButtonPressActionNames(DeviceType deviceType)
        {
            return new[]
            {
                CreateCommandName("NAV1"),
                CreateCommandName("NAV1 Active Int"),
                CreateCommandName("NAV1 Active Float"),
                CreateCommandName("NAV1 Standby Int"),
                CreateCommandName("NAV1 Standby Float"),
                CreateCommandName("NAV2"),
                CreateCommandName("NAV2 Active Int"),
                CreateCommandName("NAV2 Active Float"),
                CreateCommandName("NAV2 Standby Int"),
                CreateCommandName("NAV2 Standby Float"),
                NavigateUpActionName,
            };
        }
        public override IEnumerable<string> GetEncoderRotateActionNames(DeviceType deviceType)
        {
            return new[]
            {
                CreateAdjustmentName ("Int"),
                CreateAdjustmentName ("Float"),
            };
        }

        public override IEnumerable<string> GetEncoderPressActionNames(DeviceType deviceType)
        {
            return new[]
            {
                CreateCommandName("Int Reset"),
                CreateCommandName("Float Reset"),
            };
        }

        public override string GetAdjustmentDisplayName(string actionParameter, PluginImageSize imageSize)
        {
            var binding = isVar1Active ? Nav1StandbyFreq : Nav2StandbyFreq;
            var ret = "";
            switch (actionParameter)
            {
                case "Int":
                    ret = "NAV\n" + Math.Truncate(binding.ControllerValue / 1000000f) + ".";
                    break;
                case "Float":
                    var var1dbl = Math.Round(binding.ControllerValue / 1000000f - Math.Truncate(binding.ControllerValue / 1000000f), 3).ToString();
                    ret = "NAV\n" + (var1dbl.Length > 2 ? var1dbl.Substring(2) : var1dbl).PadRight(2, '0');
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
                    case "NAV1":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(Nav1Available.MsfsValue));
                        bitmapBuilder.DrawText(isVar1Active ? "=> NAV1" : "NAV1");
                        break;
                    case "NAV2":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(Nav2Available.MsfsValue));
                        bitmapBuilder.DrawText(!isVar1Active ? "=> NAV2" : "NAV2");
                        break;
                    case "NAV1 Active Int":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(Nav1Available.MsfsValue));
                        bitmapBuilder.DrawText((Nav1ActiveFreq.ControllerValue == 0 ? "0" : Nav1ActiveFreq.ControllerValue.ToString().Substring(0, 3)) + ".", ImageTool.Green, 40);
                        break;
                    case "NAV1 Active Float":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(Nav1Available.MsfsValue));
                        bitmapBuilder.DrawText(Nav1ActiveFreq.ControllerValue == 0 ? "0" : Nav1ActiveFreq.ControllerValue.ToString().Substring(3, 2), ImageTool.Green, 40);
                        break;
                    case "NAV1 Standby Int":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(Nav1Available.MsfsValue));
                        bitmapBuilder.DrawText((Nav1StandbyFreq.ControllerValue == 0 ? "0" : Nav1StandbyFreq.ControllerValue.ToString().Substring(0, 3)) + ".", ImageTool.Yellow, 40);
                        break;
                    case "NAV1 Standby Float":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(Nav1Available.MsfsValue));
                        bitmapBuilder.DrawText(Nav1StandbyFreq.ControllerValue == 0 ? "0" : Nav1StandbyFreq.ControllerValue.ToString().Substring(3, 2), ImageTool.Yellow, 40);
                        break;
                    case "NAV2 Active Int":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(Nav2Available.MsfsValue));
                        bitmapBuilder.DrawText((Nav2ActiveFreq.ControllerValue == 0 ? "0" : Nav2ActiveFreq.ControllerValue.ToString().Substring(0, 3)) + ".", ImageTool.Green, 40);
                        break;
                    case "NAV2 Active Float":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(Nav2Available.MsfsValue));
                        bitmapBuilder.DrawText(Nav2ActiveFreq.ControllerValue == 0 ? "0" : Nav2ActiveFreq.ControllerValue.ToString().Substring(3, 2), ImageTool.Green, 40);
                        break;
                    case "NAV2 Standby Int":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(Nav2Available.MsfsValue));
                        bitmapBuilder.DrawText((Nav2StandbyFreq.ControllerValue == 0 ? "0" : Nav2StandbyFreq.ControllerValue.ToString().Substring(0, 3)) + ".", ImageTool.Yellow, 40);
                        break;
                    case "NAV2 Standby Float":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(Nav2Available.MsfsValue));
                        bitmapBuilder.DrawText(Nav2StandbyFreq.ControllerValue == 0 ? "0" : Nav2StandbyFreq.ControllerValue.ToString().Substring(3, 2), ImageTool.Yellow, 40);
                        break;
                }
                return bitmapBuilder.ToImage();
            }
        }

        public override void RunCommand(string actionParameter)
        {
            switch (actionParameter)
            {
                case "NAV1":
                    isVar1Active = true;
                    break;
                case "NAV2":
                    isVar1Active = false;
                    break;
                case "NAV1 Active":
                case "NAV1 Active Int":
                case "NAV1 Active Float":
                case "NAV1 Standby":
                case "NAV1 Standby Int":
                case "NAV1 Standby Float":
                    Nav1Swap.SetControllerValue(1);
                    break;
                case "NAV2 Active":
                case "NAV2 Active Int":
                case "NAV2 Active Float":
                case "NAV2 Standby":
                case "NAV2 Standby Int":
                case "NAV2 Standby Float":
                    Nav2Swap.SetControllerValue(1);
                    break;
                case "Int Reset":
                case "Float Reset":
                    if (isVar1Active)
                    {
                        Nav1Swap.SetControllerValue(1);
                    }
                    else
                    {
                        Nav2Swap.SetControllerValue(1);
                    }
                    break;
            }
        }

        public override void ApplyAdjustment(string actionParameter, int ticks)
        {
            var binding = isVar1Active ? Nav1StandbyFreq : Nav2StandbyFreq;
            switch (actionParameter)
            {
                case "Int":
                    var com1int = int.Parse(binding.ControllerValue.ToString().Substring(0, 3));
                    var com1dbl = int.Parse(binding.ControllerValue.ToString().Substring(3, 2));
                    var newInt = ConvertTool.ApplyAdjustment(com1int, ticks, 108, 117, 1, true);
                    binding.SetControllerValue(newInt * 1000000 + com1dbl * 10000);
                    break;
                case "Float":
                    var com1dbl1 = int.Parse(binding.ControllerValue.ToString().Substring(3, 2));
                    var com1int1 = int.Parse(binding.ControllerValue.ToString().Substring(0, 3));
                    var newFloat = ConvertTool.ApplyAdjustment(com1dbl1, ticks, 0, 99, 1, true);
                    binding.SetControllerValue(com1int1 * 1000000 + newFloat * 10000);
                    break;
            }
            EncoderActionNamesChanged();
            ButtonActionNamesChanged();
        }

        bool isVar1Active = true;

        readonly Binding Nav1ActiveFreq;
        readonly Binding Nav2ActiveFreq;
        readonly Binding Nav1StandbyFreq;
        readonly Binding Nav2StandbyFreq;
        readonly Binding Nav1Available;
        readonly Binding Nav2Available;
        readonly Binding Nav1Swap;
        readonly Binding Nav2Swap;
    }
}
