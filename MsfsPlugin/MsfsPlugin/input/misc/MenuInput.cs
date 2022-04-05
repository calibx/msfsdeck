namespace Loupedeck.MsfsPlugin
{

    using Loupedeck.MsfsPlugin.input;

    class MenuInput : DefaultInput
    {
        public MenuInput() : base("Menu", "Menu", "Misc") { }
        protected override void ChangeValue() => MsfsData.Instance.Menu = true;
        protected override BitmapImage GetImage(PluginImageSize imageSize)
        {
            using (var bitmapBuilder = new BitmapBuilder(imageSize))
            {
                if (MsfsData.Instance.Menu)
                {
                    bitmapBuilder.SetBackgroundImage(EmbeddedResources.ReadImage(this._imageOnResourcePath));
                }
                else
                {
                    bitmapBuilder.SetBackgroundImage(EmbeddedResources.ReadImage(this._imageOffResourcePath));
                }
                bitmapBuilder.DrawText("Menu");
                return bitmapBuilder.ToImage();
            }
        }

    }
}

