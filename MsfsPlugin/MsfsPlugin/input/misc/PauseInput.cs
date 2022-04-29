namespace Loupedeck.MsfsPlugin
{

    using Loupedeck.MsfsPlugin.input;

    class PauseInput : DefaultInput
    {
        public PauseInput() : base("Pause", "Pause", "Misc")
        {
            var bind = new Binding(BindingKeys.PAUSE);
            this._bindings.Add(bind);
            MsfsData.Instance.Register(bind);
        }
        protected override void ChangeValue() => this._bindings[0].SetControllerValue(1);
        protected override BitmapImage GetImage(PluginImageSize imageSize)
        {
            using (var bitmapBuilder = new BitmapBuilder(imageSize))
            {
                if (this._bindings[0].ControllerValue == 1)
                {
                    bitmapBuilder.SetBackgroundImage(EmbeddedResources.ReadImage(this._imageOnResourcePath));
                }
                else
                {
                    bitmapBuilder.SetBackgroundImage(EmbeddedResources.ReadImage(this._imageOffResourcePath));
                }
                bitmapBuilder.DrawText("Pause");
                return bitmapBuilder.ToImage();
            }
        }
    }
}

