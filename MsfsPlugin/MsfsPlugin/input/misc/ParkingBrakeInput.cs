namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.input;
    using Loupedeck.MsfsPlugin.tools;

    class ParkingBrakeInput : DefaultInput
    {
        public ParkingBrakeInput() : base("Parking brake", "Display parking brakes state", "Misc")
        {
            binding = Register(BindingKeys.PARKING_BRAKES);
            bindings.Add(binding);
        }

        protected override void ChangeValue() => binding.SetControllerValue(ConvertTool.GetToggledValue(binding.ControllerValue));

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

