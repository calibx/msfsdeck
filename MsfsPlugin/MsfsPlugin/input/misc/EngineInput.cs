namespace Loupedeck.MsfsPlugin
{

    using Loupedeck.MsfsPlugin.input;

    class EngineInput : DefaultInput
    {
        public EngineInput() : base("AutoEngine", "Engine auto on/off", "Misc")
        {
            var bind = new Binding(BindingKeys.ENGINE_AUTO);
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
                bitmapBuilder.DrawText("Engines");
                return bitmapBuilder.ToImage();
            }
        }

    }
}

