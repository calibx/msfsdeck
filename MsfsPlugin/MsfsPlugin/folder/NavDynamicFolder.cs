namespace Loupedeck.MsfsPlugin.folder
{
    using System;
    using System.Collections.Generic;

    using Loupedeck.MsfsPlugin.tools;

    public class NavDynamicFolder : PluginDynamicFolder, INotifiable
    {
        public NavDynamicFolder()
        {
            DisplayName = "NAV";
            GroupName = "Folder";

            bindings.Add(MsfsData.Instance.Register(BindingKeys.NAV1_ACTIVE_FREQUENCY));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.NAV2_ACTIVE_FREQUENCY));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.NAV1_AVAILABLE));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.NAV2_AVAILABLE));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.NAV1_STBY_FREQUENCY));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.NAV2_STBY_FREQUENCY));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.NAV1_RADIO_SWAP));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.NAV2_RADIO_SWAP));

            MsfsData.Instance.Register(this);
        }

        public override PluginDynamicFolderNavigation GetNavigationArea(DeviceType _) => PluginDynamicFolderNavigation.None;

        public override IEnumerable<string> GetButtonPressActionNames(DeviceType deviceType)
        {
            return new[]
            {
                CreateCommandName("NAV1 Active Int"),
                CreateCommandName("NAV1 Active Float"),
                CreateCommandName("NAV1 Standby Int"),
                CreateCommandName("NAV1 Standby Float"),
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
                CreateAdjustmentName ("NAV1 Int Encoder"),
                CreateAdjustmentName ("NAV2 Int Encoder"),
                CreateAdjustmentName (""),
                CreateAdjustmentName ("NAV1 Float Encoder"),
                CreateAdjustmentName ("NAV2 Float Encoder"),
            };
        }

        public override IEnumerable<string> GetEncoderPressActionNames(DeviceType deviceType)
        {
            return new[]
            {
                CreateCommandName("NAV1 Int Reset"),
                CreateCommandName("NAV2 Int Reset"),
                CreateCommandName (""),
                CreateCommandName("NAV1 Float Reset"),
                CreateCommandName("NAV2 Float Reset"),
            };
        }

        public override string GetAdjustmentDisplayName(string actionParameter, PluginImageSize imageSize)
        {
            var ret = "";
            switch (actionParameter)
            {
                case "NAV1 Int Encoder":
                    ret = "NAV1\n" + Math.Truncate(bindings[4].ControllerValue / 1000000f) + ".";
                    break;
                case "NAV1 Float Encoder":
                    var com1dbl = Math.Round(bindings[4].ControllerValue / 1000000f - Math.Truncate(bindings[4].ControllerValue / 1000000f), 3).ToString();
                    ret = "NAV1\n" + (com1dbl.Length > 2 ? com1dbl.Substring(2) : com1dbl).PadRight(2, '0');
                    break;
                case "NAV2 Int Encoder":
                    ret = "NAV2\n" + Math.Truncate(bindings[5].ControllerValue / 1000000f) + ".";
                    break;
                case "NAV2 Float Encoder":
                    var com2dbl = Math.Round(bindings[5].ControllerValue / 1000000f - Math.Truncate(bindings[5].ControllerValue / 1000000f), 3).ToString();
                    ret = "NAV2\n" + (com2dbl.Length > 2 ? com2dbl.Substring(2) : com2dbl).PadRight(2, '0');
                    break;
            }
            return ret;
        }

        public override BitmapImage GetCommandImage(string actionParameter, PluginImageSize imageSize)
        {
            var bitmapBuilder = new BitmapBuilder(imageSize);
            switch (actionParameter)
            {
                case "NAV1 Active Int":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(bindings[2].MsfsValue));
                    bitmapBuilder.DrawText((bindings[0].ControllerValue == 0 ? "0" : bindings[0].ControllerValue.ToString().Substring(0, 3)) + ".", new BitmapColor(0, 255, 0), 40);
                    break;
                case "NAV1 Active Float":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(bindings[2].MsfsValue));
                    bitmapBuilder.DrawText(bindings[0].ControllerValue == 0 ? "0" : bindings[0].ControllerValue.ToString().Substring(3, 2), new BitmapColor(0, 255, 0), 40);
                    break;
                case "NAV1 Standby Int":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(bindings[2].MsfsValue));
                    bitmapBuilder.DrawText((bindings[4].ControllerValue == 0 ? "0" : bindings[4].ControllerValue.ToString().Substring(0, 3)) + ".", new BitmapColor(255, 255, 0), 40);
                    break;
                case "NAV1 Standby Float":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(bindings[2].MsfsValue));
                    bitmapBuilder.DrawText(bindings[4].ControllerValue == 0 ? "0" : bindings[4].ControllerValue.ToString().Substring(3, 2), new BitmapColor(255, 255, 0), 40);
                    break;
                case "NAV2 Active Int":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(bindings[3].MsfsValue));
                    bitmapBuilder.DrawText((bindings[1].ControllerValue == 0 ? "0" : bindings[1].ControllerValue.ToString().Substring(0, 3)) + ".", new BitmapColor(0, 255, 0), 40);
                    break;
                case "NAV2 Active Float":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(bindings[3].MsfsValue));
                    bitmapBuilder.DrawText(bindings[1].ControllerValue == 0 ? "0" : bindings[1].ControllerValue.ToString().Substring(3, 2), new BitmapColor(0, 255, 0), 40);
                    break;
                case "NAV2 Standby Int":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(bindings[3].MsfsValue));
                    bitmapBuilder.DrawText((bindings[5].ControllerValue == 0 ? "0" : bindings[5].ControllerValue.ToString().Substring(0, 3)) + ".", new BitmapColor(255, 255, 0), 40);
                    break;
                case "NAV2 Standby Float":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(bindings[3].MsfsValue));
                    bitmapBuilder.DrawText(bindings[5].ControllerValue == 0 ? "0" : bindings[5].ControllerValue.ToString().Substring(3, 2), new BitmapColor(255, 255, 0), 40);
                    break;

            }
            return bitmapBuilder.ToImage();
        }

        public override void RunCommand(string actionParameter)
        {
            switch (actionParameter)
            {
                case "NAV1 Active":
                case "NAV1 Active Int":
                case "NAV1 Active Float":
                case "NAV1 Standby":
                case "NAV1 Standby Int":
                case "NAV1 Standby Float":
                case "NAV1 Int Reset":
                case "NAV1 Float Reset":
                    bindings[6].SetControllerValue(1);
                    break;
                case "NAV2 Active":
                case "NAV2 Active Int":
                case "NAV2 Active Float":
                case "NAV2 Standby":
                case "NAV2 Standby Int":
                case "NAV2 Standby Float":
                case "NAV2 Int Reset":
                case "NAV2 Float Reset":
                    bindings[7].SetControllerValue(1);
                    break;
            }
        }

        public override void ApplyAdjustment(string actionParameter, int ticks)
        {
            switch (actionParameter)
            {
                case "NAV1 Int Encoder":
                    bindings[4].SetControllerValue(adjuster.IncrIntValue(bindings[4].ControllerValue, ticks));
                    break;
                case "NAV1 Float Encoder":
                    bindings[4].SetControllerValue(adjuster.IncrDecimalValue(bindings[4].ControllerValue, ticks));
                    break;
                case "NAV2 Int Encoder":
                    bindings[5].SetControllerValue(adjuster.IncrIntValue(bindings[5].ControllerValue, ticks));
                    break;
                case "NAV2 Float Encoder":
                    bindings[5].SetControllerValue(adjuster.IncrDecimalValue(bindings[5].ControllerValue, ticks));
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
        readonly DecimalValueAdjuster adjuster = new DecimalValueAdjuster(108, 117, 0, 95, 5);
    }
}
