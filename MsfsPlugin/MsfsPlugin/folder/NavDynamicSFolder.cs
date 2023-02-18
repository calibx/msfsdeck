namespace Loupedeck.MsfsPlugin.folder
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    using Loupedeck.MsfsPlugin.tools;

    public class NavDynamicSFolder : PluginDynamicFolder, Notifiable
    {
        protected readonly List<Binding> _bindings = new List<Binding>();
        private Boolean isVar1Active = true;
        public NavDynamicSFolder()
        {
            this.DisplayName = "NAV (for S)";
            this.GroupName = "Folder";

            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.NAV1_ACTIVE_FREQUENCY)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.NAV2_ACTIVE_FREQUENCY)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.NAV1_STBY_FREQUENCY)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.NAV2_STBY_FREQUENCY)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.NAV1_AVAILABLE)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.NAV2_AVAILABLE)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.NAV1_RADIO_SWAP)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.NAV2_RADIO_SWAP)));

            MsfsData.Instance.Register(this);

        }

        public override PluginDynamicFolderNavigation GetNavigationArea(DeviceType _) => PluginDynamicFolderNavigation.EncoderArea;

        public override IEnumerable<String> GetButtonPressActionNames(DeviceType deviceType)
        {
            return new[]
            {
                this.CreateCommandName("NAV1"),
                this.CreateCommandName("NAV1 Active Int"),
                this.CreateCommandName("NAV1 Active Float"),
                this.CreateCommandName("NAV1 Standby Int"),
                this.CreateCommandName("NAV1 Standby Float"),
                this.CreateCommandName("NAV2"),
                this.CreateCommandName("NAV2 Active Int"),
                this.CreateCommandName("NAV2 Active Float"),
                this.CreateCommandName("NAV2 Standby Int"),
                this.CreateCommandName("NAV2 Standby Float"),
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
            var bindingIndex = this.isVar1Active ? 2 : 3;
            var ret = "";
            switch (actionParameter)
            {
                case "Int":
                    ret = "NAV\n" + Math.Truncate(this._bindings[bindingIndex].ControllerValue / 1000000f) + ".";
                    break;
                case "Float":
                    var var1dbl = Math.Round(this._bindings[bindingIndex].ControllerValue / 1000000f - Math.Truncate(this._bindings[bindingIndex].ControllerValue / 1000000f), 3).ToString();
                    ret = "NAV\n" + (var1dbl.Length > 2 ? var1dbl.Substring(2) : var1dbl).PadRight(2, '0');
                    break;
            }
            return ret;
        }
        public override BitmapImage GetCommandImage(String actionParameter, PluginImageSize imageSize)
        {
            var bitmapBuilder = new BitmapBuilder(imageSize);
            switch (actionParameter)
            {
                case "NAV1":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(this._bindings[4].MsfsValue));
                    if (this.isVar1Active)
                    {
                        bitmapBuilder.DrawText("=> NAV1");
                    }
                    else
                    {
                        bitmapBuilder.DrawText("NAV1");
                    }

                    break;
                case "NAV2":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(this._bindings[5].MsfsValue));
                    if (!this.isVar1Active)
                    {
                        bitmapBuilder.DrawText("=> NAV2");
                    }
                    else
                    {
                        bitmapBuilder.DrawText("NAV2");
                    }
                    break;
                case "NAV1 Active Int":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(this._bindings[4].MsfsValue));
                    bitmapBuilder.DrawText((this._bindings[0].ControllerValue == 0 ? "0" : this._bindings[0].ControllerValue.ToString().Substring(0, 3)) + ".", new BitmapColor(0, 255, 0), 40);
                    break;
                case "NAV1 Active Float":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(this._bindings[4].MsfsValue));
                    bitmapBuilder.DrawText(this._bindings[0].ControllerValue == 0 ? "0" : this._bindings[0].ControllerValue.ToString().Substring(3, 2), new BitmapColor(0, 255, 0), 40);
                    break;
                case "NAV1 Standby Int":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(this._bindings[4].MsfsValue));
                    bitmapBuilder.DrawText((this._bindings[2].ControllerValue == 0 ? "0" : this._bindings[2].ControllerValue.ToString().Substring(0, 3)) + ".", new BitmapColor(255, 255, 0), 40);
                    break;
                case "NAV1 Standby Float":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(this._bindings[4].MsfsValue));
                    bitmapBuilder.DrawText(this._bindings[2].ControllerValue == 0 ? "0" : this._bindings[2].ControllerValue.ToString().Substring(3, 2), new BitmapColor(255, 255, 0), 40);
                    break;
                case "NAV2 Active Int":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(this._bindings[5].MsfsValue));
                    bitmapBuilder.DrawText((this._bindings[1].ControllerValue == 0 ? "0" : this._bindings[1].ControllerValue.ToString().Substring(0, 3)) + ".", new BitmapColor(0, 255, 0), 40);
                    break;
                case "NAV2 Active Float":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(this._bindings[5].MsfsValue));
                    bitmapBuilder.DrawText(this._bindings[1].ControllerValue == 0 ? "0" : this._bindings[1].ControllerValue.ToString().Substring(3, 2), new BitmapColor(0, 255, 0), 40);
                    break;
                case "NAV2 Standby Int":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(this._bindings[5].MsfsValue));
                    bitmapBuilder.DrawText((this._bindings[3].ControllerValue == 0 ? "0" : this._bindings[3].ControllerValue.ToString().Substring(0, 3)) + ".", new BitmapColor(255, 255, 0), 40);
                    break;
                case "NAV2 Standby Float":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(this._bindings[5].MsfsValue));
                    bitmapBuilder.DrawText(this._bindings[3].ControllerValue == 0 ? "0" : this._bindings[3].ControllerValue.ToString().Substring(3, 2), new BitmapColor(255, 255, 0), 40);
                    break;

            }
            return bitmapBuilder.ToImage();
        }

        public override void RunCommand(String actionParameter)
        {
            switch (actionParameter)
            {
                case "NAV1":
                    this.isVar1Active = true;
                    break;
                case "NAV2":
                    this.isVar1Active = false;
                    break;
                case "NAV1 Active":
                case "NAV1 Active Int":
                case "NAV1 Active Float":
                case "NAV1 Standby":
                case "NAV1 Standby Int":
                case "NAV1 Standby Float":
                    this._bindings[6].SetControllerValue(1);
                    break;
                case "NAV2 Active":
                case "NAV2 Active Int":
                case "NAV2 Active Float":
                case "NAV2 Standby":
                case "NAV2 Standby Int":
                case "NAV2 Standby Float":
                    this._bindings[7].SetControllerValue(1);
                    break;
                case "Int Reset":
                case "Float Reset":
                    if (this.isVar1Active)
                    {
                        this._bindings[6].SetControllerValue(1);
                    }
                    else
                    {
                        this._bindings[7].SetControllerValue(1);
                    }
                    break;

            }
        }

        public override void ApplyAdjustment(String actionParameter, Int32 ticks)
        {
            var bindingIndex = this.isVar1Active ? 2 : 3;
            switch (actionParameter)
            {
                case "Int":
                    var com1int = Int32.Parse(this._bindings[bindingIndex].ControllerValue.ToString().Substring(0, 3));
                    var com1dbl = Int32.Parse(this._bindings[bindingIndex].ControllerValue.ToString().Substring(3, 2));
                    var newInt = ConvertTool.ApplyAdjustment(com1int, ticks, 108, 117, 1, true);
                    this._bindings[bindingIndex].SetControllerValue(newInt * 1000000 + com1dbl * 10000);
                    break;
                case "Float":
                    var com1dbl1 = Int32.Parse(this._bindings[bindingIndex].ControllerValue.ToString().Substring(3, 2));
                    var com1int1 = Int32.Parse(this._bindings[bindingIndex].ControllerValue.ToString().Substring(0, 3));
                    var newFloat = ConvertTool.ApplyAdjustment(com1dbl1, ticks, 0, 99, 1, true);
                    this._bindings[bindingIndex].SetControllerValue(com1int1 * 1000000 + newFloat * 10000);
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
