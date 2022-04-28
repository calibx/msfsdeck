namespace Loupedeck.MsfsPlugin
{

    using Loupedeck.MsfsPlugin.input;

    class EngineInput : DefaultInput
    {
        public EngineInput() : base("AutoEngine", "Engine auto on/off", "Misc")
        {
            this._binding = new Binding(BindingKeys.ENGINE_AUTO);
            MsfsData.Instance.Register(this._binding);
        }
        protected override void ChangeValue() => this._binding.SetControllerValue(1);
        protected override BitmapImage GetImage(PluginImageSize imageSize)
        {
            using (var bitmapBuilder = new BitmapBuilder(imageSize))
            {
                if (this._binding.MsfsValue == 1)
                {
                    bitmapBuilder.SetBackgroundImage(EmbeddedResources.ReadImage(this._imageOnResourcePath));
                }
                else
                {
                    bitmapBuilder.SetBackgroundImage(EmbeddedResources.ReadImage(this._imageOffResourcePath));
                }
                bitmapBuilder.DrawText("Engines");
                return bitmapBuilder.ToImage();
            }
        }

    }
}

