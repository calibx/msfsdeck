namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.input;

    class EngineInput : DefaultInput
    {
        public EngineInput() : base("AutoEngine", "Engine auto on/off", "Misc") {
            this._binding = new Binding(BindingKeys.ENGINE_AUTO);
            MsfsData.Instance.Register(this._binding);
        }
        protected override void ChangeValue()
        {
            if (this._binding.MsfsValue.Equals("1"))
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
                if (this._binding.MsfsValue!= null && this._binding.MsfsValue.Equals("1"))
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

