namespace Loupedeck.MsfsPlugin.folder
{
    using System;
    using System.Collections.Generic;

    using Loupedeck.MsfsPlugin.tools;

    public class NavDynamicSFolder : PluginDynamicFolder, INotifiable
    {
        protected readonly List<Binding> bindings = new List<Binding>();
        private bool isVar1Active = true;
        public NavDynamicSFolder()
        {
            DisplayName = "NAV (for S)";
            GroupName = "Folder";

            bindings.Add(MsfsData.Instance.Register(BindingKeys.NAV1_ACTIVE_FREQUENCY));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.NAV2_ACTIVE_FREQUENCY));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.NAV1_STBY_FREQUENCY));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.NAV2_STBY_FREQUENCY));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.NAV1_AVAILABLE));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.NAV2_AVAILABLE));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.NAV1_RADIO_SWAP));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.NAV2_RADIO_SWAP));

            MsfsData.Instance.Register(this);

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
            var bindingIndex = isVar1Active ? 2 : 3;
            var ret = "";
            switch (actionParameter)
            {
                case "Int":
                    ret = "NAV\n" + Math.Truncate(bindings[bindingIndex].ControllerValue / 1000000f) + ".";
                    break;
                case "Float":
                    var var1dbl = Math.Round(bindings[bindingIndex].ControllerValue / 1000000f - Math.Truncate(bindings[bindingIndex].ControllerValue / 1000000f), 3).ToString();
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
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(bindings[4].MsfsValue));
                        bitmapBuilder.DrawText(isVar1Active ? "=> NAV1" : "NAV1");
                        break;
                    case "NAV2":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(bindings[5].MsfsValue));
                        bitmapBuilder.DrawText(!isVar1Active ? "=> NAV2" : "NAV2");
                        break;
                    case "NAV1 Active Int":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(bindings[4].MsfsValue));
                        bitmapBuilder.DrawText((bindings[0].ControllerValue == 0 ? "0" : bindings[0].ControllerValue.ToString().Substring(0, 3)) + ".", ImageTool.Green, 40);
                        break;
                    case "NAV1 Active Float":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(bindings[4].MsfsValue));
                        bitmapBuilder.DrawText(bindings[0].ControllerValue == 0 ? "0" : bindings[0].ControllerValue.ToString().Substring(3, 2), ImageTool.Green, 40);
                        break;
                    case "NAV1 Standby Int":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(bindings[4].MsfsValue));
                        bitmapBuilder.DrawText((bindings[2].ControllerValue == 0 ? "0" : bindings[2].ControllerValue.ToString().Substring(0, 3)) + ".", ImageTool.Yellow, 40);
                        break;
                    case "NAV1 Standby Float":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(bindings[4].MsfsValue));
                        bitmapBuilder.DrawText(bindings[2].ControllerValue == 0 ? "0" : bindings[2].ControllerValue.ToString().Substring(3, 2), ImageTool.Yellow, 40);
                        break;
                    case "NAV2 Active Int":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(bindings[5].MsfsValue));
                        bitmapBuilder.DrawText((bindings[1].ControllerValue == 0 ? "0" : bindings[1].ControllerValue.ToString().Substring(0, 3)) + ".", ImageTool.Green, 40);
                        break;
                    case "NAV2 Active Float":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(bindings[5].MsfsValue));
                        bitmapBuilder.DrawText(bindings[1].ControllerValue == 0 ? "0" : bindings[1].ControllerValue.ToString().Substring(3, 2), ImageTool.Green, 40);
                        break;
                    case "NAV2 Standby Int":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(bindings[5].MsfsValue));
                        bitmapBuilder.DrawText((bindings[3].ControllerValue == 0 ? "0" : bindings[3].ControllerValue.ToString().Substring(0, 3)) + ".", ImageTool.Yellow, 40);
                        break;
                    case "NAV2 Standby Float":
                        bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(bindings[5].MsfsValue));
                        bitmapBuilder.DrawText(bindings[3].ControllerValue == 0 ? "0" : bindings[3].ControllerValue.ToString().Substring(3, 2), ImageTool.Yellow, 40);
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
                    bindings[6].SetControllerValue(1);
                    break;
                case "NAV2 Active":
                case "NAV2 Active Int":
                case "NAV2 Active Float":
                case "NAV2 Standby":
                case "NAV2 Standby Int":
                case "NAV2 Standby Float":
                    bindings[7].SetControllerValue(1);
                    break;
                case "Int Reset":
                case "Float Reset":
                    if (isVar1Active)
                    {
                        bindings[6].SetControllerValue(1);
                    }
                    else
                    {
                        bindings[7].SetControllerValue(1);
                    }
                    break;

            }
        }

        public override void ApplyAdjustment(string actionParameter, int ticks)
        {
            var bindingIndex = isVar1Active ? 2 : 3;
            switch (actionParameter)
            {
                case "Int":
                    var com1int = int.Parse(bindings[bindingIndex].ControllerValue.ToString().Substring(0, 3));
                    var com1dbl = int.Parse(bindings[bindingIndex].ControllerValue.ToString().Substring(3, 2));
                    var newInt = ConvertTool.ApplyAdjustment(com1int, ticks, 108, 117, 1, true);
                    bindings[bindingIndex].SetControllerValue(newInt * 1000000 + com1dbl * 10000);
                    break;
                case "Float":
                    var com1dbl1 = int.Parse(bindings[bindingIndex].ControllerValue.ToString().Substring(3, 2));
                    var com1int1 = int.Parse(bindings[bindingIndex].ControllerValue.ToString().Substring(0, 3));
                    var newFloat = ConvertTool.ApplyAdjustment(com1dbl1, ticks, 0, 99, 1, true);
                    bindings[bindingIndex].SetControllerValue(com1int1 * 1000000 + newFloat * 10000);
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
    }
}
