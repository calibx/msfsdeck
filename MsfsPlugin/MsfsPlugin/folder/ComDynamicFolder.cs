namespace Loupedeck.MsfsPlugin.folder
{
    using System;
    using System.Collections.Generic;

    using Loupedeck.MsfsPlugin.tools;

    public class ComDynamicFolder : DefaultFolder
    {
        public ComDynamicFolder() : base("COM")
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

        public override PluginDynamicFolderNavigation GetNavigationArea(DeviceType _) => PluginDynamicFolderNavigation.None;

        public override IEnumerable<string> GetButtonPressActionNames(DeviceType deviceType)
        {
            return new[]
            {
                CreateCommandName("COM1 Active Int"),
                CreateCommandName("COM1 Active Float"),
                CreateCommandName("COM1 Standby Int"),
                CreateCommandName("COM1 Standby Float"),
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
                CreateAdjustmentName ("COM1 Int Encoder"),
                CreateAdjustmentName ("COM2 Int Encoder"),
                CreateAdjustmentName (""),
                CreateAdjustmentName ("COM1 Float Encoder"),
                CreateAdjustmentName ("COM2 Float Encoder"),
            };
        }

        public override IEnumerable<string> GetEncoderPressActionNames(DeviceType deviceType)
        {
            return new[]
            {
                CreateCommandName("COM1 Int Reset"),
                CreateCommandName("COM2 Int Reset"),
                CreateCommandName (""),
                CreateCommandName("COM1 Float Reset"),
                CreateCommandName("COM2 Float Reset"),
            };
        }

        public override string GetAdjustmentDisplayName(string actionParameter, PluginImageSize imageSize)
        {
            var ret = "";
            switch (actionParameter)
            {
                case "COM1 Int Encoder":
                    ret = "COM1\n" + Math.Truncate(com1Stby.ControllerValue / 1000000f) + ".";
                    break;
                case "COM1 Float Encoder":
                    var com1dbl = Math.Round(com1Stby.ControllerValue / 1000000f - Math.Truncate(com1Stby.ControllerValue / 1000000f), 3).ToString();
                    ret = "COM1\n" + (com1dbl.Length > 2 ? com1dbl.Substring(2) : com1dbl).PadRight(3, '0');
                    break;
                case "COM2 Int Encoder":
                    ret = "COM2\n" + Math.Truncate(com2Stby.ControllerValue / 1000000f) + ".";
                    break;
                case "COM2 Float Encoder":
                    var com2dbl = Math.Round(com2Stby.ControllerValue / 1000000f - Math.Truncate(com2Stby.ControllerValue / 1000000f), 3).ToString();
                    ret = "COM2\n" + (com2dbl.Length > 2 ? com2dbl.Substring(2) : com2dbl).PadRight(3, '0');
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
                case "COM1 Active":
                case "COM1 Active Int":
                case "COM1 Active Float":
                case "COM1 Standby":
                case "COM1 Standby Int":
                case "COM1 Standby Float":
                case "COM1 Int Reset":
                case "COM1 Float Reset":
                    com1Swap.SetControllerValue(1);
                    break;
                case "COM2 Active":
                case "COM2 Active Int":
                case "COM2 Active Float":
                case "COM2 Standby":
                case "COM2 Standby Int":
                case "COM2 Standby Float":
                case "COM2 Int Reset":
                case "COM2 Float Reset":
                    com2Swap.SetControllerValue(1);
                    break;
            }
        }

        public override void ApplyAdjustment(string actionParameter, int ticks)
        {
            switch (actionParameter)
            {
                case "COM1 Int Encoder":
                    IncrIntValue(com1Stby, ticks);
                    break;
                case "COM1 Float Encoder":
                    IncrDecimalValue(com1Stby, ticks);
                    break;
                case "COM2 Int Encoder":
                    IncrIntValue(com2Stby, ticks);
                    break;
                case "COM2 Float Encoder":
                    IncrDecimalValue(com2Stby, ticks);
                    break;

            }
            EncoderActionNamesChanged();
            ButtonActionNamesChanged();
        }

        private void IncrIntValue(Binding binding, int ticks) => binding.SetControllerValue(adjuster.IncrIntValue(binding.ControllerValue, ticks));

        private void IncrDecimalValue(Binding binding, int ticks) => binding.SetControllerValue(adjuster.IncrDecimalValue(binding.ControllerValue, ticks));

        private readonly DecimalValueAdjuster adjuster = new DecimalValueAdjuster(118, 136, 0, 995, 5, 1000000);

        private readonly Binding com1Active;
        private readonly Binding com2Active;
        private readonly Binding com1Available;
        private readonly Binding com2Available;
        private readonly Binding com1Stby;
        private readonly Binding com2Stby;
        private readonly Binding com1Swap;
        private readonly Binding com2Swap;
    }
}
