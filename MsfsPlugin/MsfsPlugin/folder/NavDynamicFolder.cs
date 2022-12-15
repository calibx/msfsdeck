namespace Loupedeck.MsfsPlugin.folder
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    using Loupedeck.MsfsPlugin.tools;

    public class NavDynamicFolder : PluginDynamicFolder, Notifiable
    {
        protected readonly List<Binding> _bindings = new List<Binding>();
        public NavDynamicFolder()
        {
            this.DisplayName = "NAV";
            this.GroupName = "Folder";

            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.NAV1_ACTIVE_FREQUENCY)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.NAV2_ACTIVE_FREQUENCY)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.NAV1_AVAILABLE)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.NAV2_AVAILABLE)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.NAV1_STBY_FREQUENCY)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.NAV2_STBY_FREQUENCY)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.NAV1_RADIO_SWAP)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.NAV2_RADIO_SWAP)));

            MsfsData.Instance.Register(this);

        }

        public override PluginDynamicFolderNavigation GetNavigationArea(DeviceType _) => PluginDynamicFolderNavigation.None;
        public override IEnumerable<String> GetButtonPressActionNames(DeviceType deviceType)
        {
            return new[]
            {
                this.CreateCommandName("NAV1 Active Int"),
                this.CreateCommandName("NAV1 Active Float"),
                this.CreateCommandName("NAV1 Standby Int"),
                this.CreateCommandName("NAV1 Standby Float"),
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
                this.CreateAdjustmentName ("NAV1 Int Encoder"),
                this.CreateAdjustmentName ("NAV2 Int Encoder"),
                this.CreateAdjustmentName (""),
                this.CreateAdjustmentName ("NAV1 Float Encoder"),
                this.CreateAdjustmentName ("NAV2 Float Encoder"),
            };
        }

        public override IEnumerable<String> GetEncoderPressActionNames(DeviceType deviceType)
        {
            return new[]
            {
                this.CreateCommandName("NAV1 Int Reset"),
                this.CreateCommandName("NAV2 Int Reset"),
                this.CreateCommandName (""),
                this.CreateCommandName("NAV1 Float Reset"),
                this.CreateCommandName("NAV2 Float Reset"),
            };
        }
        public override String GetAdjustmentDisplayName(String actionParameter, PluginImageSize imageSize)
        {
            var ret = "";
            switch (actionParameter)
            {
                case "NAV1 Int Encoder":
                    ret = "NAV1\n" + Math.Truncate(this._bindings[4].ControllerValue / 1000000f) + ".";
                    break;
                case "NAV1 Float Encoder":
                    var com1dbl = Math.Round(this._bindings[4].ControllerValue / 1000000f - Math.Truncate(this._bindings[4].ControllerValue / 1000000f), 3).ToString();
                    ret = "NAV1\n" + (com1dbl.Length > 2 ? com1dbl.Substring(2) : com1dbl).PadRight(2, '0');
                    break;
                case "NAV2 Int Encoder":
                    ret = "NAV2\n" + Math.Truncate(this._bindings[5].ControllerValue / 1000000f) + ".";
                    break;
                case "NAV2 Float Encoder":
                    var com2dbl = Math.Round(this._bindings[5].ControllerValue / 1000000f - Math.Truncate(this._bindings[5].ControllerValue / 1000000f), 3).ToString();
                    ret = "NAV2\n" + (com2dbl.Length > 2 ? com2dbl.Substring(2) : com2dbl).PadRight(2, '0');
                    break;
            }
            return ret;
        }
        public override BitmapImage GetCommandImage(String actionParameter, PluginImageSize imageSize)
        {
            var bitmapBuilder = new BitmapBuilder(imageSize);
            switch (actionParameter)
            {
                case "NAV1 Active Int":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(this._bindings[2].MsfsValue));
                    bitmapBuilder.DrawText((this._bindings[0].ControllerValue == 0 ? "0" : this._bindings[0].ControllerValue.ToString().Substring(0, 3)) + ".", new BitmapColor(0, 255, 0), 40);
                    break;
                case "NAV1 Active Float":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(this._bindings[2].MsfsValue));
                    bitmapBuilder.DrawText(this._bindings[0].ControllerValue == 0 ? "0" : this._bindings[0].ControllerValue.ToString().Substring(3, 2), new BitmapColor(0, 255, 0), 40);
                    break;
                case "NAV1 Standby Int":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(this._bindings[2].MsfsValue));
                    bitmapBuilder.DrawText((this._bindings[4].ControllerValue == 0 ? "0" : this._bindings[4].ControllerValue.ToString().Substring(0, 3)) + ".", new BitmapColor(255, 255, 0), 40);
                    break;
                case "NAV1 Standby Float":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(this._bindings[2].MsfsValue));
                    bitmapBuilder.DrawText(this._bindings[4].ControllerValue == 0 ? "0" : this._bindings[4].ControllerValue.ToString().Substring(3, 2), new BitmapColor(255, 255, 0), 40);
                    break;
                case "NAV2 Active Int":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(this._bindings[3].MsfsValue));
                    bitmapBuilder.DrawText((this._bindings[1].ControllerValue == 0 ? "0" : this._bindings[1].ControllerValue.ToString().Substring(0, 3)) + ".", new BitmapColor(0, 255, 0), 40);
                    break;
                case "NAV2 Active Float":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(this._bindings[3].MsfsValue));
                    bitmapBuilder.DrawText(this._bindings[1].ControllerValue == 0 ? "0" : this._bindings[1].ControllerValue.ToString().Substring(3, 2), new BitmapColor(0, 255, 0), 40);
                    break;
                case "NAV2 Standby Int":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(this._bindings[3].MsfsValue));
                    bitmapBuilder.DrawText((this._bindings[5].ControllerValue == 0 ? "0" : this._bindings[5].ControllerValue.ToString().Substring(0, 3)) + ".", new BitmapColor(255, 255, 0), 40);
                    break;
                case "NAV2 Standby Float":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(this._bindings[3].MsfsValue));
                    bitmapBuilder.DrawText(this._bindings[5].ControllerValue == 0 ? "0" : this._bindings[5].ControllerValue.ToString().Substring(3, 2), new BitmapColor(255, 255, 0), 40);
                    break;

            }
            return bitmapBuilder.ToImage();
        }

        public override void RunCommand(String actionParameter)
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
                    this._bindings[6].SetControllerValue(1);
                    break;
                case "NAV2 Active":
                case "NAV2 Active Int":
                case "NAV2 Active Float":
                case "NAV2 Standby":
                case "NAV2 Standby Int":
                case "NAV2 Standby Float":
                case "NAV2 Int Reset":
                case "NAV2 Float Reset":
                    this._bindings[7].SetControllerValue(1);
                    break;

            }
        }

        public override void ApplyAdjustment(String actionParameter, Int32 ticks)
        {
            switch (actionParameter)
            {
                case "NAV1 Int Encoder":
                    var com1int = Int32.Parse(this._bindings[4].ControllerValue.ToString().Substring(0, 3));
                    var com1dbl = Int32.Parse(this._bindings[4].ControllerValue.ToString().Substring(3, 2));
                    var newInt = ConvertTool.ApplyAdjustment(com1int, ticks, 108, 117, 1, true);
                    this._bindings[4].SetControllerValue(newInt * 1000000 + com1dbl * 10000);
                    break;
                case "NAV1 Float Encoder":
                    var com1dbl1 = Int32.Parse(this._bindings[4].ControllerValue.ToString().Substring(3, 2));
                    var com1int1 = Int32.Parse(this._bindings[4].ControllerValue.ToString().Substring(0, 3));
                    var newFloat = ConvertTool.ApplyAdjustment(com1dbl1, ticks, 0, 99, 1, true);
                    this._bindings[4].SetControllerValue(com1int1 * 1000000 + newFloat * 10000);
                    break;
                case "NAV2 Int Encoder":
                    var com2int = Int32.Parse(this._bindings[5].ControllerValue.ToString().Substring(0, 3));
                    var com2dbl = Int32.Parse(this._bindings[5].ControllerValue.ToString().Substring(3, 2));
                    var newInt2 = ConvertTool.ApplyAdjustment(com2int, ticks, 108, 117, 1, true);
                    this._bindings[5].SetControllerValue(newInt2 * 1000000 + com2dbl * 10000);
                    break;
                case "NAV2 Float Encoder":
                    var com2dbl2 = Int32.Parse(this._bindings[5].ControllerValue.ToString().Substring(3, 2));
                    var com2int2 = Int32.Parse(this._bindings[5].ControllerValue.ToString().Substring(0, 3));
                    var newFloat2 = ConvertTool.ApplyAdjustment(com2dbl2, ticks, 0, 99, 1, true);
                    this._bindings[5].SetControllerValue(com2int2 * 1000000 + newFloat2 * 10000);
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
