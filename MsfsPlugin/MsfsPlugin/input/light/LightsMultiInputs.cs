namespace Loupedeck.MsfsPlugin.folder
{
    using System;
    using System.Collections.Generic;

    using Loupedeck.MsfsPlugin.msfs;

    public class LightsMultiInputs : PluginDynamicCommand, Notifiable
    {
        protected readonly String _imageOffResourcePath = "Loupedeck.MsfsPlugin.Resources.off.png";
        protected readonly String _imageOnResourcePath = "Loupedeck.MsfsPlugin.Resources.on.png";
        protected readonly List<Binding> _bindings = new List<Binding>();

        public LightsMultiInputs() : base()
        {
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.LIGHT_NAV_MULTI)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.LIGHT_BEACON_MULTI)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.LIGHT_LANDING_MULTI)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.LIGHT_TAXI_MULTI)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.LIGHT_STROBE_MULTI)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.LIGHT_INSTRUMENT_MULTI)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.LIGHT_RECOG_MULTI)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.LIGHT_WING_MULTI)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.LIGHT_LOGO_MULTI)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.LIGHT_CABIN_MULTI)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.LIGHT_PEDESTRAL_MULTI)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.LIGHT_GLARESHIELD_MULTI)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.LIGHT_ALL_SWITCH_MULTI)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.FLASHLIGHT)));

            this.AddParameter("Navigation", "Navigation light", "Lights");
            this.AddParameter("Beacon", "Beacon light", "Lights");
            this.AddParameter("Landing", "Landing light", "Lights");
            this.AddParameter("Taxi", "Taxi light", "Lights");
            this.AddParameter("Strobes", "Strobes light", "Lights");
            this.AddParameter("Instruments", "Instruments light", "Lights");
            this.AddParameter("Recognition", "Recognition light", "Lights");
            this.AddParameter("Wing", "Wing light", "Lights");
            this.AddParameter("Logo", "Logo light", "Lights");
            this.AddParameter("Cabin", "Cabin light", "Lights");
            this.AddParameter("Pedestral", "Pedestral light", "Lights");
            this.AddParameter("Glareshield", "Glareshield light", "Lights");
            this.AddParameter("Flashlight", "Flashlight", "Lights");
            this.AddParameter("All lights", "All lights", "Lights");

            MsfsData.Instance.Register(this);
        }
        protected override BitmapImage GetCommandImage(String actionParameter, PluginImageSize imageSize)
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
        protected override void RunCommand(String actionParameter)
        {
            SimConnectDAO.Instance.setPlugin(this.Plugin);
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
                case "Flashlight":
                    this._bindings[13].SetControllerValue(1);
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
