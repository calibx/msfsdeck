namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.input;
    class GearInput : DefaultInput
    {
        public GearInput() : base("Gear", "Display gears state", "Misc") { }
        protected override BitmapImage GetCommandImage(String actionParameter, PluginImageSize imageSize)
        {
            MsfsData.Instance.ValuesDisplayed = true;
            using (var bitmapBuilder = new BitmapBuilder(imageSize))
            {
                if (MsfsData.Instance.GearRetractable == 1)
                {
                    if (MsfsData.Instance.GearFront == 0 || MsfsData.Instance.GearFront == 16383)
                    {

                        bitmapBuilder.DrawText("\t" + this.getDisplay(MsfsData.Instance.GearFront) + "\n" + this.getDisplay(MsfsData.Instance.GearLeft) + "\t" + this.getDisplay(MsfsData.Instance.GearRight), BitmapColor.White);
                    }
                    else
                    {
                        // Gear is moving
                        bitmapBuilder.DrawText("\t" + this.getDisplay(MsfsData.Instance.GearFront) + "\n" + this.getDisplay(MsfsData.Instance.GearLeft) + "\t" + this.getDisplay(MsfsData.Instance.GearRight), new BitmapColor(255, 165, 0));
                    }
                }
                else
                {
                    bitmapBuilder.DrawText("\t" + this.getDisplay(MsfsData.Instance.GearFront) + "\n" + this.getDisplay(MsfsData.Instance.GearLeft) + "\t" + this.getDisplay(MsfsData.Instance.GearRight), new BitmapColor(0, 0, 255));
                }
                return bitmapBuilder.ToImage();
            }
        }
        private String getDisplay(Int32 gearPos) => gearPos == 0 ? "-" : gearPos == 16383 ? "|" : "/";
        protected override String GetValue() => MsfsData.Instance.CurrentGearHandle.ToString();
        protected override void ChangeValue() => MsfsData.Instance.CurrentGearHandle = MsfsData.Instance.CurrentGearHandle != 0 ? 0 : 16383;
    }
}

