namespace Loupedeck.MsfsPlugin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class APAltStateInput : PluginDynamicCommand, Notifiable
    {
        public APAltStateInput() : base("AP Alt", "Display AP Alt state", "AP")
        {
            MsfsData.Instance.register(this);
        }
        protected override String GetCommandDisplayName(String actionParameter, PluginImageSize imageSize)
        {
            MsfsData.Instance.ValuesDisplayed = true;
            return MsfsData.Instance.ApAltHoldSwitch == 1 ? "AP Alt is ON" : "AP Alt is OFF";
        }

        public void Notify() => this.AdjustmentValueChanged();

        protected override void RunCommand(String actionParameter)
        {
            MsfsData.Instance.ApAltHoldSwitch = (MsfsData.Instance.ApAltHoldSwitch + 1) % 2;
        }
    }
}

