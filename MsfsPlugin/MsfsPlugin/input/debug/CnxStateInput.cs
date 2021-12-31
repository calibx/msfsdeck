namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.input;
    class CnxStateInput : DefaultInput
    {
        private Boolean heartbeat;
        public CnxStateInput() : base("Connection", "Display FSUIPC connection state", "Debug") { }

        protected override String GetValue()
        {
                this.heartbeat = !this.heartbeat;
                return (MsfsData.Instance.Connected ? "Connected" : MsfsData.Instance.TryConnect ? "Trying to connect" : "Disconnected") + (this.heartbeat ? "\n..." : "\n. .");
        }

    }
}

