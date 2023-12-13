namespace Loupedeck.MsfsPlugin.folder
{
    using System;
    using System.Collections.Generic;

    using Loupedeck.MsfsPlugin.tools;

    public class ComDynamicFolder : PluginDynamicFolder, INotifiable
    {
        public ComDynamicFolder()
        {
            DisplayName = "COM";
            GroupName = "Folder";

            bindings.Add(MsfsData.Instance.Register(BindingKeys.COM1_ACTIVE_FREQUENCY));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.COM2_ACTIVE_FREQUENCY));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.COM3_ACTIVE_FREQUENCY));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.COM1_ACTIVE_FREQUENCY_TYPE));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.COM2_ACTIVE_FREQUENCY_TYPE));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.COM3_ACTIVE_FREQUENCY_TYPE));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.COM1_STATUS));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.COM2_STATUS));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.COM3_STATUS));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.COM1_AVAILABLE));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.COM2_AVAILABLE));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.COM3_AVAILABLE));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.COM1_STBY));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.COM2_STBY));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.COM3_STBY));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.COM1_RADIO_SWAP));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.COM2_RADIO_SWAP));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.COM3_RADIO_SWAP));

            MsfsData.Instance.Register(this);
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
                    ret = "COM1\n" + Math.Truncate(bindings[12].ControllerValue / 1000000f) + ".";
                    break;
                case "COM1 Float Encoder":
                    var com1dbl = Math.Round(bindings[12].ControllerValue / 1000000f - Math.Truncate(bindings[12].ControllerValue / 1000000f), 3).ToString();
                    ret = "COM1\n" + (com1dbl.Length > 2 ? com1dbl.Substring(2) : com1dbl).PadRight(3, '0');
                    break;
                case "COM2 Int Encoder":
                    ret = "COM2\n" + Math.Truncate(bindings[13].ControllerValue / 1000000f) + ".";
                    break;
                case "COM2 Float Encoder":
                    var com2dbl = Math.Round(bindings[13].ControllerValue / 1000000f - Math.Truncate(bindings[13].ControllerValue / 1000000f), 3).ToString();
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
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(bindings[9].MsfsValue));
                        bitmapBuilder.DrawText((bindings[0].ControllerValue == 0 ? "0" : bindings[0].ControllerValue.ToString().Substring(0, 3)) + ".", ImageTool.Green, 40);
                        break;
                    case "COM1 Active Float":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(bindings[9].MsfsValue));
                        bitmapBuilder.DrawText(bindings[0].ControllerValue == 0 ? "0" : bindings[0].ControllerValue.ToString().Substring(3, 3), ImageTool.Green, 40);
                        break;
                    case "COM1 Standby Int":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(bindings[9].MsfsValue));
                        bitmapBuilder.DrawText((bindings[12].ControllerValue == 0 ? "0" : bindings[12].ControllerValue.ToString().Substring(0, 3)) + ".", ImageTool.Yellow, 40);
                        break;
                    case "COM1 Standby Float":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(bindings[9].MsfsValue));
                        bitmapBuilder.DrawText(bindings[12].ControllerValue == 0 ? "0" : bindings[12].ControllerValue.ToString().Substring(3, 3), ImageTool.Yellow, 40);
                        break;
                    case "COM2 Active Int":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(bindings[10].MsfsValue));
                        bitmapBuilder.DrawText((bindings[1].ControllerValue == 0 ? "0" : bindings[1].ControllerValue.ToString().Substring(0, 3)) + ".", ImageTool.Green, 40);
                        break;
                    case "COM2 Active Float":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(bindings[10].MsfsValue));
                        bitmapBuilder.DrawText(bindings[1].ControllerValue == 0 ? "0" : bindings[1].ControllerValue.ToString().Substring(3, 3), ImageTool.Green, 40);
                        break;
                    case "COM2 Standby Int":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(bindings[10].MsfsValue));
                        bitmapBuilder.DrawText((bindings[13].ControllerValue == 0 ? "0" : bindings[13].ControllerValue.ToString().Substring(0, 3)) + ".", ImageTool.Yellow, 40);
                        break;
                    case "COM2 Standby Float":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(bindings[10].MsfsValue));
                        bitmapBuilder.DrawText(bindings[13].ControllerValue == 0 ? "0" : bindings[13].ControllerValue.ToString().Substring(3, 3), ImageTool.Yellow, 40);
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
                    bindings[15].SetControllerValue(1);
                    break;
                case "COM2 Active":
                case "COM2 Active Int":
                case "COM2 Active Float":
                case "COM2 Standby":
                case "COM2 Standby Int":
                case "COM2 Standby Float":
                case "COM2 Int Reset":
                case "COM2 Float Reset":
                    bindings[16].SetControllerValue(1);
                    break;
            }
        }

        public override void ApplyAdjustment(string actionParameter, int ticks)
        {
            switch (actionParameter)
            {
                case "COM1 Int Encoder":
                    bindings[12].SetControllerValue(adjuster.IncrIntValue(bindings[12].ControllerValue, ticks));
                    break;
                case "COM1 Float Encoder":
                    bindings[12].SetControllerValue(adjuster.IncrDecimalValue(bindings[12].ControllerValue, ticks));
                    break;
                case "COM2 Int Encoder":
                    bindings[13].SetControllerValue(adjuster.IncrIntValue(bindings[13].ControllerValue, ticks));
                    break;
                case "COM2 Float Encoder":
                    bindings[13].SetControllerValue(adjuster.IncrDecimalValue(bindings[13].ControllerValue, ticks));
                    break;

            }
            EncoderActionNamesChanged();
            ButtonActionNamesChanged();
        }

        public void Notify()
        {
            foreach (Binding binding in bindings)
            {
                if (binding.HasMSFSChanged())
                {
                    binding.Reset();
                }
            }
        }

        readonly List<Binding> bindings = new List<Binding>();
        readonly DecimalValueAdjuster adjuster = new DecimalValueAdjuster(118, 136, 0, 995, 5);
    }
}
