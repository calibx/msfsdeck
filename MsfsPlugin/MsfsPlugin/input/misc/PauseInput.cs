namespace Loupedeck.MsfsPlugin
{

    using Loupedeck.MsfsPlugin.input;
    using Loupedeck.MsfsPlugin.tools;
    class PauseInput : DefaultInput
    {
        public PauseInput() : base("Pause", "Pause", "Misc")
        {
            this.bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.PAUSE)));
        }
        protected override void ChangeValue() => this.bindings[0].SetControllerValue(1);
        protected override BitmapImage GetImage(PluginImageSize imageSize)
        {
            using (var bitmapBuilder = new BitmapBuilder(imageSize))
            {
                bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(this.bindings[0].ControllerValue));
                bitmapBuilder.DrawText("Pause");
                return bitmapBuilder.ToImage();
            }
        }
    }
}

