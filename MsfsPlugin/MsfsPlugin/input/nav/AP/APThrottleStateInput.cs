namespace Loupedeck.MsfsPlugin
{
    using System;

    class APThrottleStateInput : PluginDynamicCommand, Notifiable
    {
        public APThrottleStateInput() : base("AP Throttle", "Display AP throttle state", "AP")
        {
            MsfsData.Instance.register(this);
        }
        protected override String GetCommandDisplayName(String actionParameter, PluginImageSize imageSize)
        {
            MsfsData.Instance.ValuesDisplayed = true;
            return MsfsData.Instance.ApThrottleSwitch == 1 ? "Disable Autothrottle" : "Enable Autothrottle";
        }

        public void Notify() => this.AdjustmentValueChanged();

        protected override void RunCommand(String actionParameter)
        {
            MsfsData.Instance.ApThrottleSwitch = (MsfsData.Instance.ApThrottleSwitch + 1) % 2;
        }
    }
}

