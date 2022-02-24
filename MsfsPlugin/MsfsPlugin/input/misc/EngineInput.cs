namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.input;

    class EngineInput : DefaultInput
    {
        public EngineInput() : base("AutoEngine", "Engine auto on/off", "Misc") { }
        protected override void ChangeValue()
        {
            if (this.EngineIsOn())
            {
                MsfsData.Instance.EngineAutoOff = true;
            }
            else
            {
                MsfsData.Instance.EngineAutoOn = true;
            }
        }
        protected override BitmapImage GetImage(PluginImageSize imageSize)
        {
            using (var bitmapBuilder = new BitmapBuilder(imageSize))
            {
                if (this.EngineIsOn())
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
        private Boolean EngineIsOn() => MsfsData.Instance.EngineType == 0 ? MsfsData.Instance.Rpm > 1 : MsfsData.Instance.E1N1 > 0.1;

    }
}

