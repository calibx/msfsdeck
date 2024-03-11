namespace Loupedeck.MsfsPlugin.folder
{
    using System;
    using System.Collections.Generic;

    using Loupedeck.MsfsPlugin.tools;

    public class ComDynamicSFolder : DefaultFolder
    {
        public ComDynamicSFolder() : base("COM (for S)")
        {
            com1Active = Bind(BindingKeys.COM1_ACTIVE_FREQUENCY);
            com2Active = Bind(BindingKeys.COM2_ACTIVE_FREQUENCY);
            com1Available = Bind(BindingKeys.COM1_AVAILABLE);
            com2Available = Bind(BindingKeys.COM2_AVAILABLE);
            com1Stby = Bind(BindingKeys.COM1_STBY);
            com2Stby = Bind(BindingKeys.COM2_STBY);
            com1Swap = Bind(BindingKeys.COM1_RADIO_SWAP);
            com2Swap = Bind(BindingKeys.COM2_RADIO_SWAP);
        }

        public override PluginDynamicFolderNavigation GetNavigationArea(DeviceType _) => PluginDynamicFolderNavigation.EncoderArea;

        public override IEnumerable<string> GetButtonPressActionNames(DeviceType deviceType)
        {
            return new[]
            {
                CreateCommandName("COM1"),
                CreateCommandName("COM1 Active Int"),
                CreateCommandName("COM1 Active Float"),
                CreateCommandName("COM1 Standby Int"),
                CreateCommandName("COM1 Standby Float"),
                CreateCommandName("COM2"),
                CreateCommandName("COM2 Active Int"),
                CreateCommandName("COM2 Active Float"),
                CreateCommandName("COM2 Standby Int"),
                CreateCommandName("COM2 Standby Float"),
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
            var binding = isCom1active ? com1Stby : com2Stby;
            var ret = "";
            switch (actionParameter)
            {
                case "Int":
                    ret = "COM\n" + Math.Truncate(binding.ControllerValue / 1000000f) + ".";
                    break;
                case "Float":
                    var com1dbl = Math.Round(binding.ControllerValue / 1000000f - Math.Truncate(binding.ControllerValue / 1000000f), 3).ToString();
                    ret = "COM\n" + (com1dbl.Length > 2 ? com1dbl.Substring(2) : com1dbl).PadRight(3, '0');
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
                    case "COM1":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(com1Available.MsfsValue));
                        bitmapBuilder.DrawText(isCom1active ? "=> COM1" : "COM1");
                        break;
                    case "COM2":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(com2Available.MsfsValue));
                        bitmapBuilder.DrawText(!isCom1active ? "=> COM2" : "COM2");
                        break;
                    case "COM1 Active Int":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(com1Available.MsfsValue));
                        bitmapBuilder.DrawText((com1Active.ControllerValue == 0 ? "0" : com1Active.ControllerValue.ToString().Substring(0, 3)) + ".", ImageTool.Green, 40);
                        break;
                    case "COM1 Active Float":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(com1Available.MsfsValue));
                        bitmapBuilder.DrawText(com1Active.ControllerValue == 0 ? "0" : com1Active.ControllerValue.ToString().Substring(3, 3), ImageTool.Green, 40);
                        break;
                    case "COM1 Standby Int":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(com1Available.MsfsValue));
                        bitmapBuilder.DrawText((com1Stby.ControllerValue == 0 ? "0" : com1Stby.ControllerValue.ToString().Substring(0, 3)) + ".", ImageTool.Yellow, 40);
                        break;
                    case "COM1 Standby Float":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(com1Available.MsfsValue));
                        bitmapBuilder.DrawText(com1Stby.ControllerValue == 0 ? "0" : com1Stby.ControllerValue.ToString().Substring(3, 3), ImageTool.Yellow, 40);
                        break;
                    case "COM2 Active Int":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(com2Available.MsfsValue));
                        bitmapBuilder.DrawText((com2Active.ControllerValue == 0 ? "0" : com2Active.ControllerValue.ToString().Substring(0, 3)) + ".", ImageTool.Green, 40);
                        break;
                    case "COM2 Active Float":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(com2Available.MsfsValue));
                        bitmapBuilder.DrawText(com2Active.ControllerValue == 0 ? "0" : com2Active.ControllerValue.ToString().Substring(3, 3), ImageTool.Green, 40);
                        break;
                    case "COM2 Standby Int":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(com2Available.MsfsValue));
                        bitmapBuilder.DrawText((com2Stby.ControllerValue == 0 ? "0" : com2Stby.ControllerValue.ToString().Substring(0, 3)) + ".", ImageTool.Yellow, 40);
                        break;
                    case "COM2 Standby Float":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(com2Available.MsfsValue));
                        bitmapBuilder.DrawText(com2Stby.ControllerValue == 0 ? "0" : com2Stby.ControllerValue.ToString().Substring(3, 3), ImageTool.Yellow, 40);
                        break;
                }
                return bitmapBuilder.ToImage();
            }
        }

        public override void RunCommand(string actionParameter)
        {
            switch (actionParameter)
            {
                case "COM1":
                    isCom1active = true;
                    break;
                case "COM2":
                    isCom1active = false;
                    break;
                case "COM1 Active Int":
                case "COM1 Active Float":
                case "COM1 Standby Int":
                case "COM1 Standby Float":
                    com1Swap.SetControllerValue(1);
                    break;
                case "COM2 Active Int":
                case "COM2 Active Float":
                case "COM2 Standby Int":
                case "COM2 Standby Float":
                    com2Swap.SetControllerValue(1);
                    break;
                case "Int Reset":
                case "Float Reset":
                    if (isCom1active)
                        com1Swap.SetControllerValue(1);
                    else
                        com2Swap.SetControllerValue(1);
                    break;
            }
        }

        public override void ApplyAdjustment(string actionParameter, int ticks)
        {
            var binding = isCom1active ? com1Stby : com2Stby;
            switch (actionParameter)
            {
                case "Int":
                    var com1int = int.Parse(binding.ControllerValue.ToString().Substring(0, 3));
                    var com1dbl = int.Parse(binding.ControllerValue.ToString().Substring(3, 3));
                    var newInt = ConvertTool.ApplyAdjustment(com1int, ticks, 118, 136, 1, true);
                    binding.SetControllerValue(newInt * 1000000 + com1dbl * 1000);
                    break;
                case "Float":
                    var com1dbl1 = int.Parse(binding.ControllerValue.ToString().Substring(3, 3));
                    var com1int1 = int.Parse(binding.ControllerValue.ToString().Substring(0, 3));
                    var newFloat = ConvertTool.ApplyAdjustment(com1dbl1, ticks, 0, 995, 5, true);
                    binding.SetControllerValue(com1int1 * 1000000 + newFloat * 1000);
                    break;
            }
            EncoderActionNamesChanged();
            ButtonActionNamesChanged();
        }

        private bool isCom1active = true;

        private readonly Binding com1Active;
        private readonly Binding com2Active;
        private readonly Binding com1Stby;
        private readonly Binding com2Stby;
        private readonly Binding com1Available;
        private readonly Binding com2Available;
        private readonly Binding com1Swap;
        private readonly Binding com2Swap;
    }
}
