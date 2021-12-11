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
                    bitmapBuilder.DrawText("\t" + this.getDisplay(MsfsData.Instance.GearFront) + "\n" + this.getDisplay(MsfsData.Instance.GearLeft) + "\t" + this.getDisplay(MsfsData.Instance.GearRight), new BitmapColor(255, 0, 0));
                }
                else
                {
                    bitmapBuilder.DrawText("\t" + this.getDisplay(MsfsData.Instance.GearFront) + "\n" + this.getDisplay(MsfsData.Instance.GearLeft) + "\t" + this.getDisplay(MsfsData.Instance.GearRight), BitmapColor.White);
                }

                return bitmapBuilder.ToImage();
            }
        }

        private String getDisplay(Int32 gearPos) => gearPos == 0 ? "-" : "|";
        protected override void RunCommand(String actionParameter)
        {
            MsfsData.Instance.CurrentGearHandle = MsfsData.Instance.CurrentGearHandle != 0 ? 0 : 16383;
        }
    }
}

