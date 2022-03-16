namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.input;
    using Loupedeck.MsfsPlugin.msfs;

    class SimConnectDebugInput : DefaultInput
    {

        public SimConnectDebugInput() : base("DebugSim", "Display debugged value ", "Debug") { }
        protected override String GetValue() => "Debug\n" + MsfsData.Instance.DebugValue1 + "\n" + MsfsData.Instance.DebugValue2 + "\n" + MsfsData.Instance.DebugValue3;
        protected override void ChangeValue() {

            SimConnectDAO dao = new SimConnectDAO();
            dao.Connect();

        }
    }
}

