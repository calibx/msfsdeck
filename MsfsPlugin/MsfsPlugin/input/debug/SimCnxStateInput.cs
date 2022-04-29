namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.input;
    using Loupedeck.MsfsPlugin.msfs;
    class SimCnxStateInput : DefaultInput
    {
       public SimCnxStateInput() : base("ConnectionSimConnect", "Display SimConnect connection state", "Debug")
        {
            var bind = new Binding(BindingKeys.CONNECTION);
            this._bindings.Add(bind);
            MsfsData.Instance.Register(bind);
        }
        protected override String GetValue() => this._bindings[0].MsfsValue == 1 ? "Connected" : this._bindings[0].MsfsValue == 2 ? "Trying to connect" : "Disconnected";
    }
}

