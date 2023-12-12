namespace Loupedeck.MsfsPlugin.folder
{
    using System;
    using System.Collections.Generic;

    using Loupedeck.MsfsPlugin.tools;

    public class ComDynamicSFolder : PluginDynamicFolder, INotifiable
    {
        protected readonly List<Binding> bindings = new List<Binding>();
        private bool isCom1active = true;
        public ComDynamicSFolder()
        {
            DisplayName = "COM (for S)";
            GroupName = "Folder";

            bindings.Add(MsfsData.Instance.Register(BindingKeys.COM1_ACTIVE_FREQUENCY));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.COM2_ACTIVE_FREQUENCY));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.COM1_STBY));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.COM2_STBY));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.COM1_AVAILABLE));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.COM2_AVAILABLE));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.COM1_RADIO_SWAP));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.COM2_RADIO_SWAP));
            MsfsData.Instance.Register(this);

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
   
            var bindingIndex = isCom1active ? 2 : 3;
            var ret = "";
            switch (actionParameter)
            {
                case "Int":
                    ret = "COM\n" + Math.Truncate(bindings[bindingIndex].ControllerValue / 1000000f) + ".";
                    break;
                case "Float":
                    var com1dbl = Math.Round(bindings[bindingIndex].ControllerValue / 1000000f - Math.Truncate(bindings[bindingIndex].ControllerValue / 1000000f), 3).ToString();
                    ret = "COM\n" + (com1dbl.Length > 2 ? com1dbl.Substring(2) : com1dbl).PadRight(3, '0');
                    break;
            }
            return ret;
        }
        public override BitmapImage GetCommandImage(string actionParameter, PluginImageSize imageSize)
        {
            var bitmapBuilder = new BitmapBuilder(imageSize);

            switch (actionParameter)
            {
                case "COM1":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(bindings[4].MsfsValue));
                    if (isCom1active)
                    {
                        bitmapBuilder.DrawText("=> COM1");
                    } else
                    {
                        bitmapBuilder.DrawText("COM1");
                    }
                    
                    break;
                case "COM2":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(bindings[5].MsfsValue));
                    if (!isCom1active)
                    {
                        bitmapBuilder.DrawText("=> COM2");
                    } else
                    {
                        bitmapBuilder.DrawText("COM2");
                    }
                    break;
                case "COM1 Active Int":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(bindings[4].MsfsValue));
                    bitmapBuilder.DrawText((bindings[0].ControllerValue == 0 ? "0" : bindings[0].ControllerValue.ToString().Substring(0, 3)) + ".", new BitmapColor(0, 255, 0), 40);
                    break;
                case "COM1 Active Float":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(bindings[4].MsfsValue));
                    bitmapBuilder.DrawText(bindings[0].ControllerValue == 0 ? "0" : bindings[0].ControllerValue.ToString().Substring(3, 3), new BitmapColor(0, 255, 0), 40);
                    break;
                case "COM1 Standby Int":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(bindings[4].MsfsValue));
                    bitmapBuilder.DrawText((bindings[2].ControllerValue == 0 ? "0" : bindings[2].ControllerValue.ToString().Substring(0, 3)) + ".", new BitmapColor(255, 255, 0), 40);
                    break;
                case "COM1 Standby Float":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(bindings[4].MsfsValue));
                    bitmapBuilder.DrawText(bindings[2].ControllerValue == 0 ? "0" : bindings[2].ControllerValue.ToString().Substring(3, 3), new BitmapColor(255, 255, 0), 40);
                    break;
                case "COM2 Active Int":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(bindings[5].MsfsValue));
                    bitmapBuilder.DrawText((bindings[1].ControllerValue == 0 ? "0" : bindings[1].ControllerValue.ToString().Substring(0, 3)) + ".", new BitmapColor(0, 255, 0), 40);
                    break;
                case "COM2 Active Float":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(bindings[5].MsfsValue));
                    bitmapBuilder.DrawText(bindings[1].ControllerValue == 0 ? "0" : bindings[1].ControllerValue.ToString().Substring(3, 3), new BitmapColor(0, 255, 0), 40);
                    break;
                case "COM2 Standby Int":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(bindings[5].MsfsValue));
                    bitmapBuilder.DrawText((bindings[3].ControllerValue == 0 ? "0" : bindings[3].ControllerValue.ToString().Substring(0, 3)) + ".", new BitmapColor(255, 255, 0), 40);
                    break;
                case "COM2 Standby Float":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(bindings[5].MsfsValue));
                    bitmapBuilder.DrawText(bindings[3].ControllerValue == 0 ? "0" : bindings[3].ControllerValue.ToString().Substring(3, 3), new BitmapColor(255, 255, 0), 40);
                    break;
            }
            return bitmapBuilder.ToImage();
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
                    bindings[6].SetControllerValue(1);
                    break;
                case "COM2 Active Int":
                case "COM2 Active Float":
                case "COM2 Standby Int":
                case "COM2 Standby Float":
                    bindings[7].SetControllerValue(1);
                    break;
                case "Int Reset":
                case "Float Reset":
                    if (isCom1active)
                        bindings[6].SetControllerValue(1);
                    else
                        bindings[7].SetControllerValue(1);
                    break;
            }
        }

        public override void ApplyAdjustment(string actionParameter, int ticks)
        {
            var bindingIndex = isCom1active ? 2 : 3;
            switch (actionParameter)
            {
                case "Int":
                    var com1int = int.Parse(bindings[bindingIndex].ControllerValue.ToString().Substring(0, 3));
                    var com1dbl = int.Parse(bindings[bindingIndex].ControllerValue.ToString().Substring(3, 3));
                    var newInt = ConvertTool.ApplyAdjustment(com1int, ticks, 118, 136, 1, true);
                    bindings[bindingIndex].SetControllerValue(newInt * 1000000 + com1dbl * 1000);
                    break;
                case "Float":
                    var com1dbl1 = int.Parse(bindings[bindingIndex].ControllerValue.ToString().Substring(3, 3));
                    var com1int1 = int.Parse(bindings[bindingIndex].ControllerValue.ToString().Substring(0, 3));
                    var newFloat = ConvertTool.ApplyAdjustment(com1dbl1, ticks, 0, 995, 5, true);
                    bindings[bindingIndex].SetControllerValue(com1int1 * 1000000 + newFloat * 1000);
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
