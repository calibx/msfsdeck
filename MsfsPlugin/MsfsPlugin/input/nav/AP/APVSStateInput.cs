namespace Loupedeck.MsfsPlugin
{
    using System;

    class APVSStateInput : PluginDynamicCommand, Notifiable
    {
        public APVSStateInput() : base("AP VS", "Display AP VS state", "AP")
        {
            MsfsData.Instance.register(this);
        }
        protected override String GetCommandDisplayName(String actionParameter, PluginImageSize imageSize)
        {
            MsfsData.Instance.ValuesDisplayed = true;
            return MsfsData.Instance.ApVSHoldSwitch == 1 ? "AP VS is ON" : " AP VS is OFF";
        }

        public void Notify() => this.AdjustmentValueChanged();

        protected override void RunCommand(String actionParameter)
        {
            MsfsData.Instance.ApVSHoldSwitch = (MsfsData.Instance.ApVSHoldSwitch + 1) % 2;
        }
    }
}

