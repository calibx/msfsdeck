namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.input;
    using Loupedeck.MsfsPlugin.tools;

    class PauseInput : DefaultInput
    {
        public PauseInput() : base("Pause", "Pause", "Misc")
        {
            binding = Bind(BindingKeys.PAUSE);
        }

        protected override void ChangeValue() => binding.SetControllerValue(1);

        protected override BitmapImage GetImage(PluginImageSize imageSize)
        {
            using (var bitmapBuilder = new BitmapBuilder(imageSize))
            {
                bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(binding.ControllerValue));
                bitmapBuilder.DrawText("Pause");
                return bitmapBuilder.ToImage();
            }
        }

        readonly Binding binding;
    }
}
