namespace Loupedeck.MsfsPlugin.folder
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    public class APMultiInputs : PluginDynamicCommand, Notifiable
    {
        protected readonly String _imageOffResourcePath = "Loupedeck.MsfsPlugin.Resources.off.png";
        protected readonly String _imageOnResourcePath = "Loupedeck.MsfsPlugin.Resources.on.png";
        protected readonly String _imageDisconnectResourcePath = "Loupedeck.MsfsPlugin.Resources.disconnect.png";
        protected readonly String _imageTryingResourcePath = "Loupedeck.MsfsPlugin.Resources.trying.png";

        protected readonly List<Binding> _bindings = new List<Binding>();
        protected readonly Binding _bindingCnx = MsfsData.Instance.Register(new Binding(BindingKeys.CONNECTION));

        public APMultiInputs() : base()
        {
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_ALT_SWITCH)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_HEAD_SWITCH)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_NAV_SWITCH)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_SPEED_SWITCH)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_MASTER_SWITCH)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_THROTTLE_SWITCH)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_VSPEED_SWITCH)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_FD_SWITCH_AL_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_FLC_SWITCH_AL_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_APP_SWITCH_AL_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AP_LOC_SWITCH_AL_FOLDER)));


            this.AddParameter("AP Alt", "Autopilot Altitude Switch", "AP");
            this.AddParameter("AP Head", "Autopilot Heading Switch", "AP");
            this.AddParameter("AP Nav", "Autopilot Nav Switch", "AP");
            this.AddParameter("AP Speed", "Autopilot Speed Switch", "AP");
            this.AddParameter("AP Master", "Autopilot Master Switch", "AP");
            this.AddParameter("AP Throttle", "Autopilot ThrottleSwitch", "AP");
            this.AddParameter("AP VSpeed", "Autopilot VSpeed Switch", "AP");
            this.AddParameter("AP FD", "Autopilot FD Switch", "AP");
            this.AddParameter("AP FLC", "Autopilot FLC Switch", "AP");
            this.AddParameter("AP APP", "Autopilot APP Switch", "AP");
            this.AddParameter("AP LOC", "Autopilot LOC Switch", "AP");
            MsfsData.Instance.Register(this);

        }
        protected override BitmapImage GetCommandImage(String actionParameter, PluginImageSize imageSize)
        {
            if (this._bindingCnx.MsfsValue == 0)
            {
                using (var bitmapBuilder = new BitmapBuilder(imageSize))
                {
                    bitmapBuilder.SetBackgroundImage(EmbeddedResources.ReadImage(this._imageDisconnectResourcePath));
                    bitmapBuilder.DrawText(actionParameter);
                    return bitmapBuilder.ToImage();
                }
            }
            else if (this._bindingCnx.MsfsValue == 2)
            {
                using (var bitmapBuilder = new BitmapBuilder(imageSize))
                {
                    bitmapBuilder.SetBackgroundImage(EmbeddedResources.ReadImage(this._imageTryingResourcePath));
                    bitmapBuilder.DrawText(actionParameter);
                    return bitmapBuilder.ToImage();
                }
            }
            else
            {
                return this.GetImage(actionParameter, imageSize);
            }
        }

        private BitmapImage GetImage(String actionParameter, PluginImageSize imageSize)
        {
            var bitmapBuilder = new BitmapBuilder(imageSize);
            switch (actionParameter)
            {
                case "AP Alt":
                    bitmapBuilder.SetBackgroundImage(this._bindings[0].ControllerValue == 1 ? EmbeddedResources.ReadImage(this._imageOnResourcePath) : EmbeddedResources.ReadImage(this._imageOffResourcePath));
                    break;
                case "AP Head":
                    bitmapBuilder.SetBackgroundImage(this._bindings[1].ControllerValue == 1 ? EmbeddedResources.ReadImage(this._imageOnResourcePath) : EmbeddedResources.ReadImage(this._imageOffResourcePath));
                    break;
                case "AP Nav":
                    bitmapBuilder.SetBackgroundImage(this._bindings[2].ControllerValue == 1 ? EmbeddedResources.ReadImage(this._imageOnResourcePath) : EmbeddedResources.ReadImage(this._imageOffResourcePath));
                    break;
                case "AP Speed":
                    bitmapBuilder.SetBackgroundImage(this._bindings[3].ControllerValue == 1 ? EmbeddedResources.ReadImage(this._imageOnResourcePath) : EmbeddedResources.ReadImage(this._imageOffResourcePath));
                    break;
                case "AP Master":
                    bitmapBuilder.SetBackgroundImage(this._bindings[4].ControllerValue == 1 ? EmbeddedResources.ReadImage(this._imageOnResourcePath) : EmbeddedResources.ReadImage(this._imageOffResourcePath));
                    break;
                case "AP Throttle":
                    bitmapBuilder.SetBackgroundImage(this._bindings[5].ControllerValue == 1 ? EmbeddedResources.ReadImage(this._imageOnResourcePath) : EmbeddedResources.ReadImage(this._imageOffResourcePath));
                    break;
                case "AP VSpeed":
                    bitmapBuilder.SetBackgroundImage(this._bindings[6].ControllerValue == 1 ? EmbeddedResources.ReadImage(this._imageOnResourcePath) : EmbeddedResources.ReadImage(this._imageOffResourcePath));
                    break;
                case "AP FD":
                    bitmapBuilder.SetBackgroundImage(this._bindings[7].ControllerValue == 1 ? EmbeddedResources.ReadImage(this._imageOnResourcePath) : EmbeddedResources.ReadImage(this._imageOffResourcePath));
                    break;
                case "AP FLC":
                    bitmapBuilder.SetBackgroundImage(this._bindings[8].ControllerValue == 1 ? EmbeddedResources.ReadImage(this._imageOnResourcePath) : EmbeddedResources.ReadImage(this._imageOffResourcePath));
                    break;
                case "AP APP":
                    bitmapBuilder.SetBackgroundImage(this._bindings[9].ControllerValue == 1 ? EmbeddedResources.ReadImage(this._imageOnResourcePath) : EmbeddedResources.ReadImage(this._imageOffResourcePath));
                    break;
                case "AP LOC":
                    bitmapBuilder.SetBackgroundImage(this._bindings[10].ControllerValue == 1 ? EmbeddedResources.ReadImage(this._imageOnResourcePath) : EmbeddedResources.ReadImage(this._imageOffResourcePath));
                    break;
            }
            bitmapBuilder.DrawText(actionParameter);
            return bitmapBuilder.ToImage();
        }
        protected override void RunCommand(String actionParameter)
        {
             switch (actionParameter)
            {
                case "AP Alt":
                    this._bindings[0].SetControllerValue(1);
                    break;
                case "AP Head":
                    this._bindings[1].SetControllerValue(1);
                    break;
                case "AP Nav":
                    this._bindings[2].SetControllerValue(1);
                    break;
                case "AP Speed":
                    this._bindings[3].SetControllerValue(1);
                    break;
                case "AP Master":
                    this._bindings[4].SetControllerValue(1);
                    break;
                case "AP Throttle":
                    this._bindings[5].SetControllerValue(1);
                    break;
                case "AP VSpeed":
                    this._bindings[6].SetControllerValue(1);
                    break;
                case "AP FD":
                    this._bindings[7].SetControllerValue(1);
                    break;
                case "AP FLC":
                    this._bindings[8].SetControllerValue(1);
                    break;
                case "AP APP":
                    this._bindings[9].SetControllerValue(1);
                    break;
                case "AP LOC":
                    this._bindings[10].SetControllerValue(1);
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
                    Debug.WriteLine(binding.Key);
                    this.ActionImageChanged();
                }
            }
        }
    }

}
