namespace Loupedeck.MsfsPlugin
{

    using Loupedeck.MsfsPlugin.input;
    using Loupedeck.MsfsPlugin.tools;
    class ParkingBrakeInput : DefaultInput
    {
        public ParkingBrakeInput() : base("Parking brake", "Display parking brakes state", "Misc")
        {
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.PARKING_BRAKES)));
        }
        protected override void ChangeValue() => this._bindings[0].SetControllerValue(ConvertTool.getToggledValue(this._bindings[0].ControllerValue));
        protected override BitmapImage GetImage(PluginImageSize imageSize)
        {
            using (var bitmapBuilder = new BitmapBuilder(imageSize))
            {
                bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(this._bindings[0].ControllerValue));
                bitmapBuilder.DrawText("Parking Brakes");
                return bitmapBuilder.ToImage();
            }
        }
    }
}

