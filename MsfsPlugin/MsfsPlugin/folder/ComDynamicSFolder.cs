namespace Loupedeck.MsfsPlugin.folder
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Diagnostics;

    using Loupedeck.MsfsPlugin.tools;

    public class ComDynamicSFolder : PluginDynamicFolder, Notifiable
    {
        protected readonly List<Binding> _bindings = new List<Binding>();
        private Boolean isCom1active = true;
        public ComDynamicSFolder()
        {
            this.DisplayName = "COM (for S)";
            this.GroupName = "Folder";

            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.COM1_ACTIVE_FREQUENCY)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.COM2_ACTIVE_FREQUENCY)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.COM1_STBY)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.COM2_STBY)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.COM1_AVAILABLE)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.COM2_AVAILABLE)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.COM1_RADIO_SWAP)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.COM2_RADIO_SWAP)));
            MsfsData.Instance.Register(this);

        }
        public override PluginDynamicFolderNavigation GetNavigationArea(DeviceType _) => PluginDynamicFolderNavigation.EncoderArea;
        public override IEnumerable<String> GetButtonPressActionNames(DeviceType deviceType)
        {
            return new[]
            {
                this.CreateCommandName("COM1"),
                this.CreateCommandName("COM1 Active Int"),
                this.CreateCommandName("COM1 Active Float"),
                this.CreateCommandName("COM1 Standby Int"),
                this.CreateCommandName("COM1 Standby Float"),
                this.CreateCommandName("COM2"),
                this.CreateCommandName("COM2 Active Int"),
                this.CreateCommandName("COM2 Active Float"),
                this.CreateCommandName("COM2 Standby Int"),
                this.CreateCommandName("COM2 Standby Float"),
                PluginDynamicFolder.NavigateUpActionName,
            };
        }
        public override IEnumerable<String> GetEncoderRotateActionNames(DeviceType deviceType)
        {
            return new[]
            {
                this.CreateAdjustmentName ("Int"),
                this.CreateAdjustmentName ("Float"),
            };
        }

        public override IEnumerable<String> GetEncoderPressActionNames(DeviceType deviceType)
        {
            return new[]
            {
                this.CreateCommandName("Int Reset"),
                this.CreateCommandName("Float Reset"),
            };
        }
        public override String GetAdjustmentDisplayName(String actionParameter, PluginImageSize imageSize)
        {
   
            var bindingIndex = this.isCom1active ? 2 : 3;
            var ret = "";
            switch (actionParameter)
            {
                case "Int":
                    ret = "COM\n" + Math.Truncate(this._bindings[bindingIndex].ControllerValue / 1000000f) + ".";
                    break;
                case "Float":
                    var com1dbl = Math.Round(this._bindings[bindingIndex].ControllerValue / 1000000f - Math.Truncate(this._bindings[bindingIndex].ControllerValue / 1000000f), 3).ToString();
                    ret = "COM\n" + (com1dbl.Length > 2 ? com1dbl.Substring(2) : com1dbl).PadRight(3, '0');
                    break;
            }
            return ret;
        }
        public override BitmapImage GetCommandImage(String actionParameter, PluginImageSize imageSize)
        {
            var bitmapBuilder = new BitmapBuilder(imageSize);

            switch (actionParameter)
            {
                case "COM1":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(this._bindings[4].MsfsValue));
                    if (this.isCom1active)
                    {
                        bitmapBuilder.DrawText("=> COM1");
                    } else
                    {
                        bitmapBuilder.DrawText("COM1");
                    }
                    
                    break;
                case "COM2":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(this._bindings[5].MsfsValue));
                    if (!this.isCom1active)
                    {
                        bitmapBuilder.DrawText("=> COM2");
                    } else
                    {
                        bitmapBuilder.DrawText("COM2");
                    }
                    break;
                case "COM1 Active Int":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(this._bindings[4].MsfsValue));
                    bitmapBuilder.DrawText((this._bindings[0].ControllerValue == 0 ? "0" : this._bindings[0].ControllerValue.ToString().Substring(0, 3)) + ".", new BitmapColor(0, 255, 0), 40);
                    break;
                case "COM1 Active Float":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(this._bindings[4].MsfsValue));
                    bitmapBuilder.DrawText(this._bindings[0].ControllerValue == 0 ? "0" : this._bindings[0].ControllerValue.ToString().Substring(3, 3), new BitmapColor(0, 255, 0), 40);
                    break;
                case "COM1 Standby Int":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(this._bindings[4].MsfsValue));
                    bitmapBuilder.DrawText((this._bindings[2].ControllerValue == 0 ? "0" : this._bindings[2].ControllerValue.ToString().Substring(0, 3)) + ".", new BitmapColor(255, 255, 0), 40);
                    break;
                case "COM1 Standby Float":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(this._bindings[4].MsfsValue));
                    bitmapBuilder.DrawText(this._bindings[2].ControllerValue == 0 ? "0" : this._bindings[2].ControllerValue.ToString().Substring(3, 3), new BitmapColor(255, 255, 0), 40);
                    break;
                case "COM2 Active Int":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(this._bindings[5].MsfsValue));
                    bitmapBuilder.DrawText((this._bindings[1].ControllerValue == 0 ? "0" : this._bindings[1].ControllerValue.ToString().Substring(0, 3)) + ".", new BitmapColor(0, 255, 0), 40);
                    break;
                case "COM2 Active Float":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(this._bindings[5].MsfsValue));
                    bitmapBuilder.DrawText(this._bindings[1].ControllerValue == 0 ? "0" : this._bindings[1].ControllerValue.ToString().Substring(3, 3), new BitmapColor(0, 255, 0), 40);
                    break;
                case "COM2 Standby Int":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(this._bindings[5].MsfsValue));
                    bitmapBuilder.DrawText((this._bindings[3].ControllerValue == 0 ? "0" : this._bindings[3].ControllerValue.ToString().Substring(0, 3)) + ".", new BitmapColor(255, 255, 0), 40);
                    break;
                case "COM2 Standby Float":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(this._bindings[5].MsfsValue));
                    bitmapBuilder.DrawText(this._bindings[3].ControllerValue == 0 ? "0" : this._bindings[3].ControllerValue.ToString().Substring(3, 3), new BitmapColor(255, 255, 0), 40);
                    break;
            }
            return bitmapBuilder.ToImage();
        }

        public override void RunCommand(String actionParameter)
        {
            switch (actionParameter)
            {
                case "COM1":
                    this.isCom1active = true;
                    break;
                case "COM2":
                    this.isCom1active = false;
                    break;
                case "COM1 Active Int":
                case "COM1 Active Float":
                case "COM1 Standby Int":
                case "COM1 Standby Float":
                    this._bindings[6].SetControllerValue(1);
                    break;
                case "COM2 Active Int":
                case "COM2 Active Float":
                case "COM2 Standby Int":
                case "COM2 Standby Float":
                    this._bindings[7].SetControllerValue(1);
                    break;
                case "Int Reset":
                case "Float Reset":
                    if (this.isCom1active)
                    { 
                        this._bindings[6].SetControllerValue(1); 
                    } else
                    { 
                        this._bindings[7].SetControllerValue(1);
                    }
                    break;
            }
        }

        public override void ApplyAdjustment(String actionParameter, Int32 ticks)
        {
            var bindingIndex = this.isCom1active ? 2 : 3;
            Debug.WriteLine(actionParameter);
            switch (actionParameter)
            {
                case "Int":
                    var com1int = Int32.Parse(this._bindings[bindingIndex].ControllerValue.ToString().Substring(0, 3));
                    var com1dbl = Int32.Parse(this._bindings[bindingIndex].ControllerValue.ToString().Substring(3, 3));
                    var newInt = ConvertTool.ApplyAdjustment(com1int, ticks, 118, 136, 1, true);
                    this._bindings[bindingIndex].SetControllerValue(newInt * 1000000 + com1dbl * 1000);
                    break;
                case "Float":
                    var com1dbl1 = Int32.Parse(this._bindings[bindingIndex].ControllerValue.ToString().Substring(3, 3));
                    var com1int1 = Int32.Parse(this._bindings[bindingIndex].ControllerValue.ToString().Substring(0, 3));
                    var newFloat = ConvertTool.ApplyAdjustment(com1dbl1, ticks, 0, 995, 5, true);
                    this._bindings[bindingIndex].SetControllerValue(com1int1 * 1000000 + newFloat * 1000);
                    break;
            }
            this.EncoderActionNamesChanged();
            this.ButtonActionNamesChanged();
        }
        public void Notify()
        {
            foreach (Binding binding in this._bindings)
            {
                if (binding.HasMSFSChanged())
                {
                    binding.Reset();
                }
            }
        }
    }
}
