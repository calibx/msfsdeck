namespace Loupedeck.MsfsPlugin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class APNavStateInput : PluginDynamicCommand, Notifiable
    {
        public APNavStateInput() : base("AP Nav", "Display AP Nav state", "AP")
        {
            MsfsData.Instance.register(this);
        }
        protected override String GetCommandDisplayName(String actionParameter, PluginImageSize imageSize)
        {
            MsfsData.Instance.ValuesDisplayed = true;
            return MsfsData.Instance.ApNavHoldSwitch == 1 ? "Nav is ON" : "Nav is OFF";
        }

        public void Notify() => this.AdjustmentValueChanged();

        protected override void RunCommand(String actionParameter)
        {
            MsfsData.Instance.ApNavHoldSwitch = (MsfsData.Instance.ApNavHoldSwitch + 1) % 2;
        }
    }
}

