namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.input;
    using Loupedeck.MsfsPlugin.tools;

    class GearInput : DefaultInput
    {
        public GearInput() : base("Gear", "Display gears state", "Misc")
        {
            retractable = Bind(BindingKeys.GEAR_RETRACTABLE);
            front = Bind(BindingKeys.GEAR_FRONT);
            left = Bind(BindingKeys.GEAR_LEFT);
            right = Bind(BindingKeys.GEAR_RIGHT);
        }
        protected override BitmapImage GetImage(PluginImageSize imageSize)
        {
            using (var bitmapBuilder = new BitmapBuilder(imageSize))
            {
                if (retractable.ControllerValue == 1)
                {
                    if (front.ControllerValue == 0 || front.ControllerValue == 10)
                    {
                        bitmapBuilder.DrawText("\t" + GetDisplay(front.MsfsValue) + "\n" + GetDisplay(left.MsfsValue) + "\t" + GetDisplay(right.MsfsValue), BitmapColor.White);
                    }
                    else
                    {
                        bitmapBuilder.DrawText("\t" + GetDisplay(front.MsfsValue) + "\n" + GetDisplay(left.MsfsValue) + "\t" + GetDisplay(right.MsfsValue), new BitmapColor(255, 165, 0));
                    }
                }
                else
                {
                    bitmapBuilder.DrawText("\t" + GetDisplay(front.MsfsValue) + "\n" + GetDisplay(left.MsfsValue) + "\t" + GetDisplay(right.MsfsValue), ImageTool.Blue);
                }
                return bitmapBuilder.ToImage();
            }
        }

        protected override void ChangeValue() => front.SetControllerValue(1);

        private string GetDisplay(double gearPos) => gearPos == 0 ? "-" : gearPos == 10 ? "|" : "/";

        readonly Binding retractable;
        readonly Binding front;
        readonly Binding left;
        readonly Binding right;
    }
}
