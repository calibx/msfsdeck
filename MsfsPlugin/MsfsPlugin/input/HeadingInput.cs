namespace Loupedeck.MsfsPlugin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class HeadingInput : PluginDynamicCommand, Notifiable
    {
        public HeadingInput() : base("Heading", "Display current and AP heading", "Nav") => MsfsData.Instance.register(this);

        public void Notify() => this.AdjustmentValueChanged();

        protected override String GetCommandDisplayName(String actionParameter, PluginImageSize imageSize)
        {
            MsfsData.Instance.ValuesDisplayed = true;
            return MsfsData.Instance.CurrentHeading + "\n" + MsfsData.Instance.CurrentAPHeading;
        }


        protected override void RunCommand(String actionParameter) => MsfsData.Instance.CurrentAPHeading = MsfsData.Instance.CurrentHeading;

    }
}

