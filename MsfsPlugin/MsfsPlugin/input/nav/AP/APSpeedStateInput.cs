namespace Loupedeck.MsfsPlugin
{
    using System;

    class APSpeedStateInput : PluginDynamicCommand, Notifiable
    {
        public APSpeedStateInput() : base("AP Speed", "Display AP Speed state", "AP")
        {
            MsfsData.Instance.register(this);
        }
        protected override String GetCommandDisplayName(String actionParameter, PluginImageSize imageSize)
        {
            MsfsData.Instance.ValuesDisplayed = true;
            return MsfsData.Instance.ApSpeedHoldSwitch == 1 ? "AP speed is ON" : " AP speed is OFF";
        }

        public void Notify() => this.AdjustmentValueChanged();

        protected override void RunCommand(String actionParameter)
        {
            MsfsData.Instance.ApSpeedHoldSwitch = (MsfsData.Instance.ApSpeedHoldSwitch + 1) % 2;
        }
    }
}

