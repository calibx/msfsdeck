namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.input;

    class AircraftInput : DefaultInput
    {
        public AircraftInput() : base("Aircraft", "Display current aircraft name", "Debug") { }
        protected override string GetValue() => MsfsData.Instance.AircraftName;
    }
}

