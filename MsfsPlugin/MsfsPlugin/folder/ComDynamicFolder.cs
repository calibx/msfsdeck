namespace Loupedeck.MsfsPlugin.folder
{
    using System;
    using System.Collections.Generic;

    public class ComDynamicFolder : PluginDynamicFolder, Notifiable
    {
        protected readonly String _imageDisableResourcePath = "Loupedeck.MsfsPlugin.Resources.disableBorder.png";
        protected readonly String _imageAvailableResourcePath = "Loupedeck.MsfsPlugin.Resources.onBorder.png";
        protected readonly List<Binding> _bindings = new List<Binding>();
        public ComDynamicFolder()
        {
            this.DisplayName = "Com";
            this.GroupName = "Folder";
            this.Navigation = PluginDynamicFolderNavigation.None;

            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.COM1_ACTIVE_FREQUENCY)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.COM2_ACTIVE_FREQUENCY)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.COM3_ACTIVE_FREQUENCY)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.COM1_ACTIVE_FREQUENCY_TYPE)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.COM2_ACTIVE_FREQUENCY_TYPE)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.COM3_ACTIVE_FREQUENCY_TYPE)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.COM1_STATUS)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.COM2_STATUS)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.COM3_STATUS)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.COM1_AVAILABLE)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.COM2_AVAILABLE)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.COM3_AVAILABLE)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.COM1_STBY)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.COM2_STBY)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.COM3_STBY)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.COM1_RADIO_SWAP)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.COM2_RADIO_SWAP)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.COM3_RADIO_SWAP)));

            MsfsData.Instance.Register(this);

        }
        public override IEnumerable<String> GetButtonPressActionNames()
        {
            return new[]
            {
                PluginDynamicFolder.NavigateUpActionName,
                this.CreateCommandName("COM1 Active"),
                this.CreateCommandName("COM1 Standby"),
                this.CreateCommandName("COM1 Status"),
/*                this.CreateCommandName("COM2 Active"),
                this.CreateCommandName("COM2 Standby"),
                this.CreateCommandName("COM3 Active"),
                this.CreateCommandName("COM3 Standby"),*/
            };
        }
        public override IEnumerable<String> GetEncoderRotateActionNames()
        {
            return new[]
            {
                this.CreateAdjustmentName ("COM1 Int Encoder"),
                this.CreateAdjustmentName ("COM2 Int Encoder"),
                this.CreateAdjustmentName ("COM3 Int Encoder"),
                this.CreateAdjustmentName ("COM1 Float Encoder"),
                this.CreateAdjustmentName ("COM2 Float Encoder"),
                this.CreateAdjustmentName ("COM3 Float Encoder"),
            };
        }

        public override IEnumerable<String> GetEncoderPressActionNames()
        {
            return new[]
            {
                this.CreateCommandName("COM1 Int Reset"),
                this.CreateCommandName("COM2 Int Reset"),
                this.CreateCommandName("COM3 Int Reset"),
                this.CreateCommandName("COM1 Float Reset"),
                this.CreateCommandName("COM2 Float Reset"),
                this.CreateCommandName("COM3 Float Reset"),
            };
        }
        public override String GetAdjustmentDisplayName(String actionParameter, PluginImageSize imageSize)
        {
            var ret = "";
            switch (actionParameter)
            {
                case "COM1 Int Encoder":
                    ret = "COM1\n" + Math.Truncate(this.bcd2dbl(this._bindings[0].ControllerValue)) + ".";
                    break;
                case "COM1 Float Encoder":
                    ret = "COM1\n" + Math.Round((this.bcd2dbl(this._bindings[0].ControllerValue) - Math.Truncate(this.bcd2dbl(this._bindings[0].ControllerValue))), 3);
                    break;
            }
            return ret;
        }
        public override BitmapImage GetCommandImage(String actionParameter, PluginImageSize imageSize)
        {
            var bitmapBuilder = new BitmapBuilder(imageSize);
            switch (actionParameter)
            {
                case "COM1 Active":
                    if (this._bindings[9].MsfsValue == 1)
                    {
                        bitmapBuilder.SetBackgroundImage(EmbeddedResources.ReadImage(this._imageAvailableResourcePath));
                    }
                    else
                    {
                        bitmapBuilder.SetBackgroundImage(EmbeddedResources.ReadImage(this._imageDisableResourcePath));
                    }
                    bitmapBuilder.DrawText("COM 1\n" + this.bcd2dbl(this._bindings[0].ControllerValue));
                    break;
                case "COM1 Standby":
                    bitmapBuilder.DrawText("COM 1 STB\n" + this.bcd2dbl(this._bindings[12].ControllerValue));
                    break;
                case "COM1 Status":
                    bitmapBuilder.DrawText("COM 1 Status\n" + this._bindings[6].ControllerValue + "\nType\n" + this._bindings[3].ControllerValue);
                    break;
            }
            return bitmapBuilder.ToImage();
        }

        public override void RunCommand(String actionParameter)
        {
            switch (actionParameter)
            {
                case "COM1 Active":
                    break;
                case "COM1 Standby":
                    break;
                case "COM1 Int Reset":
                case "COM1 Float Reset":
                    this._bindings[16].SetControllerValue(1);
                    break;
            }
        }

        public override void ApplyAdjustment(String actionParameter, Int32 ticks)
        {
            switch (actionParameter)
            {
                case "COM1 Int Encoder":
                    this._bindings[0].SetControllerValue(this.ApplyAdjustment(this._bindings[0].ControllerValue, -0, 999, 1, ticks));
                    break;
                case "COM1 Float Encoder":
                    this._bindings[2].SetControllerValue(this.ApplyAdjustment(this._bindings[2].ControllerValue, 0, 999, 1, ticks));
                    break;
            }
            this.EncoderActionNamesChanged();
        }
        public void Notify()
        {
            foreach (Binding binding in this._bindings)
            {
                if (binding.HasMSFSChanged())
                {
                    MsfsData.Instance.refreshLimiter++;
                    binding.Reset();
                    switch (binding.Key)
                    {
                        case BindingKeys.COM1_ACTIVE_FREQUENCY:
                            this.AdjustmentValueChanged("COM1 Int Encoder");
                            this.AdjustmentValueChanged("COM1 Float Encoder");
                            break;
                        default:
                            break;
                    }
                    this.ButtonActionNamesChanged();
                }
            }
        }
        private Int64 ApplyAdjustment(Int64 value, Int32 min, Int32 max, Int32 steps, Int32 ticks)
        {
            value += ticks * steps;
            if (value < min)
            { value = min; }
            else if (value > max)
            { value = max; }
            return value;
        }

        private Double bcd2dbl(Int64 bcd)

        {
            string bcdstr = "1" + (bcd).ToString("X4");
            int bcdint = Convert.ToInt32(bcdstr);
            double freq = (double)bcdint / 100;
            return freq;

        }
    }

}
