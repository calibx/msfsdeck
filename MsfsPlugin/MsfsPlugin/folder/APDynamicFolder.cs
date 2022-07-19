namespace Loupedeck.MsfsPlugin.folder
{
    using System;
    using System.Collections.Generic;

    public class APDynamicFolder : PluginDynamicFolder, Notifiable
    {
        protected readonly String _imageOffResourcePath = "Loupedeck.MsfsPlugin.Resources.off.png";
        protected readonly String _imageOnResourcePath = "Loupedeck.MsfsPlugin.Resources.on.png";
        protected readonly List<Binding> _bindings = new List<Binding>();
        public APDynamicFolder()
        {
            this.DisplayName = "AP";
            this.GroupName = "Folder";
            this.Navigation = PluginDynamicFolderNavigation.None;
            MsfsData.Instance.Register(this);

            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_ALT_AP_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.ALT_AP_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_HEADING_AP_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.HEADING_AP_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_SPEED_AP_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.SPEED_AP_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_VSPEED_AP_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.VSPEED_AP_FOLDER)));

            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_ALT_SWITCH_AP_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_HEAD_SWITCH_AP_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_NAV_SWITCH_AP_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_SPEED_SWITCH_AP_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_MASTER_SWITCH_AP_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_THROTTLE_SWITCH_AP_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_VSPEED_SWITCH_AP_FOLDER)));
        }

        public override IEnumerable<String> GetEncoderRotateActionNames()
        {
            return new[]
            {
                this.CreateAdjustmentName ("Altitude Encoder"),
                this.CreateAdjustmentName ("Heading Encoder"),
                this.CreateAdjustmentName ("Speed Encoder"),
                this.CreateAdjustmentName ("VS Speed Encoder"),
            };
        }

        public override IEnumerable<String> GetEncoderPressActionNames()
        {
            return new[]
            {
                this.CreateCommandName("Altitude Reset"),
                this.CreateCommandName("Heading Reset"),
                this.CreateCommandName("Speed Reset"),
                this.CreateCommandName("VS Speed Reset"),
            };
        }
        public override IEnumerable<String> GetButtonPressActionNames()
        {
            return new[]
            {
                PluginDynamicFolder.NavigateUpActionName,
                this.CreateCommandName("Altitude"),
                this.CreateCommandName("Heading"),
                this.CreateCommandName("Nav"),
                this.CreateCommandName("Speed"),
                this.CreateCommandName("AP"),
                this.CreateCommandName("Throttle"),
                this.CreateCommandName("VS Speed"),
            };
        }
        public override String GetAdjustmentDisplayName(String actionParameter, PluginImageSize imageSize)
        {
            var ret = "";
            switch (actionParameter)
            {
                case "Altitude Encoder":
                    ret = "Alt\n[" + this._bindings[0].ControllerValue + "]\n" + this._bindings[1].ControllerValue;
                    break;
                case "Heading Encoder":
                    ret = "Head\n[" + this._bindings[2].ControllerValue + "]\n" + this._bindings[3].ControllerValue;
                    break;
                case "Speed Encoder":
                    ret = "Speed\n[" + this._bindings[4].ControllerValue + "]\n" + this._bindings[5].ControllerValue;
                    break;
                case "VS Speed Encoder":
                    ret = "VS\n[" + this._bindings[6].ControllerValue + "]\n" + this._bindings[7].ControllerValue;
                    break;
            }
            return ret;
        }
        public override BitmapImage GetCommandImage(String actionParameter, PluginImageSize imageSize)
        {
            var bitmapBuilder = new BitmapBuilder(imageSize);
            switch (actionParameter)
            {
                case "Altitude":
                    bitmapBuilder.SetBackgroundImage(this._bindings[8].ControllerValue == 1 ? EmbeddedResources.ReadImage(this._imageOnResourcePath) : EmbeddedResources.ReadImage(this._imageOffResourcePath));
                    break;
                case "Heading":
                    bitmapBuilder.SetBackgroundImage(this._bindings[9].ControllerValue == 1 ? EmbeddedResources.ReadImage(this._imageOnResourcePath) : EmbeddedResources.ReadImage(this._imageOffResourcePath));
                    break;
                case "Nav":
                    bitmapBuilder.SetBackgroundImage(this._bindings[10].ControllerValue == 1 ? EmbeddedResources.ReadImage(this._imageOnResourcePath) : EmbeddedResources.ReadImage(this._imageOffResourcePath));
                    break;
                case "Speed":
                    bitmapBuilder.SetBackgroundImage(this._bindings[11].ControllerValue == 1 ? EmbeddedResources.ReadImage(this._imageOnResourcePath) : EmbeddedResources.ReadImage(this._imageOffResourcePath));
                    break;
                case "AP":
                    bitmapBuilder.SetBackgroundImage(this._bindings[12].ControllerValue == 1 ? EmbeddedResources.ReadImage(this._imageOnResourcePath) : EmbeddedResources.ReadImage(this._imageOffResourcePath));
                    break;
                case "Throttle":
                    bitmapBuilder.SetBackgroundImage(this._bindings[13].ControllerValue == 1 ? EmbeddedResources.ReadImage(this._imageOnResourcePath) : EmbeddedResources.ReadImage(this._imageOffResourcePath));
                    break;
                case "VS Speed":
                    bitmapBuilder.SetBackgroundImage(this._bindings[14].ControllerValue == 1 ? EmbeddedResources.ReadImage(this._imageOnResourcePath) : EmbeddedResources.ReadImage(this._imageOffResourcePath));
                    break;
            }
            bitmapBuilder.DrawText(actionParameter);
            return bitmapBuilder.ToImage();
        }


        public override void ApplyAdjustment(String actionParameter, Int32 ticks)
        {
            switch (actionParameter)
            {
                case "Altitude Encoder":
                    this._bindings[0].SetControllerValue(this.ApplyAdjustment(this._bindings[0].ControllerValue, -10000, 99900, 100, ticks));
                    break;
                case "Heading Encoder":
                    this._bindings[2].SetControllerValue(this.ApplyAdjustmentForHeading(this._bindings[2].ControllerValue, 1, 360, 1, ticks));
                    break;
                case "Speed Encoder":
                    this._bindings[4].SetControllerValue(this.ApplyAdjustment(this._bindings[4].ControllerValue, 0, 2000, 1, ticks));
                    break;
                case "VS Speed Encoder":
                    this._bindings[6].SetControllerValue(this.ApplyAdjustment(this._bindings[6].ControllerValue, -10000, 10000, 100, ticks));
                    break;
            }
            this.EncoderActionNamesChanged();
        }
        public override void RunCommand(String actionParameter)
        {
            switch (actionParameter)
            {
                case "Altitude":
                    this._bindings[8].SetControllerValue(1);
                    break;
                case "Heading":
                    this._bindings[9].SetControllerValue(1);
                    break;
                case "Nav":
                    this._bindings[10].SetControllerValue(1);
                    break;
                case "Speed":
                    this._bindings[11].SetControllerValue(1);
                    break;
                case "AP":
                    this._bindings[12].SetControllerValue(1);
                    break;
                case "Throttle":
                    this._bindings[13].SetControllerValue(1);
                    break;
                case "VS Speed":
                    this._bindings[14].SetControllerValue(1);
                    break;
                case "Altitude Reset":
                    this._bindings[0].SetControllerValue((Int64)(Math.Round(this._bindings[1].ControllerValue / 100d, 0) * 100));
                    break;
                case "Heading Reset":
                    this._bindings[2].SetControllerValue(this._bindings[3].ControllerValue);
                    break;
                case "Speed Reset":
                    this._bindings[4].SetControllerValue((Int64)(Math.Round(this._bindings[5].ControllerValue / 100d, 0) * 100));
                    break;
                case "VS Speed Reset":
                    this._bindings[6].SetControllerValue((Int64)(Math.Round(this._bindings[7].ControllerValue / 100d, 0) * 100));
                    break;
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
        
        private Int64 ApplyAdjustmentForHeading(Int64 value, Int32 min, Int32 max, Int32 steps, Int32 ticks)
        {
            value += ticks * steps;
            if (value < min)
            { value = max; }
            else if (value > max)
            { value = min; }
            return value;
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
                        case BindingKeys.AP_ALT_AP_FOLDER:
                        case BindingKeys.ALT_AP_FOLDER:
                            this.AdjustmentValueChanged("Altitude Encoder");
                            break;
                        case BindingKeys.AP_HEADING_AP_FOLDER:
                        case BindingKeys.HEADING_AP_FOLDER:
                            this.AdjustmentValueChanged("Heading Encoder");
                            break;
                        case BindingKeys.AP_SPEED_AP_FOLDER:
                        case BindingKeys.SPEED_AP_FOLDER:
                            this.AdjustmentValueChanged("Speed Encoder");
                            break;
                        case BindingKeys.AP_VSPEED_AP_FOLDER:
                        case BindingKeys.VSPEED_AP_FOLDER:
                            this.AdjustmentValueChanged("VS Speed Encoder");
                            break;
                        default:
                            this.ButtonActionNamesChanged();
                            break;
                    }


                }
            }
        }
    }
}
