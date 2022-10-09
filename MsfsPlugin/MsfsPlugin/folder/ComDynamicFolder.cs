namespace Loupedeck.MsfsPlugin.folder
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;

    public class ComDynamicFolder : PluginDynamicFolder, Notifiable
    {
        protected readonly String _imageDisableResourcePath = "Loupedeck.MsfsPlugin.Resources.disableBorder.png";
        protected readonly String _imageAvailableResourcePath = "Loupedeck.MsfsPlugin.Resources.onBorder.png";
        protected readonly List<Binding> _bindings = new List<Binding>();
        public ComDynamicFolder()
        {
            this.DisplayName = "COM";
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
                this.CreateCommandName("COM1 Active Int"),
                this.CreateCommandName("COM1 Active Float"),
                this.CreateCommandName("COM1 Standby Int"),
                this.CreateCommandName("COM1 Standby Float"),
                this.CreateCommandName("COM2 Active Int"),
                this.CreateCommandName("COM2 Active Float"),
                this.CreateCommandName("COM2 Standby Int"),
                this.CreateCommandName("COM2 Standby Float"),
                PluginDynamicFolder.NavigateUpActionName,
            };
        }
        public override IEnumerable<String> GetEncoderRotateActionNames()
        {
            return new[]
            {
                this.CreateAdjustmentName ("COM1 Int Encoder"),
                this.CreateAdjustmentName ("COM2 Int Encoder"),
                this.CreateAdjustmentName (""),
                this.CreateAdjustmentName ("COM1 Float Encoder"),
                this.CreateAdjustmentName ("COM2 Float Encoder"),
            };
        }

        public override IEnumerable<String> GetEncoderPressActionNames()
        {
            return new[]
            {
                this.CreateCommandName("COM1 Int Reset"),
                this.CreateCommandName("COM2 Int Reset"),
                //this.CreateCommandName("COM3 Int Reset"),
                this.CreateCommandName("COM1 Float Reset"),
                this.CreateCommandName("COM2 Float Reset"),
                //this.CreateCommandName("COM3 Float Reset"),
            };
        }
        public override String GetAdjustmentDisplayName(String actionParameter, PluginImageSize imageSize)
        {
            var ret = "";
            switch (actionParameter)
            {
                case "COM1 Int Encoder":
                    ret = "COM1\n" + Math.Truncate(this._bindings[12].ControllerValue / 1000000f) + ".";
                    break;
                case "COM1 Float Encoder":
                    var com1dbl = Math.Round(this._bindings[12].ControllerValue / 1000000f - Math.Truncate(this._bindings[12].ControllerValue / 1000000f), 3).ToString();
                    ret = "COM1\n" + (com1dbl.Length > 2 ? com1dbl.Substring(2) : com1dbl).PadRight(3, '0');
                    break;
                case "COM2 Int Encoder":
                    ret = "COM2\n" + Math.Truncate(this._bindings[13].ControllerValue / 1000000f) + ".";
                    break;
                case "COM2 Float Encoder":
                    var com2dbl = Math.Round(this._bindings[13].ControllerValue / 1000000f - Math.Truncate(this._bindings[13].ControllerValue / 1000000f), 3).ToString();
                    ret = "COM2\n" + (com2dbl.Length > 2 ? com2dbl.Substring(2) : com2dbl).PadRight(3, '0');
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
                    this.SetBackground(bitmapBuilder, this._bindings[9]);
                    bitmapBuilder.DrawText("COM 1\n" + (this._bindings[0].ControllerValue / 1000000f).ToString("F3", CultureInfo.InvariantCulture));
                    break;
                case "COM1 Standby":
                    this.SetBackground(bitmapBuilder, this._bindings[9]);
                    bitmapBuilder.DrawText("Standby\n" + (this._bindings[12].ControllerValue / 1000000f).ToString("F3", CultureInfo.InvariantCulture));
                    break;
                case "COM1 Status":
                    this.SetBackground(bitmapBuilder, this._bindings[9]);
                    bitmapBuilder.DrawText("Status\n" + this.IntToCOMStatus(this._bindings[6].ControllerValue) + "\nType\n" + this.IntToCOMType(this._bindings[3].ControllerValue));
                    break;
                case "COM1 Active Int":
                    this.SetBackground(bitmapBuilder, this._bindings[9]);
                    bitmapBuilder.DrawText((this._bindings[0].ControllerValue == 0 ? "0" : this._bindings[0].ControllerValue.ToString().Substring(0,3)) +".", new BitmapColor(0, 255, 0), 40);
                    break;
                case "COM1 Active Float":
                    this.SetBackground(bitmapBuilder, this._bindings[9]);
                    bitmapBuilder.DrawText(this._bindings[0].ControllerValue == 0 ? "0" : this._bindings[0].ControllerValue.ToString().Substring(3, 3), new BitmapColor(0, 255, 0), 40);
                    break;
                case "COM1 Standby Int":
                    this.SetBackground(bitmapBuilder, this._bindings[9]);
                    bitmapBuilder.DrawText((this._bindings[12].ControllerValue == 0 ? "0" : this._bindings[12].ControllerValue.ToString().Substring(0, 3)) + ".", new BitmapColor(255, 255, 0), 40);
                    break;
                case "COM1 Standby Float":
                    this.SetBackground(bitmapBuilder, this._bindings[9]);
                    bitmapBuilder.DrawText(this._bindings[12].ControllerValue == 0 ? "0" : this._bindings[12].ControllerValue.ToString().Substring(3, 3), new BitmapColor(255, 255, 0), 40);
                    break;
                case "COM2 Active":
                    this.SetBackground(bitmapBuilder, this._bindings[10]);
                    bitmapBuilder.DrawText("COM 2\n" + (this._bindings[1].ControllerValue / 1000000f).ToString("F3", CultureInfo.InvariantCulture));
                    break;
                case "COM2 Standby":
                    this.SetBackground(bitmapBuilder, this._bindings[10]);
                    bitmapBuilder.DrawText("Standby\n" + (this._bindings[13].ControllerValue / 1000000f).ToString("F3", CultureInfo.InvariantCulture));
                    break;
                case "COM2 Status":
                    this.SetBackground(bitmapBuilder, this._bindings[10]);
                    bitmapBuilder.DrawText("Status\n" + this.IntToCOMStatus(this._bindings[7].ControllerValue) + "\nType\n" + this.IntToCOMType(this._bindings[3].ControllerValue));
                    break;
                case "COM2 Active Int":
                    this.SetBackground(bitmapBuilder, this._bindings[10]);
                    bitmapBuilder.DrawText((this._bindings[1].ControllerValue == 0 ? "0" : this._bindings[1].ControllerValue.ToString().Substring(0, 3)) + ".", new BitmapColor(0, 255, 0), 40);
                    break;
                case "COM2 Active Float":
                    this.SetBackground(bitmapBuilder, this._bindings[10]);
                    bitmapBuilder.DrawText(this._bindings[1].ControllerValue == 0 ? "0" : this._bindings[1].ControllerValue.ToString().Substring(3, 3), new BitmapColor(0, 255, 0), 40);
                    break;
                case "COM2 Standby Int":
                    this.SetBackground(bitmapBuilder, this._bindings[10]);
                    bitmapBuilder.DrawText((this._bindings[13].ControllerValue == 0 ? "0" : this._bindings[13].ControllerValue.ToString().Substring(0, 3)) + ".", new BitmapColor(255, 255, 0), 40);
                    break;
                case "COM2 Standby Float":
                    this.SetBackground(bitmapBuilder, this._bindings[10]);
                    bitmapBuilder.DrawText(this._bindings[13].ControllerValue == 0 ? "0" : this._bindings[13].ControllerValue.ToString().Substring(3, 3), new BitmapColor(255, 255, 0), 40);
                    break;

            }
            return bitmapBuilder.ToImage();
        }

        private void SetBackground(BitmapBuilder bitmapBuilder, Binding binding)
        {
            if (binding.MsfsValue == 1)
            {
                bitmapBuilder.SetBackgroundImage(EmbeddedResources.ReadImage(this._imageAvailableResourcePath));
            }
            else
            {
                bitmapBuilder.SetBackgroundImage(EmbeddedResources.ReadImage(this._imageDisableResourcePath));
            }
        }

        public override void RunCommand(String actionParameter)
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
                    this._bindings[15].SetControllerValue(1);
                    break;
                case "COM2 Active":
                case "COM2 Active Int":
                case "COM2 Active Float":
                case "COM2 Standby":
                case "COM2 Standby Int":
                case "COM2 Standby Float":
                case "COM2 Int Reset":
                case "COM2 Float Reset":
                    this._bindings[16].SetControllerValue(1);
                    break;

            }
        }

        public override void ApplyAdjustment(String actionParameter, Int32 ticks)
        {
            switch (actionParameter)
            {
                case "COM1 Int Encoder":
                    var com1int = Int32.Parse(this._bindings[12].ControllerValue.ToString().Substring(0, 3));
                    var com1dbl = Int32.Parse(this._bindings[12].ControllerValue.ToString().Substring(3, 3));
                    var newInt = this.ApplyAdjustment(com1int, 118, 136, 1, ticks);
                    this._bindings[12].SetControllerValue(newInt * 1000000 + com1dbl * 1000);
                    break;
                case "COM1 Float Encoder":
                    var com1dbl1 = Int32.Parse(this._bindings[12].ControllerValue.ToString().Substring(3, 3));
                    var com1int1 = Int32.Parse(this._bindings[12].ControllerValue.ToString().Substring(0, 3));
                    var newFloat = this.ApplyAdjustment(com1dbl1, 0, 995, 5, ticks);
                    this._bindings[12].SetControllerValue(com1int1 * 1000000 + newFloat * 1000);
                    break;
                case "COM2 Int Encoder":
                    var com2int = Int32.Parse(this._bindings[13].ControllerValue.ToString().Substring(0, 3));
                    var com2dbl = Int32.Parse(this._bindings[13].ControllerValue.ToString().Substring(3, 3));
                    var newInt2 = this.ApplyAdjustment(com2int, 118, 136, 1, ticks);
                    this._bindings[13].SetControllerValue(newInt2 * 1000000 + com2dbl * 1000);
                    break;
                case "COM2 Float Encoder":
                    var com2dbl2 = Int32.Parse(this._bindings[13].ControllerValue.ToString().Substring(3, 3));
                    var com2int2 = Int32.Parse(this._bindings[13].ControllerValue.ToString().Substring(0, 3));
                    var newFloat2 = this.ApplyAdjustment(com2dbl2, 0, 995, 5, ticks);
                    this._bindings[13].SetControllerValue(com2int2 * 1000000 + newFloat2 * 1000);
                    break;

            }
            this.EncoderActionNamesChanged();
            this.ButtonActionNamesChanged();
        }
        public void Notify()
        {
            foreach (Binding binding in this._bindings)
            {
                    binding.Reset();
            }
        }
        private Int64 ApplyAdjustment(Int64 value, Int32 min, Int32 max, Int32 steps, Int32 ticks)
        {
            value += ticks * steps;
            if (value < min)
            { value = max; }
            else if (value > max)
            { value = min; }
            return value;
        }
        private String IntToCOMStatus(Int64 comStatus)
        {
            String type;
            switch (comStatus)
            {
                case -1:
                    type = "INVALID";
                    break;
                case 0:
                    type = "OK";
                    break;
                case 1:
                    type = "NOT EXIST";
                    break;
                case 2:
                    type = "NO ELEC";
                    break;
                case 3:
                    type = "FAILED";
                    break;
                default:
                    type = "Unknown";
                    break;
            }
            return type;
        }
        private String IntToCOMType(Int64 comType)
        {
            String type;
            switch (comType)
            {
                case 0 :
                    type = "ATIS";
                    break;
                case 1 :
                    type = "UNICOM";
                    break;
                case 2 :
                    type = "CTAF";
                    break;
                case 3 :
                    type = "GROUND";
                    break;
                case 4 :
                    type = "TOWER";
                    break;
                case 5 :
                    type = "CLR";
                    break;
                case 6 :
                    type = "APPR";
                    break;
                case 7 :
                    type = "DEP";
                    break;
                case 8 :
                    type = "FSS";
                    break;
                case 9 :
                    type = "AWOS";
                    break;
                default:
                    type = "Unknow";
                    break;
            }
            return type;
        }

    }
}
