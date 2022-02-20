﻿namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.input;

    class PushbackInput : DefaultInput
    {
        public PushbackInput() : base("Pushback", "Pushback", "Misc") { }
        protected override void ChangeValue() => MsfsData.Instance.Pushback = !MsfsData.Instance.Pushback;
        protected override BitmapImage GetImage(PluginImageSize imageSize)
        {
            using (var bitmapBuilder = new BitmapBuilder(imageSize))
            {
                if (MsfsData.Instance.Pushback)
                {
                    bitmapBuilder.SetBackgroundImage(EmbeddedResources.ReadImage(this._imageOnResourcePath));
                }
                else
                {
                    bitmapBuilder.SetBackgroundImage(EmbeddedResources.ReadImage(this._imageOffResourcePath));
                }
                bitmapBuilder.DrawText("Pushback");
                return bitmapBuilder.ToImage();
            }
        }

    }
}
