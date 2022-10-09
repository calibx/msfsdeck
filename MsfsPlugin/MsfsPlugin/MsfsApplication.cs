namespace Loupedeck.MsfsPlugin
{
    using System;

    public class MSFSApplication : ClientApplication
    {
        public MSFSApplication()
        {

        }

        protected override String GetProcessName() => "FlightSimulator";

    }
}