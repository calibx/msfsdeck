namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.input;
    using Loupedeck.MsfsPlugin.tools;

    class GearInput : DefaultInput
    {
        public GearInput() : base("Gear", "Display gears state", "Misc")
        {
            bindings.Add(MsfsData.Instance.Register(BindingKeys.GEAR_RETRACTABLE));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.GEAR_FRONT));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.GEAR_LEFT));
            bindings.Add(MsfsData.Instance.Register(BindingKeys.GEAR_RIGHT));
        }
        protected override BitmapImage GetImage(PluginImageSize imageSize)
        {
            using (var bitmapBuilder = new BitmapBuilder(imageSize))
            {
                if (bindings[0].ControllerValue == 1)
                {
                    if (bindings[1].ControllerValue == 0 || bindings[1].ControllerValue == 10)
                    {
                        bitmapBuilder.DrawText("\t" + GetDisplay(bindings[1].MsfsValue) + "\n" + GetDisplay(bindings[2].MsfsValue) + "\t" + GetDisplay(bindings[3].MsfsValue), BitmapColor.White);
                    }
                    else
                    {
                        bitmapBuilder.DrawText("\t" + GetDisplay(bindings[1].MsfsValue) + "\n" + GetDisplay(bindings[2].MsfsValue) + "\t" + GetDisplay(bindings[3].MsfsValue), new BitmapColor(255, 165, 0));
                    }
                }
                else
                {
                    bitmapBuilder.DrawText("\t" + GetDisplay(bindings[1].MsfsValue) + "\n" + GetDisplay(bindings[2].MsfsValue) + "\t" + GetDisplay(bindings[3].MsfsValue), ImageTool.Blue);
                }
                return bitmapBuilder.ToImage();
            }
        }
        private string GetDisplay(double gearPos) => gearPos == 0 ? "-" : gearPos == 10 ? "|" : "/";
        protected override void ChangeValue() => bindings[1].SetControllerValue(1);
    }
}

