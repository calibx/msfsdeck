namespace Loupedeck.MsfsPlugin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class VerticalSpeedInput : PluginDynamicCommand, Notifiable
    {
        public VerticalSpeedInput() : base("VS", "Display current and AP vertical speed", "Nav") => MsfsData.Instance.register(this);

        public void Notify() => this.AdjustmentValueChanged();

        protected override String GetCommandDisplayName(String actionParameter, PluginImageSize imageSize)
        {
            MsfsData.Instance.ValuesDisplayed = true;
            return MsfsData.Instance.CurrentVerticalSpeed + "\n" + MsfsData.Instance.CurrentAPVerticalSpeed;
        }

        protected override void RunCommand(String actionParameter) => MsfsData.Instance.CurrentAPVerticalSpeed = (Int32)(Math.Round(MsfsData.Instance.CurrentVerticalSpeed / 100d, 0) * 100);


    }
}

