namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.input;
    using Loupedeck.MsfsPlugin.tools;

    class AutoTaxiInput : DefaultInput
    {
        public AutoTaxiInput() : base("AutoTaxi", "Auto speed to 20 knots on taxi", "Misc")
        {
            binding = Bind(BindingKeys.AUTO_TAXI);
        }

        protected override void ChangeValue() => binding.SetControllerValue(binding.MsfsValue == 1 ? 2 : 1);

        protected override BitmapImage GetImage(PluginImageSize imageSize)
        {
            using (var bitmapBuilder = new BitmapBuilder(imageSize))
            {
                if (binding.MsfsValue == 2)
                {
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetOnAvailableWaitDisableImage(2));
                }
                else if (binding.MsfsValue == 1)
                {
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetOnAvailableWaitDisableImage(1));
                }
                else if (binding.MsfsValue == 3)
                {
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetOnAvailableWaitDisableImage(3));
                }
                else
                {
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetOnAvailableWaitDisableImage(0));
                }
                bitmapBuilder.DrawText("AutoTaxi");
                return bitmapBuilder.ToImage();
            }
        }

        readonly Binding binding;
    }
}
