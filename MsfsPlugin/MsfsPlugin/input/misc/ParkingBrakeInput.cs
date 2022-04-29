namespace Loupedeck.MsfsPlugin
{

    using Loupedeck.MsfsPlugin.input;
    class ParkingBrakeInput : DefaultInput
    {
        public ParkingBrakeInput() : base("Parking brake", "Display parking brakes state", "Misc")
        {
            var bind = new Binding(BindingKeys.PARKING_BRAKES);
            this._bindings.Add(bind);
            MsfsData.Instance.Register(bind);
        }
        protected override void ChangeValue() => this._bindings[0].SetControllerValue(1);
        protected override BitmapImage GetImage(PluginImageSize imageSize)
        {
            using (var bitmapBuilder = new BitmapBuilder(imageSize))
            {
                if (this._bindings[0].MsfsValue == 1)
                {
                    bitmapBuilder.SetBackgroundImage(EmbeddedResources.ReadImage(this._imageOnResourcePath));
                }
                else
                {
                    bitmapBuilder.SetBackgroundImage(EmbeddedResources.ReadImage(this._imageOffResourcePath));
                }
                bitmapBuilder.DrawText("Brakes");
                return bitmapBuilder.ToImage();
            }
        }
    }
}

