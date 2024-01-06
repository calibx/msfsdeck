namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.input;
    using Loupedeck.MsfsPlugin.tools;

    class PitotInput : DefaultInput
    {
        public PitotInput() : base("Pitot", "Pitot heating", "Misc")
        {
            binding = Register(BindingKeys.PITOT);
            bindings.Add(binding);
        }

        protected override void ChangeValue() => binding.SetControllerValue(1);

        protected override BitmapImage GetImage(PluginImageSize imageSize)
        {
            using (var bitmapBuilder = new BitmapBuilder(imageSize))
            {
                bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(binding.ControllerValue));
                bitmapBuilder.DrawText("Pitot");
                return bitmapBuilder.ToImage();
            }
        }

        readonly Binding binding;
    }
}

