namespace Loupedeck.MsfsPlugin
{

    using Loupedeck.MsfsPlugin.input;
    using Loupedeck.MsfsPlugin.tools;

    class EngineInput : DefaultInput
    {
        public EngineInput() : base("AutoEngine", "Engine auto on/off", "Misc") => this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.ENGINE_AUTO)));
        protected override void ChangeValue() => this._bindings[0].SetControllerValue(1);
        protected override BitmapImage GetImage(PluginImageSize imageSize)
        {
            using (var bitmapBuilder = new BitmapBuilder(imageSize))
            {
                bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(this._bindings[0].ControllerValue));
                bitmapBuilder.DrawText("Engines");
                return bitmapBuilder.ToImage();
            }
        }
    }
}

