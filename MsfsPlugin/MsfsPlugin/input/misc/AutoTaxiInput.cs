namespace Loupedeck.MsfsPlugin
{

    using Loupedeck.MsfsPlugin.input;

    class AutoTaxiInput : DefaultInput
    {
        public AutoTaxiInput() : base("AutoTaxi", "Auto speed to 20 knots on taxi", "Misc") => this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.AUTO_TAXI)));
        protected override void ChangeValue() => this._bindings[0].SetControllerValue(this._bindings[0].MsfsValue == 1 ? 2 : 1);
        protected override BitmapImage GetImage(PluginImageSize imageSize)
        {
            using (var bitmapBuilder = new BitmapBuilder(imageSize))
            {
                if (this._bindings[0].MsfsValue == 2)
                {
                    bitmapBuilder.SetBackgroundImage(EmbeddedResources.ReadImage(this._imageOnResourcePath));
                }
                else if (this._bindings[0].MsfsValue == 1)
                {
                    bitmapBuilder.SetBackgroundImage(EmbeddedResources.ReadImage(this._imageAvailableResourcePath));
                }
                else if (this._bindings[0].MsfsValue == 3)
                {
                    bitmapBuilder.SetBackgroundImage(EmbeddedResources.ReadImage(this._imageWaitResourcePath));
                }
                else
                {
                    bitmapBuilder.SetBackgroundImage(EmbeddedResources.ReadImage(this._imageDisableResourcePath));
                }
                bitmapBuilder.DrawText("AutoTaxi");
                return bitmapBuilder.ToImage();
            }
        }

    }
}
