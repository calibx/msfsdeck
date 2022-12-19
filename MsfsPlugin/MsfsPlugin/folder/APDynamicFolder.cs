namespace Loupedeck.MsfsPlugin.folder
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    using Loupedeck.MsfsPlugin.tools;

    public class APDynamicFolder : PluginDynamicFolder, Notifiable
    {
        protected readonly List<Binding> _bindings = new List<Binding>();
        public APDynamicFolder()
        {
            this.DisplayName = "AP";
            this.GroupName = "Folder";
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
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_YAW_DAMPER_AP_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_BC_AP_FOLDER)));

        }

        public override PluginDynamicFolderNavigation GetNavigationArea(DeviceType _) => PluginDynamicFolderNavigation.None;
        public override IEnumerable<String> GetEncoderRotateActionNames(DeviceType deviceType)
        {
            return new[]
            {
                this.CreateAdjustmentName ("Altitude Encoder"),
                this.CreateAdjustmentName ("Heading Encoder"),
                this.CreateAdjustmentName ("Speed Encoder"),
                this.CreateAdjustmentName ("VS Speed Encoder"),
            };
        }

        public override IEnumerable<String> GetEncoderPressActionNames(DeviceType deviceType)
        {
            return new[]
            {
                this.CreateCommandName("Altitude Reset"),
                this.CreateCommandName("Heading Reset"),
                this.CreateCommandName("Speed Reset"),
                this.CreateCommandName("VS Speed Reset"),
            };
        }
        public override IEnumerable<String> GetButtonPressActionNames(DeviceType deviceType)
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
                this.CreateCommandName("Yaw Damper"),
                this.CreateCommandName("Back Course"),
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
            Debug.WriteLine(actionParameter);
            var bitmapBuilder = new BitmapBuilder(imageSize);
            switch (actionParameter)
            {
                case "Altitude":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(this._bindings[8].ControllerValue));
                    break;
                case "Heading":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(this._bindings[9].ControllerValue));
                    break;
                case "Nav":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(this._bindings[10].ControllerValue));
                    break;
                case "Speed":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(this._bindings[11].ControllerValue));
                    break;
                case "AP":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(this._bindings[12].ControllerValue));
                    break;
                case "Throttle":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(this._bindings[13].ControllerValue));
                    break;
                case "VS Speed":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(this._bindings[14].ControllerValue));
                    break;
                case "Yaw Damper":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(this._bindings[15].ControllerValue));
                    break;
                case "Back Course":
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(this._bindings[16].ControllerValue));
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
                    this._bindings[0].SetControllerValue(ConvertTool.ApplyAdjustment(this._bindings[0].ControllerValue, ticks, -10000, 99900, 100));
                    break;
                case "Heading Encoder":
                    this._bindings[2].SetControllerValue(ConvertTool.ApplyAdjustment(this._bindings[2].ControllerValue, ticks, 1, 360, 1, true));
                    break;
                case "Speed Encoder":
                    this._bindings[4].SetControllerValue(ConvertTool.ApplyAdjustment(this._bindings[4].ControllerValue, ticks, 0, 2000, 1));
                    break;
                case "VS Speed Encoder":
                    this._bindings[6].SetControllerValue(ConvertTool.ApplyAdjustment(this._bindings[6].ControllerValue, ticks, -10000, 10000, 100));
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
                case "Yaw Damper":
                    this._bindings[15].SetControllerValue(1);
                    break;
                case "Back Course":
                    this._bindings[16].SetControllerValue(1);
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
