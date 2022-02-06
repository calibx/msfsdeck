namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.input;

    class PauseInput : DefaultInput
    {
        public PauseInput() : base("Pause", "Pause", "Misc") { }
        protected override String GetValue() => MsfsData.Instance.Pause ? "Paused" : "Pause";
        protected override void ChangeValue() => MsfsData.Instance.Pause = !MsfsData.Instance.Pause;

        protected override BitmapImage GetImage(PluginImageSize imageSize)
        {
            using (var bitmapBuilder = new BitmapBuilder(imageSize))
            {
                if (MsfsData.Instance.Pause)
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

