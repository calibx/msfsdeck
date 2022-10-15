namespace Loupedeck.MsfsPlugin.folder
{
    using System;
    using System.Collections.Generic;

    public class LightsDynamicFolder : PluginDynamicFolder, Notifiable
    {
        protected readonly String _imageOffResourcePath = "Loupedeck.MsfsPlugin.Resources.off.png";
        protected readonly String _imageOnResourcePath = "Loupedeck.MsfsPlugin.Resources.on.png";
        protected readonly List<Binding> _bindings = new List<Binding>();
        public LightsDynamicFolder()
        {
            this.DisplayName = "Lights";
            this.GroupName = "Folder";
            this.Navigation = PluginDynamicFolderNavigation.None;

            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.LIGHT_NAV_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.LIGHT_BEACON_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.LIGHT_LANDING_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.LIGHT_TAXI_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.LIGHT_STROBE_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.LIGHT_INSTRUMENT_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.LIGHT_RECOG_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.LIGHT_WING_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.LIGHT_LOGO_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.LIGHT_CABIN_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.LIGHT_PEDESTRAL_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.LIGHT_GLARESHIELD_FOLDER)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.LIGHT_ALL_SWITCH_FOLDER)));
            MsfsData.Instance.Register(this);

        }
        public override IEnumerable<String> GetButtonPressActionNames()
        {
            return new[]
            {
                PluginDynamicFolder.NavigateUpActionName,
                this.CreateCommandName("Navigation"),
                this.CreateCommandName("Beacon"),
                this.CreateCommandName("Landing"),
                this.CreateCommandName("Taxi"),
                this.CreateCommandName("Strobes"),
                this.CreateCommandName("Instruments"),
                this.CreateCommandName("Recognition"),
                this.CreateCommandName("Wing"),
                this.CreateCommandName("Logo"),
                this.CreateCommandName("Cabin"),
                this.CreateCommandName("Pedestral"),
                this.CreateCommandName("Glareshield"),
                this.CreateCommandName("All")
            };
        }
        public override BitmapImage GetCommandImage(String actionParameter, PluginImageSize imageSize)
        {
            var bitmapBuilder = new BitmapBuilder(imageSize);
            switch (actionParameter)
            {
                case "Navigation":
                    bitmapBuilder.SetBackgroundImage(this._bindings[0].ControllerValue == 1 ? EmbeddedResources.ReadImage(this._imageOnResourcePath) : EmbeddedResources.ReadImage(this._imageOffResourcePath));
                    break;
                case "Beacon":
                    bitmapBuilder.SetBackgroundImage(this._bindings[1].ControllerValue == 1 ? EmbeddedResources.ReadImage(this._imageOnResourcePath) : EmbeddedResources.ReadImage(this._imageOffResourcePath));
                    break;
                case "Landing":
                    bitmapBuilder.SetBackgroundImage(this._bindings[2].ControllerValue == 1 ? EmbeddedResources.ReadImage(this._imageOnResourcePath) : EmbeddedResources.ReadImage(this._imageOffResourcePath));
                    break;
                case "Taxi":
                    bitmapBuilder.SetBackgroundImage(this._bindings[3].ControllerValue == 1 ? EmbeddedResources.ReadImage(this._imageOnResourcePath) : EmbeddedResources.ReadImage(this._imageOffResourcePath));
                    break;
                case "Strobes":
                    bitmapBuilder.SetBackgroundImage(this._bindings[4].ControllerValue == 1 ? EmbeddedResources.ReadImage(this._imageOnResourcePath) : EmbeddedResources.ReadImage(this._imageOffResourcePath));
                    break;
                case "Instruments":
                    bitmapBuilder.SetBackgroundImage(this._bindings[5].ControllerValue == 1 ? EmbeddedResources.ReadImage(this._imageOnResourcePath) : EmbeddedResources.ReadImage(this._imageOffResourcePath));
                    break;
                case "Recognition":
                    bitmapBuilder.SetBackgroundImage(this._bindings[6].ControllerValue == 1 ? EmbeddedResources.ReadImage(this._imageOnResourcePath) : EmbeddedResources.ReadImage(this._imageOffResourcePath));
                    break;
                case "Wing":
                    bitmapBuilder.SetBackgroundImage(this._bindings[7].ControllerValue == 1 ? EmbeddedResources.ReadImage(this._imageOnResourcePath) : EmbeddedResources.ReadImage(this._imageOffResourcePath));
                    break;
                case "Logo":
                    bitmapBuilder.SetBackgroundImage(this._bindings[8].ControllerValue == 1 ? EmbeddedResources.ReadImage(this._imageOnResourcePath) : EmbeddedResources.ReadImage(this._imageOffResourcePath));
                    break;
                case "Cabin":
                    bitmapBuilder.SetBackgroundImage(this._bindings[9].ControllerValue == 1 ? EmbeddedResources.ReadImage(this._imageOnResourcePath) : EmbeddedResources.ReadImage(this._imageOffResourcePath));
                    break;
                case "Pedestral":
                    bitmapBuilder.SetBackgroundImage(this._bindings[10].ControllerValue == 1 ? EmbeddedResources.ReadImage(this._imageOnResourcePath) : EmbeddedResources.ReadImage(this._imageOffResourcePath));
                    break;
                case "Glareshield":
                    bitmapBuilder.SetBackgroundImage(this._bindings[11].ControllerValue == 1 ? EmbeddedResources.ReadImage(this._imageOnResourcePath) : EmbeddedResources.ReadImage(this._imageOffResourcePath));
                    break;
                case "All":
                    break;
            }
            bitmapBuilder.DrawText(actionParameter);

            return bitmapBuilder.ToImage();
        }

        public override void RunCommand(String actionParameter)
        {
            switch (actionParameter)
            {
                case "Navigation":
                    this._bindings[0].SetControllerValue(1);
                    break;
                case "Beacon":
                    this._bindings[1].SetControllerValue(1);
                    break;
                case "Landing":
                    this._bindings[2].SetControllerValue(1);
                    break;
                case "Taxi":
                    this._bindings[3].SetControllerValue(1);
                    break;
                case "Strobes":
                    this._bindings[4].SetControllerValue(1);
                    break;
                case "Instruments":
                    this._bindings[5].SetControllerValue(1);
                    break;
                case "Recognition":
                    this._bindings[6].SetControllerValue(1);
                    break;
                case "Wing":
                    this._bindings[7].SetControllerValue(1);
                    break;
                case "Logo":
                    this._bindings[8].SetControllerValue(1);
                    break;
                case "Cabin":
                    this._bindings[9].SetControllerValue(1);
                    break;
                case "Pedestral":
                    this._bindings[10].SetControllerValue(1);
                    break;
                case "Glareshield":
                    this._bindings[11].SetControllerValue(1);
                    break;
                case "All":
                    this._bindings[12].SetControllerValue(1);
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
