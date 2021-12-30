namespace Loupedeck.MsfsPlugin
{
    using System;

    class CnxStateInput : PluginDynamicCommand, Notifiable
    {
        private Boolean heartbeat;
        public CnxStateInput() : base("Connection", "Display FSUIPC connexion state", "Debug")
        {
        }
        protected override String GetCommandDisplayName(String actionParameter, PluginImageSize imageSize)
        {
            MsfsData.Instance.ValuesDisplayed = true;
            this.heartbeat = !this.heartbeat;
            return (MsfsData.Instance.Connected ? "Connected" : MsfsData.Instance.TryConnect ? "Trying to connect" : "Disconnected") + (this.heartbeat ? "\n..." : "\n. .");
        }

        public void Notify() => this.AdjustmentValueChanged();

        protected override void RunCommand(String actionParameter)
        {
        }
    }
}

