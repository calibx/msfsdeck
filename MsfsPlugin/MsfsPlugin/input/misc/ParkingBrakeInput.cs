namespace Loupedeck.MsfsPlugin
{

    using Loupedeck.MsfsPlugin.input;
    class ParkingBrakeInput : DefaultInput
    {
        public ParkingBrakeInput() : base("Parking brake", "Display parking brakes state", "Misc") { }

        protected override void ChangeValue() => MsfsData.Instance.CurrentBrakes = !MsfsData.Instance.CurrentBrakes;
        protected override BitmapImage GetImage(PluginImageSize imageSize)
        {
            using (var bitmapBuilder = new BitmapBuilder(imageSize))
            {
                if (MsfsData.Instance.CurrentBrakes)
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

