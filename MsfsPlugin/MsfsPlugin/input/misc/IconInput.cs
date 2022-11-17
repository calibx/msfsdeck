namespace Loupedeck.MsfsPlugin
{

    using Loupedeck.MsfsPlugin.input;
    using Loupedeck.MsfsPlugin.tools;

    class IconInput : DefaultInput
    {
        public IconInput() : base("IconSize", "ChangeIconSize", "Misc") => this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.ICON_SIZE)));
        protected override void ChangeValue()
        {
            if (this._bindings[0].ControllerValue == 0)
            {
                this._bindings[0].SetControllerValue(1);
            } 
            else { 
                this._bindings[0].SetControllerValue(0); 
            }
            ImageTool.Refresh();
        }
        protected override BitmapImage GetImage(PluginImageSize imageSize)
        {
            using (var bitmapBuilder = new BitmapBuilder(imageSize))
            {
                bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(this._bindings[0].ControllerValue));
                bitmapBuilder.DrawText("ICON");
                return bitmapBuilder.ToImage();
            }
        }
    }
}

