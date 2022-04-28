﻿namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.input;
    using Loupedeck.MsfsPlugin.msfs;
    class SimCnxStateInput : DefaultInput
    {
        private Boolean heartbeat;
        public SimCnxStateInput() : base("ConnectionSimConnect", "Display SimConnect connection state", "Debug") { }

        protected override String GetValue()
        {
            this.heartbeat = !this.heartbeat;
            return (MsfsData.Instance.SimConnected ? "Connected" : MsfsData.Instance.SimTryConnect ? "Trying to connect" : "Disconnected") + (this.heartbeat ? "\n..." : "\n. .");
        }

        protected override void ChangeValue() => SimConnectDAO.Instance.Connect(this.Plugin);

    }
}

