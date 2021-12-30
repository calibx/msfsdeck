namespace Loupedeck.MsfsPlugin
{
    using System;

    class APStateInput : PluginDynamicCommand, Notifiable
    {
        public APStateInput() : base("AP Master", "Display AP state", "AP")
        {
            MsfsData.Instance.register(this);
        }
        protected override String GetCommandDisplayName(String actionParameter, PluginImageSize imageSize)
        {
            MsfsData.Instance.ValuesDisplayed = true;
            return MsfsData.Instance.ApSwitch == 1 ? "AP is ON" : "AP is OFF";
        }

        public void Notify() => this.AdjustmentValueChanged();

        protected override void RunCommand(String actionParameter)
        {
            MsfsData.Instance.ApSwitch = (MsfsData.Instance.ApSwitch + 1) % 2;
        }
    }
}

