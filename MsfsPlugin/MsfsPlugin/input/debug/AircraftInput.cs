namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.input;

    class AircraftInput : DefaultInput
    {
        public AircraftInput() : base("Aircraft", "Display current aircraft name", "Debug") { }
        protected override String GetValue() => MsfsData.Instance.AircraftName;
    }
}

