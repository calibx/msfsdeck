namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.input;
    using Loupedeck.MsfsPlugin.tools;

    class EngineInput : DefaultInput
    {
        public EngineInput() : base("AutoEngine", "Engine auto on/off", "Misc")
        {
            binding = Bind(BindingKeys.ENGINE_AUTO);
        }

        protected override void ChangeValue() => binding.SetControllerValue(1);

        protected override BitmapImage GetImage(PluginImageSize imageSize)
        {
            using (var bitmapBuilder = new BitmapBuilder(imageSize))
            {
                bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(binding.ControllerValue));
                bitmapBuilder.DrawText("Engines");
                return bitmapBuilder.ToImage();
            }
        }

        readonly Binding binding;
    }
}
