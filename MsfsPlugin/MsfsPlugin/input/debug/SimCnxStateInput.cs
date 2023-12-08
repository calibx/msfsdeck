namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.input;
    class SimCnxStateInput : DefaultInput
    {
        public SimCnxStateInput() : base("ConnectionSimConnect", "Display SimConnect connection state", "Debug") => bindings.Add(MsfsData.Instance.Register(BindingKeys.CONNECTION));
        protected override string GetValue() => bindings[0].MsfsValue == 1 ? "Connected" : bindings[0].MsfsValue == 2 ? "Trying to connect" : "Disconnected";
    }
}

