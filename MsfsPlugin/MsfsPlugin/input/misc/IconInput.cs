namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.input;
    using Loupedeck.MsfsPlugin.tools;

    class IconInput : DefaultInput
    {
        public IconInput() : base("IconSize", "ChangeIconSize", "Misc")
        { }

        protected override void ChangeValue()
        {
            value = !value;
            ImageTool.Refresh(value);
        }

        protected override BitmapImage GetImage(PluginImageSize imageSize)
        {
            using (var bitmapBuilder = new BitmapBuilder(imageSize))
            {
                bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(value ? 1 : 0));
                bitmapBuilder.DrawText("Icon size");
                return bitmapBuilder.ToImage();
            }
        }

        bool value = false;
    }
}
