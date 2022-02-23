namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.input;

    class AutoTaxiInput : DefaultInput
    {
        public AutoTaxiInput() : base("AutoTaxi", "Auto speed to 20 knots on taxi", "Misc") { }
        protected override void ChangeValue() => MsfsData.Instance.AutoTaxiSwitch = (Int16)(MsfsData.Instance.AutoTaxiSwitch == 1 ?  2 : 1);
        protected override BitmapImage GetImage(PluginImageSize imageSize)
        {
            using (var bitmapBuilder = new BitmapBuilder(imageSize))
            {
                if (MsfsData.Instance.AutoTaxiSwitch == 2)
                {
                    bitmapBuilder.SetBackgroundImage(EmbeddedResources.ReadImage(this._imageOnResourcePath));
                }
                else if (MsfsData.Instance.AutoTaxiSwitch == 1)
                {
                    bitmapBuilder.SetBackgroundImage(EmbeddedResources.ReadImage(this._imageAvailableResourcePath));
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

