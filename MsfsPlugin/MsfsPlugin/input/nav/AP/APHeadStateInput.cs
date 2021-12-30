namespace Loupedeck.MsfsPlugin
{
    using System;

    class APHeadStateInput : PluginDynamicCommand, Notifiable
    {
        public APHeadStateInput() : base("AP Head", "Display AP Head state", "AP")
        {
            MsfsData.Instance.register(this);
        }
        protected override String GetCommandDisplayName(String actionParameter, PluginImageSize imageSize)
        {
            MsfsData.Instance.ValuesDisplayed = true;
            return MsfsData.Instance.ApHeadHoldSwitch == 1 ? "AP Head is ON" : "AP Head is OFF";
        }

        public void Notify() => this.AdjustmentValueChanged();

        protected override void RunCommand(String actionParameter)
        {
            MsfsData.Instance.ApHeadHoldSwitch = (MsfsData.Instance.ApHeadHoldSwitch + 1) % 2;
        }
    }
}

