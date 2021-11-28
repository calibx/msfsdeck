namespace Loupedeck.MsfsPlugin
{
    using System;

    public class MsfsApplication : ClientApplication
    {
        public MsfsApplication()
        {

        }

        protected override String GetProcessName() => "FlightSimulator";

        protected override String GetBundleName() => "";
    }
}