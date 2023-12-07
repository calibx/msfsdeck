namespace Loupedeck.MsfsPlugin
{

    using Loupedeck.MsfsPlugin.input;
    using Loupedeck.MsfsPlugin.tools;

    class IconInput : DefaultInput
    {
        public IconInput() : base("IconSize", "ChangeIconSize", "Misc") => this.bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.ICON_SIZE)));
        protected override void ChangeValue()
        {
            if (this.bindings[0].ControllerValue == 0)
            {
                this.bindings[0].SetControllerValue(1);
            } 
            else { 
                this.bindings[0].SetControllerValue(0); 
            }
            ImageTool.Refresh();
        }
        protected override BitmapImage GetImage(PluginImageSize imageSize)
        {
            using (var bitmapBuilder = new BitmapBuilder(imageSize))
            {
                bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(this.bindings[0].ControllerValue));
                bitmapBuilder.DrawText("ICON");
                return bitmapBuilder.ToImage();
            }
        }
    }
}

