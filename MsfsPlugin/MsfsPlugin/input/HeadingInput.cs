namespace Loupedeck.MsfsPlugin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class HeadingInput : PluginDynamicCommand, Notifiable
    {
        public HeadingInput() : base("Heading", "Display current and AP heading", "Nav")
        {
            MsfsData.Instance.register(this);
        }

        public void Notify() => this.AdjustmentValueChanged();

        protected override String GetCommandDisplayName(String actionParameter, PluginImageSize imageSize)
        {
            return MsfsData.Instance.currentHeading + "\n" + MsfsData.Instance.currentAPHeading;
        }

        protected override void RunCommand(String actionParameter)
        {
            MsfsData.Instance.currentAPHeading = MsfsData.Instance.currentHeading;
            MsfsData.Instance.changed();
        }
    }
}

