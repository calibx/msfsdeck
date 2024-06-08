namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.input;
    using Loupedeck.MsfsPlugin.tools;

    class ParkingBrakeInput : DefaultInput
    {
        public ParkingBrakeInput() : base("Parking brake", "Display parking brakes state", "Misc")
        {
            binding = Bind(BindingKeys.PARKING_BRAKES);
        }

        protected override void ChangeValue() => binding.ToggleControllerValue();

        protected override BitmapImage GetImage(PluginImageSize imageSize)
        {
            using (var bitmapBuilder = new BitmapBuilder(imageSize))
            {
                bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(binding.ControllerValue));
                bitmapBuilder.DrawText("Parking Brakes");
                return bitmapBuilder.ToImage();
            }
        }

        readonly Binding binding;
    }
}
