namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.input;
    using Loupedeck.MsfsPlugin.tools;

    class IconInput : DefaultInput
    {
        public IconInput() : base("IconSize", "ChangeIconSize", "Misc") => bindings.Add(MsfsData.Instance.Register(BindingKeys.ICON_SIZE));
        protected override void ChangeValue()
        {
            bindings[0].SetControllerValue(bindings[0].ControllerValue == 0 ? 1 : 0);
            ImageTool.Refresh();
        }
        protected override BitmapImage GetImage(PluginImageSize imageSize)
        {
            using (var bitmapBuilder = new BitmapBuilder(imageSize))
            {
                bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(bindings[0].ControllerValue));
                bitmapBuilder.DrawText("ICON");
                return bitmapBuilder.ToImage();
            }
        }
    }
}

