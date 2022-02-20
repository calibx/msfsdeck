namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.input;

    class PushbackLeftInput : DefaultInput
    {
        public PushbackLeftInput() : base("Pushback Left", "Pushback left", "Misc") { }
        protected override void ChangeValue() => MsfsData.Instance.Pushback = (Int16)(MsfsData.Instance.Pushback == 1 ? 3 : 1); 
        protected override BitmapImage GetImage(PluginImageSize imageSize)
        {
            using (var bitmapBuilder = new BitmapBuilder(imageSize))
            {
                if (MsfsData.Instance.Pushback == 1)
                {
                    bitmapBuilder.SetBackgroundImage(EmbeddedResources.ReadImage(this._imageOnResourcePath));
                }
                else 
                {
                    bitmapBuilder.SetBackgroundImage(EmbeddedResources.ReadImage(this._imageOffResourcePath));
                }
                bitmapBuilder.DrawText("Pushback L");
                return bitmapBuilder.ToImage();
            }
        }

    }
}

