namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.input;
    using Loupedeck.MsfsPlugin.tools;

    class SpoilerArmInput : DefaultInput
    {
        public SpoilerArmInput() : base("SpoilerArm", "Spoiler Arm", "Misc")
        {
            binding = MsfsData.Instance.Register(BindingKeys.SPOILERS_ARM);
            bindings.Add(binding);
        }

        protected override void ChangeValue() => binding.SetControllerValue(1);

        protected override BitmapImage GetImage(PluginImageSize imageSize)
        {
            using (var bitmapBuilder = new BitmapBuilder(imageSize))
            {
                bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(binding.ControllerValue));
                bitmapBuilder.DrawText("Spoiler Arm");
                return bitmapBuilder.ToImage();
            }
        }

        readonly Binding binding;
    }
}

