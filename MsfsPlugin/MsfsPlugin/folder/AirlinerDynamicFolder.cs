namespace Loupedeck.MsfsPlugin.folder
{
    using System;
    using System.Collections.Generic;

    public class AirlinerDynamicFolder : PluginDynamicFolder, Notifiable
    {
        protected readonly String _imageOffResourcePath = "Loupedeck.MsfsPlugin.Resources.off.png";
        protected readonly String _imageOnResourcePath = "Loupedeck.MsfsPlugin.Resources.on.png";
        protected readonly List<Binding> _bindings = new List<Binding>();
        public AirlinerDynamicFolder()
        {
            this.DisplayName = "Airliner";
            this.GroupName = "Folder";
            this.Navigation = PluginDynamicFolderNavigation.None;
            MsfsData.Instance.Register(this);

            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_ALT_AL_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.ALT_AL_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_HEADING_AL_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.HEADING_AL_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_SPEED_AL_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.SPEED_AL_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_VSPEED_AL_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.VSPEED_AL_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_FD_SWITCH_AL_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_ALT_SWITCH_AL_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_SWITCH_AL_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_GPS_SWITCH_AL_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_FLC_SWITCH_AL_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_APP_SWITCH_AL_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_LOC_SWITCH_AL_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_SPEED_SWITCH_AL_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_HEAD_SWITCH_AL_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_THROTTLE_SWITCH_AL_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_VSPEED_SWITCH_AL_FOLDER)));

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
                this.CreateCommandName("FD"),
                this.CreateCommandName("Altitude"),
                this.CreateCommandName("AP"),
                this.CreateCommandName("GPS"),
                this.CreateCommandName("FLC"),
                this.CreateCommandName("APP"),
                this.CreateCommandName("LOC"),
                this.CreateCommandName("Speed"),
                this.CreateCommandName("Heading"),
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
                case "LOC":
                    bitmapBuilder.SetBackgroundImage(this._bindings[14].ControllerValue == 1 ? EmbeddedResources.ReadImage(this._imageOnResourcePath) : EmbeddedResources.ReadImage(this._imageOffResourcePath));
                    break;
                case "FD":
                    bitmapBuilder.SetBackgroundImage(this._bindings[8].ControllerValue == 1 ? EmbeddedResources.ReadImage(this._imageOnResourcePath) : EmbeddedResources.ReadImage(this._imageOffResourcePath));
                    break;
                case "FLC":
                    bitmapBuilder.SetBackgroundImage(this._bindings[12].ControllerValue == 1 ? EmbeddedResources.ReadImage(this._imageOnResourcePath) : EmbeddedResources.ReadImage(this._imageOffResourcePath));
                    break;
                case "APP":
                    bitmapBuilder.SetBackgroundImage(this._bindings[13].ControllerValue == 1 ? EmbeddedResources.ReadImage(this._imageOnResourcePath) : EmbeddedResources.ReadImage(this._imageOffResourcePath));
                    break;
                case "Altitude":
                    bitmapBuilder.SetBackgroundImage(this._bindings[9].ControllerValue == 1 ? EmbeddedResources.ReadImage(this._imageOnResourcePath) : EmbeddedResources.ReadImage(this._imageOffResourcePath));
                    break;
                case "Heading":
                    bitmapBuilder.SetBackgroundImage(this._bindings[16].ControllerValue == 1 ? EmbeddedResources.ReadImage(this._imageOnResourcePath) : EmbeddedResources.ReadImage(this._imageOffResourcePath));
                    break;
                case "GPS":
                    bitmapBuilder.SetBackgroundImage(this._bindings[11].ControllerValue == 1 ? EmbeddedResources.ReadImage(this._imageOnResourcePath) : EmbeddedResources.ReadImage(this._imageOffResourcePath));
                    break;
                case "Speed":
                    bitmapBuilder.SetBackgroundImage(this._bindings[15].ControllerValue == 1 ? EmbeddedResources.ReadImage(this._imageOnResourcePath) : EmbeddedResources.ReadImage(this._imageOffResourcePath));
                    break;
                case "AP":
                    bitmapBuilder.SetBackgroundImage(this._bindings[10].ControllerValue == 1 ? EmbeddedResources.ReadImage(this._imageOnResourcePath) : EmbeddedResources.ReadImage(this._imageOffResourcePath));
                    break;
                case "Throttle":
                    bitmapBuilder.SetBackgroundImage(this._bindings[17].ControllerValue == 1 ? EmbeddedResources.ReadImage(this._imageOnResourcePath) : EmbeddedResources.ReadImage(this._imageOffResourcePath));
                    break;
                case "VS Speed":
                    bitmapBuilder.SetBackgroundImage(this._bindings[18].ControllerValue == 1 ? EmbeddedResources.ReadImage(this._imageOnResourcePath) : EmbeddedResources.ReadImage(this._imageOffResourcePath));
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
                    this._bindings[2].SetControllerValue(this.ApplyAdjustment(this._bindings[2].ControllerValue, 0, 360, 1, ticks));
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
                case "LOC":
                    this._bindings[14].SetControllerValue(1);
                    break;
                case "FD":
                    this._bindings[8].SetControllerValue(1);
                    break;
                case "FLC":
                    this._bindings[12].SetControllerValue(1);
                    break;
                case "APP":
                    this._bindings[13].SetControllerValue(1);
                    break;
                case "Altitude":
                    this._bindings[9].SetControllerValue(1);
                    break;
                case "Heading":
                    this._bindings[16].SetControllerValue(1);
                    break;
                case "GPS":
                    this._bindings[11].SetControllerValue(1);
                    break;
                case "Speed":
                    this._bindings[15].SetControllerValue(1);
                    break;
                case "AP":
                    this._bindings[10].SetControllerValue(1);
                    break;
                case "Throttle":
                    this._bindings[17].SetControllerValue(1);
                    break;
                case "VS Speed":
                    this._bindings[18].SetControllerValue(1);
                    break;
                case "Altitude Reset":
                    //this._bindings[18].SetControllerValue(1);
                    break;
                case "Heading Reset":
                    //this._bindings[18].SetControllerValue(1);
                    break;
                case "Speed Reset":
                    //this._bindings[18].SetControllerValue(1);
                    break;
                case "VS Speed Reset":
                    //this._bindings[18].SetControllerValue(1);
                    break;
            }
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

        private Int64 ApplyAdjustment(Int64 value, Int32 min, Int32 max, Int32 steps, Int32 ticks)
        {
            value += ticks * steps;
            if (value < min)
            { value = min; }
            else if (value > max)
            { value = max; }
            return value;
        }
    }
}
