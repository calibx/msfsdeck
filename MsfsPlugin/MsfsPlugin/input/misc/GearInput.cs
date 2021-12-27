namespace Loupedeck.MsfsPlugin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class GearInput : PluginDynamicCommand, Notifiable
    {
        public GearInput() : base("Gear", "Display gears state", "Misc")

        {
            MsfsData.Instance.register(this);
        }

        public void Notify() => this.AdjustmentValueChanged();

        protected override BitmapImage GetCommandImage(String actionParameter, PluginImageSize imageSize)
        {
            MsfsData.Instance.ValuesDisplayed = true;
            using (var bitmapBuilder = new BitmapBuilder(imageSize))
            {
                if (MsfsData.Instance.GearOverSpeed == 1)
                {
                    // Don't know when it is used in MSFS
                    bitmapBuilder.DrawText("\t" + this.getDisplay(MsfsData.Instance.GearFront) + "\n" + this.getDisplay(MsfsData.Instance.GearLeft) + "\t" + this.getDisplay(MsfsData.Instance.GearRight), new BitmapColor(255, 0, 0));
                }
                else if (MsfsData.Instance.GearFront == 0 || MsfsData.Instance.GearFront == 16383)
                {

                    bitmapBuilder.DrawText("\t" + this.getDisplay(MsfsData.Instance.GearFront) + "\n" + this.getDisplay(MsfsData.Instance.GearLeft) + "\t" + this.getDisplay(MsfsData.Instance.GearRight), BitmapColor.White);
                }
                else
                {
                    // Gear is moving
                    bitmapBuilder.DrawText("\t" + this.getDisplay(MsfsData.Instance.GearFront) + "\n" + this.getDisplay(MsfsData.Instance.GearLeft) + "\t" + this.getDisplay(MsfsData.Instance.GearRight), new BitmapColor(255, 165, 0));
                }

                return bitmapBuilder.ToImage();
            }
        }

        private String getDisplay(Int32 gearPos) => gearPos == 0 ? "-" : gearPos == 16383 ? "|" : "/";

        protected override void RunCommand(String actionParameter) => MsfsData.Instance.CurrentGearHandle = MsfsData.Instance.CurrentGearHandle != 0 ? 0 : 16383;
    }
}

