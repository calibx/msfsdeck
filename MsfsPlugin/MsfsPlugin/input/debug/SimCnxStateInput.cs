namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.input;
    using Loupedeck.MsfsPlugin.msfs;

    class SimCnxStateInput : DefaultInput
    {
        public SimCnxStateInput() : base("ConnectionSimConnect", "Display SimConnect connection state", "Debug") => bindings.Add(MsfsData.Instance.Register(BindingKeys.CONNECTION));

        protected override string GetValue() => bindings[0].MsfsValue == 1 ? "connected" : bindings[0].MsfsValue == 2 ? "trying to\nconnect" : "not\nconnected";

        protected override void RunCommand(string actionParameter)
        {
            var curValue = bindings[0].MsfsValue;
            if ( curValue == 1 || curValue == 2)
                SimConnectDAO.Instance.Disconnect();
            else
                SimConnectDAO.Instance.Connect();
        }
    }
}
