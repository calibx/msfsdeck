namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.input;

    class PushbackRightInput : DefaultInput
    {
        public PushbackRightInput() : base("Pushback Right", "Pushback Right", "Misc") { }
        protected override void ChangeValue() => MsfsData.Instance.Pushback = (Int16)(MsfsData.Instance.Pushback == 2 ? 3 : 2);
        protected override BitmapImage GetImage(PluginImageSize imageSize)
        {
            using (var bitmapBuilder = new BitmapBuilder(imageSize))
            {
                if (MsfsData.Instance.Pushback == 2)
                {
                    bitmapBuilder.SetBackgroundImage(EmbeddedResources.ReadImage(this._imageOnResourcePath));
                }
                else
                {
                    bitmapBuilder.SetBackgroundImage(EmbeddedResources.ReadImage(this._imageOffResourcePath));
                }
                bitmapBuilder.DrawText("Pushback R");
                return bitmapBuilder.ToImage();
            }
        }

    }
}

